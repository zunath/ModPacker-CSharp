using System;
using System.Collections.Generic;
using ModPacker_CSharp.Enums;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWTrigger
    {
        public bool AutoRemoveKey { get; set; }
        public byte Cursor { get; set; }
        public byte DisarmDC { get; set; }
        public uint FactionID { get; set; }
        public float HighlightHeight { get; set; }
        public string KeyName { get; set; }
        public string LinkedTo { get; set; }
        public TransitionLinkType LinkedToType { get; set; }
        public ushort LoadScreenID { get; set; }
        public NWLocalizedString LocalizedName { get; set; }
        public string OnClick { get; set; }
        public string OnDisarm { get; set; }
        public string OnTrapTriggered { get; set; }
        public ushort PortraitID { get; set; }
        public string OnHeartbeat { get; set; }
        public string OnEnter { get; set; }
        public string OnExit { get; set; }
        public string OnUserDefined { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public bool TrapIsDetectable { get; set; }
        public byte TrapDetectDC { get; set; }
        public byte TrapDisarmable { get; set; }
        public bool IsTrap { get; set; }
        public bool IsTrapOneShot { get; set; }
        public byte TrapTypeID { get; set; }
        public TriggerType TriggerType { get; set; }

        public string Comment { get; set; }
        public byte PaletteID { get; set; }
        public string TemplateResref { get; set; }
        public List<NWPoint> Geometry { get; set; } 
        public float XOrientation { get; set; }
        public float YOrientation { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float ZPosition { get; set; }

        public NWTrigger()
        {
            Geometry = new List<NWPoint>();
        }

        public static NWTrigger FromGff(GffStruct source)
        {
            NWTrigger trigger = new NWTrigger();
            trigger.AutoRemoveKey = Convert.ToBoolean(source["AutoRemoveKey"].ByteValue);
            trigger.Cursor = source["Cursor"].ByteValue;
            trigger.DisarmDC = source["DisarmDC"].ByteValue;
            trigger.FactionID = source["Faction"].DWordValue;
            trigger.HighlightHeight = source["HighlightHeight"].FloatValue;
            trigger.KeyName = source["KeyName"].StringValue;
            trigger.LinkedTo = source["LinkedTo"].StringValue;
            trigger.LinkedToType = (TransitionLinkType)source["LinkedToFlags"].ByteValue;
            trigger.LoadScreenID = source["LoadScreenID"].WordValue;
            trigger.LocalizedName = source["LocalizedName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["LocalizedName"].LocalizedStrings[0];
            trigger.OnClick = source["OnClick"].ResrefValue;
            trigger.OnDisarm = source["OnDisarm"].ResrefValue;
            trigger.OnTrapTriggered = source["OnTrapTriggered"].ResrefValue;
            trigger.PortraitID = source["PortraitId"].WordValue;
            trigger.OnHeartbeat = source["ScriptHeartbeat"].ResrefValue;
            trigger.OnEnter = source["ScriptOnEnter"].ResrefValue;
            trigger.OnExit = source["ScriptOnExit"].ResrefValue;
            trigger.OnUserDefined = source["ScriptUserDefine"].ResrefValue;
            trigger.Tag = source["Tag"].StringValue;
            trigger.TemplateResref = source["TemplateResRef"].ResrefValue;
            trigger.TrapIsDetectable = Convert.ToBoolean(source["TrapDetectable"].ByteValue);
            trigger.TrapDetectDC = source["TrapDetectDC"].ByteValue;
            trigger.TrapDisarmable = source["TrapDisarmable"].ByteValue;
            trigger.IsTrap = Convert.ToBoolean(source["TrapFlag"].ByteValue);
            trigger.IsTrapOneShot = Convert.ToBoolean(source["TrapOneShot"].ByteValue);
            trigger.TrapTypeID = source["TrapType"].ByteValue;
            trigger.TriggerType = (TriggerType)source["Type"].IntValue;

            trigger.PaletteID = source.ContainsKey("PaletteID") ? source["PaletteID"].ByteValue : (byte)0;
            trigger.TemplateResref = source["TemplateResRef"].ResrefValue;

            if (source.ContainsKey("Geometry"))
            {
                foreach (GffStruct @struct in source["Geometry"].ListValue)
                {
                    trigger.Geometry.Add(
                        new NWPoint(@struct["PointX"].FloatValue,
                                    @struct["PointY"].FloatValue,
                                    @struct["PointZ"].FloatValue)
                    );
                }
            }

            return trigger;
        }
    }
}
