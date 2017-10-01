using System.Collections.Generic;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp
{
    public static class TempStaticStorage
    {
        public static List<Gff> ReaderGffList = new List<Gff>();
        public static List<Gff> WriterGffList = new List<Gff>();
    }
}
