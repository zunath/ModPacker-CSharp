using System;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWPlaceable: NWSituatedObject
    {
        public byte BodyBagID { get; set; }
        public bool HasInventory { get; set; }
        public string OnInventoryDisturbed { get; set; }
        public string OnUsed { get; set; }
        public bool IsStatic { get; set; }
        public bool IsUseable { get; set; }
        
        public static NWPlaceable FromGff(GffStruct source)
        {
            NWPlaceable placeable = new NWPlaceable();
            placeable = (NWPlaceable)placeable.SharedFieldsFromGff(source);

            placeable.BodyBagID = source["BodyBag"].ByteValue;
            placeable.HasInventory = Convert.ToBoolean(source["HasInventory"].ByteValue);
            placeable.OnInventoryDisturbed = source["OnInvDisturbed"].ResrefValue;
            placeable.OnUsed = source["OnUsed"].ResrefValue;
            placeable.IsStatic = Convert.ToBoolean(source["Static"].ByteValue);
            placeable.IsUseable = Convert.ToBoolean(source["Useable"].ByteValue);

            // TODO: ItemList loading

            return placeable;
        }

        public GffStruct ToGff()
        {
            GffStruct gff = SharedFieldsToGff();
            gff.Add("BodyBag", new GffField { ByteValue = BodyBagID });
            gff.Add("HasInventory", new GffField { ByteValue = Convert.ToByte(HasInventory)});
            gff.Add("OnInvDisturbed", new GffField { ResrefValue = OnInventoryDisturbed });
            gff.Add("OnUsed", new GffField { ResrefValue = OnUsed });
            gff.Add("Static", new GffField { ByteValue = Convert.ToByte(IsStatic) });
            gff.Add("Useable", new GffField { ByteValue = Convert.ToByte(IsUseable) });

            // TODO: ItemList

            return gff;
        }
    }
}
