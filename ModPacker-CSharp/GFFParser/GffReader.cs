using System.Collections.Generic;
using System.IO;
using System.Text;
using ModPacker_CSharp.NWObjects;

namespace ModPacker_CSharp.GFFParser
{
    public class GffReader
    {
        private uint _structCount;
        private uint _fieldCount;
        private uint _labelCount;
        private uint _fieldDataOffset;
        private uint _fieldIndicesOffset;
        private uint _listIndicesOffset;
        private readonly List<GffRawStruct> _structs;
        private readonly List<GffRawField> _fields;
        private readonly List<string> _labels; 
        private Gff _result;

        BinaryReader _reader;

        public GffReader()
        {
            _structs = new List<GffRawStruct>();
            _fields = new List<GffRawField>();
            _labels = new List<string>();
        }

        public Gff LoadGff(GffResource resource)
        {
            _result = new Gff
            {
                ResourceType = resource.ResourceType,
                Resref = resource.Resref
            };
            _reader = new BinaryReader(new MemoryStream(resource.Data));
            
            ReadHeader();
            ReadRawStructs();
            ReadRawFields();
            ReadLabels();
            ProcessStructs();

            return _result;
        }
        
        private void ReadHeader()
        {
            _reader.ReadBytes(4); // FileType
            _reader.ReadBytes(4); // FileVersion
            _reader.ReadUInt32(); // StructOffset
            _structCount = _reader.ReadUInt32();
            _reader.ReadUInt32(); // FieldOffset
            _fieldCount = _reader.ReadUInt32();
            _reader.ReadUInt32(); // LabelOffset
            _labelCount = _reader.ReadUInt32();
            _fieldDataOffset = _reader.ReadUInt32();
            _reader.ReadUInt32(); // FieldDataCount
            _fieldIndicesOffset = _reader.ReadUInt32();
            _reader.ReadUInt32(); // FieldIndicesCount
            _listIndicesOffset = _reader.ReadUInt32(); 
            _reader.ReadUInt32(); // ListIndicesCount
        }

        private void ReadRawStructs()
        {
            for(int i = 0; i < _structCount; i++)
            {
                _structs.Add(ReadSingleRawStruct());
            }
        }

        private GffRawStruct ReadSingleRawStruct()
        {
            return new GffRawStruct
            {
                StructType = _reader.ReadUInt32(),
                DataOrDataOffset = _reader.ReadUInt32(),
                FieldCount = _reader.ReadUInt32()
            };
        }

        private void ReadRawFields()
        {
            for(int i = 0; i < _fieldCount; i++)
            {
                GffRawField field = new GffRawField
                {
                    FieldType = (GffFieldType) _reader.ReadUInt32(),
                    LabelIndex = _reader.ReadUInt32(),
                    DataOrDataOffset = _reader.ReadUInt32()
                };

                _fields.Add(field);
            }
        }

        private void ReadLabels()
        {
            for(uint i = 0; i < _labelCount; i++)
            {
                _labels.Add(Encoding.UTF8.GetString(_reader.ReadBytes(16)).TrimEnd((char)0));
            }
        }
        
        private void ProcessStructs()
        {
            GffRawStruct rawStruct = _structs[0];
            _result.RootStruct = ProcessSingleStruct(rawStruct);
        }

        private GffStruct ProcessSingleStruct(GffRawStruct rawStruct)
        {
            GffStruct @struct = new GffStruct();

            if (rawStruct.FieldCount == 1)
            {
                GffRawField rawField = _fields[(int)rawStruct.DataOrDataOffset];
                string label = _labels[(int)rawField.LabelIndex];
                @struct[label] = ProcessField(rawField, @struct);
            }
            else if(rawStruct.FieldCount > 0)
            {
                _reader.BaseStream.Seek(_fieldIndicesOffset + rawStruct.DataOrDataOffset, SeekOrigin.Begin);

                for (int x = 0; x < rawStruct.FieldCount; x++)
                {
                    uint fieldIndex = _reader.ReadUInt32();
                    GffRawField rawField = _fields[(int)fieldIndex];
                    string label = _labels[(int) rawField.LabelIndex];
                    @struct[label] = ProcessField(rawField, @struct);
                }
            }

            return @struct;
        }
        
        private GffField ProcessField(GffRawField rawField, GffStruct parent)
        {
            GffRawStruct rawStruct;
            long backupPosition = _reader.BaseStream.Position;

            if (rawField.IsComplexType)
            {
                _reader.BaseStream.Seek(_fieldDataOffset + rawField.DataOrDataOffset, SeekOrigin.Begin);
            }

            GffField field = new GffField(rawField.FieldType)
            {
                Parent = parent,
                Label = _labels[(int)rawField.LabelIndex]
            };

            switch (rawField.FieldType)
            {
                case GffFieldType.Byte:
                    field.ByteValue = (byte) rawField.DataOrDataOffset;
                    break;
                case GffFieldType.Char:
                    field.CharValue = (char) rawField.DataOrDataOffset;
                    break;
                case GffFieldType.Word:
                    field.WordValue = (ushort) rawField.DataOrDataOffset;
                    break;
                case GffFieldType.Short:
                    field.ShortValue = (short) rawField.DataOrDataOffset;
                    break;
                case GffFieldType.DWord:
                    field.DWordValue = rawField.DataOrDataOffset;
                    break;
                case GffFieldType.Int:
                    field.IntValue = (int)rawField.DataOrDataOffset;
                    break;
                case GffFieldType.Float:
                    field.FloatValue = rawField.DataOrDataOffset;
                    break;
                case GffFieldType.DWord64:
                    field.DWord64Value = _reader.ReadUInt64();
                    break;
                case GffFieldType.Int64:
                    field.Int64Value = _reader.ReadInt64();
                    break;
                case GffFieldType.Double:
                    field.DoubleValue = _reader.ReadDouble();
                    break;
                case GffFieldType.CExoString:
                    uint stringSize = _reader.ReadUInt32();
                    byte[] stringData = new byte[stringSize];

                    for (uint i = 0; i < stringSize; i++)
                    {
                        stringData[i] = _reader.ReadByte();
                    }
                    field.StringValue = Encoding.UTF8.GetString(stringData);

                    break;
                case GffFieldType.ResRef:
                    byte resrefStringSize = _reader.ReadByte();
                    field.ResrefValue = Encoding.UTF8.GetString(_reader.ReadBytes(resrefStringSize));
                    break;
                case GffFieldType.CExoLocString:
                    _reader.ReadUInt32(); // TotalSize
                    _reader.ReadUInt32(); // StringRef
                    uint stringCount = _reader.ReadUInt32();

                    for (uint i = 0; i < stringCount; i++)
                    {
                        NWLocalizedString locString = new NWLocalizedString
                        {
                            LanguageID = _reader.ReadInt32()
                        };

                        int stringLength = _reader.ReadInt32();
                        byte[] stringBytes = _reader.ReadBytes(stringLength);
                        locString.Text = Encoding.UTF8.GetString(stringBytes);

                        field.LocalizedStrings.Add(locString);
                    }
                    break;
                case GffFieldType.Void:
                    uint size = _reader.ReadUInt32();
                    byte[] data = new byte[size];
                    for (uint i = 0; i < size; i++)
                    {
                        data[i] = _reader.ReadByte();
                    }
                    field.VoidDataValue = data;
                    break;
                case GffFieldType.Struct:
                    rawStruct = _structs[(int)rawField.DataOrDataOffset];
                    field.StructValue = ProcessSingleStruct(rawStruct);
                    break;
                case GffFieldType.List:
                    _reader.BaseStream.Seek(_listIndicesOffset + rawField.DataOrDataOffset, SeekOrigin.Begin);
                    uint listSize = _reader.ReadUInt32();

                    for (uint i = 0; i < listSize; i++)
                    {
                        uint value = _reader.ReadUInt32();
                        long listPosition = _reader.BaseStream.Position;
                        rawStruct = _structs[(int) value];
                        GffStruct listStruct = ProcessSingleStruct(rawStruct);

                        field.ListValue.Add(listStruct);
                        _reader.BaseStream.Position = listPosition;
                    }

                    break;
            }

            _reader.BaseStream.Position = backupPosition;
            return field;
        }

    }
}
