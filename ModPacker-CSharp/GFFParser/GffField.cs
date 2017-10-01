using System.Collections.Generic;
using ModPacker_CSharp.NWObjects;

namespace ModPacker_CSharp.GFFParser
{
    public class GffField
    {
        public GffFieldType FieldType { get; set; }
        public GffStruct Parent { get; set; }
        public string Label { get; set; }

        public byte ByteValue { get; set; }
        public char CharValue { get; set; }
        public ushort WordValue { get; set; }
        public short ShortValue { get; set; }
        public uint DWordValue { get; set; }
        public int IntValue { get; set; }
        public float FloatValue { get; set; }
        public long Int64Value { get; set; }
        public ulong DWord64Value { get; set; }
        public double DoubleValue { get; set; }
        public string ResrefValue { get; set; }
        public byte[] VoidDataValue { get; set; }
        public string StringValue { get; set; }
        public List<NWLocalizedString> LocalizedStrings { get; set; }
        public GffStruct StructValue { get; set; }
        public List<GffStruct> ListValue { get; set; } 

        public GffField(GffFieldType fieldType)
        {
            FieldType = fieldType;
            LocalizedStrings = new List<NWLocalizedString>();
            ListValue = new List<GffStruct>();
        }

    }
}
