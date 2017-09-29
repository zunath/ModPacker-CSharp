
namespace ModPacker_CSharp.NWObjects
{
    // CExoLocString
    public class NWLocalizedString
    {
        public int LanguageID { get; set; }
        public string Text { get; set; }

        public NWLocalizedString()
        {
            LanguageID = 0;
            Text = string.Empty;
        }
    }
}
