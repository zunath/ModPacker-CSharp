namespace ModPacker_CSharp.GFFParser
{
    public enum GffFieldType: uint
    {
        Byte = 0,
        Char = 1,
        Word = 2,
        Short = 3,
        DWord = 4,
        Int = 5,
        DWord64 = 6,
        Int64 = 7,
        Float = 8,
        Double = 9,
        CExoString = 10,
        ResRef = 11,
        CExoLocString = 12,
        Void = 13,
        Struct = 14,
        List = 15
    }
}
