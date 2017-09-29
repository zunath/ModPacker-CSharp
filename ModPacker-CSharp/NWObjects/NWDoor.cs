using ModPacker_CSharp.Enums;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWDoor: NWSituatedObject
    {
        public byte GenericTypeID { get; set; }
        public string LinkedTo { get; set; }
        public TransitionLinkType LinkType { get; set; }
        public ushort LoadScreenID { get; set; }
        public string OnClick { get; set; }
        public string OnFailToOpen { get; set; }

        public static NWDoor FromGff(GffStruct source)
        {
            NWDoor door = new NWDoor();
            door = (NWDoor)door.SharedFieldsFromGff(source);

            door.GenericTypeID = source.ContainsKey("GenericType") ? source["GenericType"].ByteValue : (byte)0;
            door.LinkedTo = source["LinkedTo"].StringValue;
            door.LinkType = (TransitionLinkType) source["LinkedToFlags"].ByteValue;
            door.LoadScreenID = source["LoadScreenID"].WordValue;
            door.OnClick = source["OnClick"].ResrefValue;
            door.OnFailToOpen = source["OnFailToOpen"].ResrefValue;

            return door;
        }
    }
}
