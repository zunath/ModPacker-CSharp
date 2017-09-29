namespace ModPacker_CSharp.GFFParser
{
    public class Gff
    {
        public string Resref { get; set; }
        public GffResourceType ResourceType { get; set; }
        public GffStruct RootStruct { get; set; }

        public Gff()
        {
            Resref = string.Empty;
            ResourceType = GffResourceType.Invalid;
        }
    }
}
