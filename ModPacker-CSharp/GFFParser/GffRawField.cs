using System.Linq;

namespace ModPacker_CSharp.GFFParser
{
    public class GffRawField
    {
        public GffFieldType FieldType { get; set; }
        public uint LabelIndex { get; set; }
        public uint DataOrDataOffset { get; set; }


        public bool IsComplexType
        {
            get
            {
                GffFieldType[] complexTypes =
                {
                    GffFieldType.DWord64,
                    GffFieldType.Int64,
                    GffFieldType.Double,
                    GffFieldType.CExoString,
                    GffFieldType.ResRef,
                    GffFieldType.CExoLocString,
                    GffFieldType.Void,
                    GffFieldType.Struct,
                    GffFieldType.List
                };

                return complexTypes.Contains(FieldType);
            }
        }

    }
}
