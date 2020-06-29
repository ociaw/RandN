<#
MIT License

Copyright (c) 2020 James Turner

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.



This build script is originally based off of Quickenshtein's build.ps1:
https://github.com/Turnerj/Quickenshtein/blob/662f80f2820ba53e6c795efd6d8be1d5277166ac/build.ps1

Along with buildconfig.json
#>


[CmdletBinding(PositionalBinding=$false)]
param(
    [bool] $RunTests = $true,
    [bool] $CreatePackages,
    [string] $BuildVersion
)

$packageOutputFolder = "$PSScriptRoot\build-artifacts"
mkdir -Force $packageOutputFolder | Out-Null

$solution = "src/RandN.sln"
$testProject = "src/Tests/Tests.csproj"

if (-not $BuildVersion)
{
    $lastTaggedVersion = git describe --tags --abbrev=0
    if ($lastTaggedVersion -contains "fatal")
    {
        $lastTaggedVersion = "0.0.0"
    }

    $BuildVersion = $lastTaggedVersion
}

Write-Host "Run Parameters:" -ForegroundColor Cyan
Write-Host "  RunTests: $RunTests"
Write-Host "  CreatePackages: $CreatePackages"
Write-Host "  BuildVersion: $BuildVersion"
Write-Host "Configuration:" -ForegroundColor Cyan
Write-Host "  TestProject: $testProject"
Write-Host "Environment:" -ForegroundColor Cyan
Write-Host "  .NET Version:" (dotnet --version)
Write-Host "  Artifact Path: $packageOutputFolder"

Write-Host "Building solution..." -ForegroundColor "Magenta"
dotnet build -c Release $solution /p:Version=$BuildVersion
if ($LastExitCode -ne 0)
{
    Write-Host "Build failed, aborting!" -Foreground "Red"
    Exit 1
}
Write-Host "Solution built!" -ForegroundColor "Green"

if ($RunTests)
{
    Write-Host "Running tests without coverage..." -ForegroundColor "Magenta"

    $env:COMPlus_EnableAVX2 = 1
    $env:COMPlus_EnableSSE2 = 1
    Write-Host "Test Environment: Normal" -ForegroundColor "Cyan"
    dotnet test $testProject --framework netcoreapp3.1
    if ($LastExitCode -ne 0)
    {
        Write-Host "Tests failed, aborting build!" -Foreground "Red"
        Exit 1
    }

    $env:COMPlus_EnableAVX2 = 0
    $env:COMPlus_EnableSSE2 = 1
    Write-Host "Test Environment: AVX2 Disabled" -ForegroundColor "Cyan"
    dotnet test $testProject --framework netcoreapp3.1
    if ($LastExitCode -ne 0)
    {
        Write-Host "Tests failed, aborting build!" -Foreground "Red"
        Exit 1
    }

    $env:COMPlus_EnableAVX2 = 0
    $env:COMPlus_EnableSSE2 = 0
    Write-Host "Test Environment: SSE2 Disabled" -ForegroundColor "Cyan"
    dotnet test $testProject --framework netcoreapp3.1
    if ($LastExitCode -ne 0)
    {
        Write-Host "Tests failed, aborting build!" -Foreground "Red"
        Exit 1
    }

    Write-Host "Tests passed!" -ForegroundColor "Green"
}

if ($CreatePackages)
{
    $dirty = $(hg id --template "{dirty}") -eq "+"
    if ($dirty)
    {
        Write-Host "Working directory is dirty, aborting packaging!" -Foreground "Red"
        Exit 1
    }

    Write-Host "Clearing existing $packageOutputFolder... " -NoNewline
    Get-ChildItem $packageOutputFolder | Remove-Item
    Write-Host "Packages cleared!" -ForegroundColor "Green"

    Write-Host "Packing..." -ForegroundColor "Magenta"
    PackProject src/RandN.csproj
    PackProject src/Core.csproj
    Write-Host "Packing complete!" -ForegroundColor "Green"
}

function PackProject
{
    $branch = $(hg id --bookmark -r .)
    $commit = $(hg id --debug --id -r .)
    Param([string] $path)
    dotnet pack $path `
        --no-build `
        --no-logo `
        --configuration Release `
        /p:Version=$BuildVersion `
        /p:Repository/Branch=$branch `
        /p:Repository/Commit=$commit `
        /p:PackageOutputPath=$packageOutputFolder
}
