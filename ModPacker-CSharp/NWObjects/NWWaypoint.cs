using System;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWWaypoint
    {
        public byte AppearanceID { get; set; }
        public NWLocalizedString Description { get; set; }
        public bool HasMapNote { get; set; }
        public string LinkedTo { get; set; }
        public NWLocalizedString LocalizedName { get; set; }
        public NWLocalizedString MapNote { get; set; }
        public bool IsMapNoteEnabled { get; set; }
        public string Tag { get; set; }
        public string Label { get; set; }
        public string Resref { get; set; }
        public int StrRef { get; set; }
        public string Comment { get; set; }
        public byte PaletteID { get; set; }
        public string TemplateResRef { get; set; }
        public float XOrientation { get; set; }
        public float YOrientation { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float ZPosition { get; set; }

        public static NWWaypoint FromGff(GffStruct source)
        {
            NWWaypoint waypoint = new NWWaypoint();
            waypoint.AppearanceID = source["Appearance"].ByteValue;
            waypoint.Description = source["Description"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["Description"].LocalizedStrings[0];
            waypoint.HasMapNote = Convert.ToBoolean(source["HasMapNote"].ByteValue);
            waypoint.LinkedTo = source["LinkedTo"].StringValue;
            waypoint.LocalizedName = source["LocalizedName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["LocalizedName"].LocalizedStrings[0];
            waypoint.MapNote = source["MapNote"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["MapNote"].LocalizedStrings[0];
            waypoint.IsMapNoteEnabled = Convert.ToBoolean(source["MapNoteEnabled"].ByteValue);
            waypoint.Tag = source["Tag"].StringValue;
            waypoint.Comment = source.ContainsKey("Comment") ? source["Comment"].StringValue : string.Empty;
            waypoint.PaletteID = source.ContainsKey("PaletteID") ? source["PaletteID"].ByteValue : (byte)0;
            waypoint.TemplateResRef = source["TemplateResRef"].ResrefValue;
            waypoint.XOrientation = source.ContainsKey("XOrientation") ? source["XOrientation"].FloatValue : 0.0f;
            waypoint.YOrientation = source.ContainsKey("YOrientation") ? source["YOrientation"].FloatValue : 0.0f;
            waypoint.XPosition = source.ContainsKey("XPosition") ? source["XPosition"].FloatValue : 0.0f;
            waypoint.YPosition = source.ContainsKey("YPosition") ? source["YPosition"].FloatValue : 0.0f;
            waypoint.ZPosition = source.ContainsKey("ZPosition") ? source["ZPosition"].FloatValue : 0.0f;

            return waypoint;
        }

        public GffStruct ToGff()
        {
            GffStruct gff = new GffStruct();
            gff.Add("Appearance", new GffField { ByteValue = AppearanceID });

            GffField tempField = new GffField();
            tempField.LocalizedStrings.Add(Description);
            gff.Add("Description", tempField);

            gff.Add("HasMapNote", new GffField { ByteValue = Convert.ToByte(HasMapNote) });
            gff.Add("LinkedTo", new GffField { StringValue = LinkedTo });

            tempField = new GffField();
            tempField.LocalizedStrings.Add(LocalizedName);
            gff.Add("LocalizedName", tempField);

            tempField = new GffField();
            tempField.LocalizedStrings.Add(MapNote);
            gff.Add("MapNote", tempField);
            
            gff.Add("MapNoteEnabled", new GffField { ByteValue = Convert.ToByte(IsMapNoteEnabled) });
            gff.Add("Tag", new GffField { StringValue = Tag });
            gff.Add("Comment", new GffField { StringValue = Comment });
            gff.Add("PaletteID", new GffField { ByteValue = PaletteID });
            gff.Add("TemplateResRef", new GffField { ResrefValue = TemplateResRef });
            gff.Add("XOrientation", new GffField { FloatValue = XOrientation });
            gff.Add("YOrientation", new GffField { FloatValue = YOrientation });
            gff.Add("XPosition", new GffField { FloatValue = XPosition });
            gff.Add("YPosition", new GffField { FloatValue = YPosition });
            gff.Add("ZPosition", new GffField { FloatValue = ZPosition });

            return gff;
        }
    }
}
