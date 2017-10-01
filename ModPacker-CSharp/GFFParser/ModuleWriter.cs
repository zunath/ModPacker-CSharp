using System.Collections.Generic;
using System.IO;
using ModPacker_CSharp.NWObjects;

namespace ModPacker_CSharp.GFFParser
{
    public class ModuleWriter
    {
        private BinaryWriter _writer;
        private NWModule _module;
        private List<Gff> _gffList;

        public void SaveModule(Stream outStream, NWModule module)
        {
            _writer = new BinaryWriter(outStream);
            _module = module;
            _gffList = _module.ToGff();

            // TODO: DEBUGGING

            TempStaticStorage.WriterGffList = _gffList;

            // TODO: END DEBUGGING

            WriteHeader();
            WriteStrings();
            WriteResourceIndices();
            WriteResourceList();
            WriteResourceData();
        }

        private void WriteHeader()
        {
            _writer.Write("MOD "); // FileType
            _writer.Write("V1.0"); // Version

            // Module description
            if (_module.Description == null)
            {
                _writer.Write(0); // Zero strings
                _writer.Write(0); // Zero size block
            }
            else
            {
                _writer.Write(1); // String count
                int stringSize = 0x8 + _module.Description.Text.Length;
                _writer.Write(stringSize);
            }
            

        }

        private void WriteStrings()
        {
            
        }

        private void WriteResourceIndices()
        {
            
        }

        private void WriteResourceList()
        {
            
        }

        private void WriteResourceData()
        {
            
        }


    }
}
