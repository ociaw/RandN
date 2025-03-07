using System;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace RandN.Benchmarks;

internal sealed class ThroughputColumn(UInt64 bytesPerIteration) : IColumn
{
    public UInt64 BytesPerIteration { get; } = bytesPerIteration;

    public String Id => nameof(ThroughputColumn);

    public String ColumnName => "Throughput";

    public Boolean AlwaysShow => true;

    public ColumnCategory Category => ColumnCategory.Custom;

    public Int32 PriorityInCategory => 0;

    public Boolean IsNumeric => true;

    public UnitType UnitType => UnitType.Dimensionless;

    public String Legend => "Megabytes per Second";

    public Boolean IsAvailable(Summary summary) => true;

    public Boolean IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;

    public String GetValue(Summary summary, BenchmarkCase benchmarkCase)
    {
        Double meanNsPerIteration = summary[benchmarkCase]?.ResultStatistics?.Mean ?? 0d;
        Double meanSecPerIteration = meanNsPerIteration / (1.0 * 1000 * 1000 * 1000);
        Double bytesPerSecond = BytesPerIteration / meanSecPerIteration;
        Double megabytesPerSecond = bytesPerSecond / (1024 * 1024);
        return megabytesPerSecond.ToString("N2");
    }

    public String GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
    {
        String unit = style.PrintUnitsInContent ? " MB/s" : "";
        return GetValue(summary, benchmarkCase) + unit;
    }

}
