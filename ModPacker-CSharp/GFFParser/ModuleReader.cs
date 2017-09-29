using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ModPacker_CSharp.NWObjects;

namespace ModPacker_CSharp.GFFParser
{
    public class ModuleReader
    {
        private BinaryReader _reader;
        private int _languageCount;
        private int _entryCount;
        private int _offsetToResourceList;
        private readonly List<NWLocalizedString> _localizedModuleDescriptions;
        private readonly List<GffResource> _resources;
        private NWModule _module;

        public ModuleReader()
        {
            _localizedModuleDescriptions = new List<NWLocalizedString>();
            _resources = new List<GffResource>();
        }

        public NWModule LoadModule(FileStream stream)
        {
            _reader = new BinaryReader(stream);

            ReadHeader();
            ReadStrings();
            ReadResourceIndices();
            ReadResourceList();
            ReadResourceData();
            
            return _module;
        }

        private void ReadHeader()
        {
            _reader.ReadBytes(4); // FileType
            _reader.ReadBytes(4); // Version
            _languageCount = _reader.ReadInt32();
            _reader.ReadInt32(); // LocalizedStringSize
            _entryCount = _reader.ReadInt32();
            _reader.ReadInt32(); // OffsetToLocalizedString
            _reader.ReadInt32(); // OffsetToKeyList
            _offsetToResourceList = _reader.ReadInt32();
            _reader.ReadInt32(); // BuildYear
            _reader.ReadInt32(); // BuildDay
            _reader.ReadInt32(); // DescriptionStrRef
            _reader.ReadBytes(116); // Bioware Reserved
        }

        private void ReadStrings()
        {
            for(int i = 0; i < _languageCount; i++)
            {
                NWLocalizedString locString = new NWLocalizedString();
                locString.LanguageID = _reader.ReadInt32();
                int stringSize = _reader.ReadInt32();
                locString.Text = Encoding.UTF8.GetString(_reader.ReadBytes(stringSize));

                _localizedModuleDescriptions.Add(locString);
            }
        }

        private void ReadResourceIndices()
        {
            for(int i = 0; i < _entryCount; i++)
            {
                GffResource resource = new GffResource();
                resource.Resref = Encoding.UTF8.GetString(_reader.ReadBytes(16)).TrimEnd(new char[] { (char)0 });
                resource.ResourceID = _reader.ReadInt32();
                resource.ResourceType = (GffResourceType)_reader.ReadInt16();
                _reader.ReadInt16(); // Unused by Bioware

                _resources.Add(resource);
            }
        }

        private void ReadResourceList()
        {
            _reader.BaseStream.Seek(_offsetToResourceList, SeekOrigin.Begin);

            for (int i = 0; i < _entryCount; i++)
            {
                _resources[i].OffsetToResource = _reader.ReadInt32();
                _resources[i].ResourceSize = _reader.ReadInt32();
            }
        }
        
        private void ReadResourceData()
        {
            GffResourceType[] validTypes = {
                GffResourceType.IFO, GffResourceType.ARE, GffResourceType.GIC,
                GffResourceType.GIT, GffResourceType.UTC, GffResourceType.UTD,
                GffResourceType.UTE, GffResourceType.UTI, GffResourceType.UTP,
                GffResourceType.UTS, GffResourceType.UTM, GffResourceType.UTT,
                GffResourceType.UTW, GffResourceType.DLG, GffResourceType.JRL,
                GffResourceType.FAC, GffResourceType.ITP, GffResourceType.PTM,
                GffResourceType.PTT, GffResourceType.BIC };

            List<Gff> gffRecords = new List<Gff>();

            foreach(var resource in _resources)
            {
                resource.Data = _reader.ReadBytes(resource.ResourceSize);

                if(validTypes.Contains(resource.ResourceType))
                {
                    GffReader gffReader = new GffReader();

                    gffRecords.Add(gffReader.LoadGff(resource));
                }

            }

            _module = NWModule.FromGff(gffRecords);

        }

    }
}
