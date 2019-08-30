using System;
using System.Collections.Generic;
using System.Text;

namespace Cuhogaus
{
    public sealed class MersenneTwister : IRng
    {
        private const int WordSize = sizeof(uint) * 8;
        private const int RecurrenceDegree = 624;
        private const int MiddleWord = 397;
        private const int OneWordSeparationPoint = 31;

        private const uint TwistMatrixCoefficients = 0x9908B0DF;

    }
}
