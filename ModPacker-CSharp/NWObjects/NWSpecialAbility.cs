using ModPacker_CSharp.Enums;

namespace ModPacker_CSharp.NWObjects
{
    public class NWSpecialAbility
    {
        public ushort SpellID { get; set; }
        public byte SpellCasterLevel { get; set; }
        public SpellFlagType SpellFlags { get; set; }
    }
}
