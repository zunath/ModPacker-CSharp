namespace ModPacker_CSharp.GFFParser
{
    public class GffResource
    {
        public string Resref { get; set; }
        public int ResourceID { get; set; }
        public GffResourceType ResourceType { get; set; }
        public int OffsetToResource { get; set; }
        public int ResourceSize { get; set; }
        public byte[] Data { get; set; }
    }
}
