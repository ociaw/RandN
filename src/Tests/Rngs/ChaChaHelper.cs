using System;
using System.Collections.Immutable;

namespace RandN.Rngs;

/// <summary>
/// Helpers for ChaCha tests.
/// </summary>
public static class ChaChaHelper
{
    /// <summary>
    /// 1024 bytes of output from ChaCha20 with a seed of 0, stream of 0, beginning at position 0. 
    /// </summary>
    public static ImmutableArray<UInt32> ChaCha20Output1Kibibyte { get; } = new UInt32[]
    {
        0xade0b876, 0x903df1a0, 0xe56a5d40, 0x28bd8653, 0xb819d2bd, 0x1aed8da0, 0xccef36a8, 0xc70d778b,
        0x7c5941da, 0x8d485751, 0x3fe02477, 0x374ad8b8, 0xf4b8436a, 0x1ca11815, 0x69b687c3, 0x8665eeb2,
        0xbee7079f, 0x7a385155, 0x7c97ba98,  0xd082d73, 0xa0290fcb, 0x6965e348, 0x3e53c612, 0xed7aee32,
        0x7621b729, 0x434ee69c, 0xb03371d5, 0xd539d874, 0x281fed31, 0x45fb0a51, 0x1f0ae1ac, 0x6f4d794b,
        0xe6a0092d, 0xe16c2663,  0x8d17eae, 0x75a06819, 0x998e718e, 0xc662d37b, 0x3446c3b0, 0x5db3a0a9,
        0x68372701,  0xf5d7b1f, 0xfd3a1e28, 0x1ebc58e4, 0x13d3d273, 0xc094cfc9, 0x6271f35f, 0xf248a240,
        0x58a02013, 0x6b56b3d7, 0xaada20d5,  0xabfd23e, 0x20b1b8c5, 0x732785fb, 0x349763c3, 0xa4915cb4,
        0x83cbd42d, 0x2e0d84f8, 0x1358b1ed, 0x3fac6210, 0xfff82c1f, 0x5618cd6d, 0x6c1e6ae8, 0x7e166731,
        0x7488a6e5, 0xadc5472b, 0xdfd459fb, 0xb11dfd76, 0x3be01ee5, 0x2af8a91c, 0xdb3e17ca, 0x4793728b,
         0xf98be4e, 0xc9104d90, 0x472b4416, 0x84e9a083, 0xc9b60c86, 0x389cb357, 0xcf518fed, 0x4d8aa6fa,
        0xa32510e0, 0x4645509c,  0x614dcb9, 0x1528eba7, 0xd750511e, 0xa7ba04b2, 0x91f0d419, 0xdb171202,
        0xc8b5f15c, 0x1aa74f4c, 0xa1109687, 0x52ac95a6, 0x77565b7c, 0x218a6b4a, 0x8586e8aa, 0x4c098e86,
         0x9f49ef2,  0xca9f70a, 0x17887ec0, 0x638752aa, 0x333c7d79, 0x4bca672b, 0x2c6410c1, 0x47ec5121,
        0x8ccb84ee, 0x105fd842, 0x18cba8e2, 0x5f33b7c3, 0x9ac3e826, 0xc1bcb112, 0xb7777170, 0x2e733861,
        0x4db7aaed, 0xc00f41a1, 0x8c06ea55,  0xa26e999, 0xcf37e3cb, 0xe5003e5d, 0xfe0f23b3,  0x7990bdb,
         0xec7d087, 0x9841fe0b, 0xdd5867ea, 0x5ffb615a, 0x81f92dec, 0xe1ef1bf3, 0x171df853, 0xdb841716,
        0xd522881c, 0x7deed13c, 0x483632b5,  0x4f4bd28, 0xdca840b0, 0xd3f322c5, 0x4bec9ad9, 0xb8ed5780,
        0xa2310950,  0xc2f2dc4, 0x10470857, 0xda54570b, 0xb8bd5ffc, 0x1aefbb94, 0x7fa0e12d, 0xb9c4a08b,
        0x66103019, 0x6b05bced, 0x7a1e487b, 0x7b29460c, 0x9d9d58bb, 0xa675b6a5, 0x2e153e72, 0xcea4635e,
        0x839e4e03, 0x3a018ae5, 0x2f35e7f0, 0x148590b7,  0x4d1b3e3, 0x63b90b0d, 0x634b95b3, 0xbfd45f6b,
        0xbaad0a6d,  0x67d15f8, 0x1824cb2a, 0x75a476c1, 0xc3351b51, 0x568a21f6, 0xc65bea68, 0x82874bf5,
        0xf040b3f8, 0xbabec10a, 0x63cd625e, 0x80e77c2a,  0x856729c, 0xbfefa5ac, 0x37f2417c, 0xc0063f64,
        0x17077299, 0xf967e81d, 0x5ebf97d6, 0xbc1a01a6, 0xdb8c6cce, 0xd2941321, 0xfbd02dc0, 0x2c5adb60,
        0xc83dac17,  0xba97858, 0xdb0938ed, 0x54aa6eb9, 0xae8efc26, 0xc4652d0d,  0x89f472a, 0x2dbe4886,
        0x2ad801c8, 0xc0dd6f36, 0x634223ef, 0x7d41b6c0, 0x18a49d5f, 0x688db817, 0x9571e6e5, 0x30eec1c5,
        0xf221e895,  0xbb22425, 0x59eb1ce4, 0x1de41204, 0x3f8448c6, 0x7aecbfa9, 0xab61cf3d, 0x33574105,
        0x81fad316,  0x3936251, 0x564197fe, 0xdb65d02e, 0x5000bc4e, 0x648355ef, 0x4a1281ae, 0x13c0f528,
        0xbc2f2313, 0x8afd6d49, 0x7b656825, 0x14726d68,   0x1a2a38, 0xdd173090, 0x848769a9, 0xff5aba42,
        0x553f61f6, 0x3c23bb3c, 0xee9a6de4, 0x6c87a793, 0x29e8e9f5, 0xad8cb112, 0x2743b3f0, 0x7e42e0b2,
        0xceb766cf, 0x8d91c0b7, 0xf1df7bc4, 0xdf2a062a,  0x9301307, 0x5c5e7ace, 0x68017e91, 0xb7096130,
        0x3a6549cb, 0xf0ae2c6d, 0x3a78de05,  0x5fe9b9a, 0x34d11e38, 0x65ec948d,  0xb9c6f88, 0xc5529c61,
        0xb1003853, 0x7261836c, 0xdb8251b9, 0x42c0eec5, 0xf1229eb8, 0x735b081a, 0xcd11369a, 0x1860838d,
    }.ToImmutableArray();
}