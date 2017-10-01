using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public GffStruct ToGff()
        {
            GffStruct gff = new GffStruct();
            gff.Add("AutoRemoveKey", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(AutoRemoveKey) });
            gff.Add("Cursor", new GffField(GffFieldType.Byte) { ByteValue = Cursor });
            gff.Add("DisarmDC", new GffField(GffFieldType.Byte) { ByteValue = DisarmDC });
            gff.Add("Faction", new GffField(GffFieldType.DWord) { DWordValue = FactionID});
            gff.Add("HighlightHeight", new GffField(GffFieldType.Float) { FloatValue = HighlightHeight});
            gff.Add("KeyName", new GffField(GffFieldType.CExoString) { StringValue = KeyName });
            gff.Add("LinkedTo", new GffField(GffFieldType.CExoString) { StringValue = LinkedTo});
            gff.Add("LinkedToFlags", new GffField(GffFieldType.Byte) { ByteValue = (byte)LinkedToType});
            gff.Add("LoadScreenID", new GffField(GffFieldType.Word) { WordValue = LoadScreenID });

            GffField tempField = new GffField(GffFieldType.CExoLocString);
            tempField.LocalizedStrings.Add(LocalizedName);
            gff.Add("LocalizedName", tempField);
            
            gff.Add("OnClick", new GffField(GffFieldType.ResRef) { ResrefValue = OnClick });
            gff.Add("OnDisarm", new GffField(GffFieldType.ResRef) { ResrefValue = OnDisarm});
            gff.Add("OnTrapTriggered", new GffField(GffFieldType.ResRef) { ResrefValue = OnTrapTriggered});
            gff.Add("PortraitId", new GffField(GffFieldType.Word) { WordValue = PortraitID });
            gff.Add("ScriptHeartbeat", new GffField(GffFieldType.ResRef) { ResrefValue = OnHeartbeat});
            gff.Add("ScriptOnEnter", new GffField(GffFieldType.ResRef) { ResrefValue = OnEnter });
            gff.Add("ScriptOnExit", new GffField(GffFieldType.ResRef) { ResrefValue = OnExit });
            gff.Add("ScriptUserDefine", new GffField(GffFieldType.ResRef) { ResrefValue = OnUserDefined});
            gff.Add("Tag", new GffField(GffFieldType.CExoString) { StringValue = Tag });
            gff.Add("TemplateResRef", new GffField(GffFieldType.ResRef) { ResrefValue = TemplateResref});
            gff.Add("TrapDetectable", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(TrapIsDetectable)});
            gff.Add("TrapDetectDC", new GffField(GffFieldType.Byte) { ByteValue = TrapDetectDC});
            gff.Add("TrapDisarmable", new GffField(GffFieldType.Byte) { ByteValue = TrapDisarmable});
            gff.Add("TrapFlag", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsTrap)});
            gff.Add("TrapOneShot", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsTrapOneShot)});
            gff.Add("TrapType", new GffField(GffFieldType.Byte) { ByteValue = TrapTypeID});
            gff.Add("Type", new GffField(GffFieldType.Int) { IntValue = (int)TriggerType});
            gff.Add("PaletteID", new GffField(GffFieldType.Byte) { ByteValue = PaletteID});

            if (Geometry.Count > 0)
            {
                GffField geometry = new GffField(GffFieldType.List);
                foreach (NWPoint point in Geometry)
                {
                    GffStruct @struct = new GffStruct
                    {
                        {"PointX", new GffField(GffFieldType.Float) {FloatValue = point.X}},
                        {"PointY", new GffField(GffFieldType.Float) {FloatValue = point.Y}},
                        {"PointZ", new GffField(GffFieldType.Float) {FloatValue = point.Z}}
                    };

                    geometry.ListValue.Add(@struct);
                }

                gff.Add("Geometry", geometry);
            }

            return gff;
        }
    }
}
