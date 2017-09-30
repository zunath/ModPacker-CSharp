using System.IO;
using ModPacker_CSharp.NWObjects;

namespace ModPacker_CSharp.GFFParser
{
    public class ModuleWriter
    {
        private BinaryWriter _writer;
        private NWModule _module;

        public void SaveModule(FileStream stream, NWModule module)
        {
            _writer = new BinaryWriter(stream);
            _module = module;

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
