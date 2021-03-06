﻿using System;
using System.Collections.Generic;
using ModPacker_CSharp.Enums;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWSound
    {
        public bool IsActive { get; set; }
        public bool IsContinuous { get; set; }
        public float Elevation { get; set; }
        public uint Hours { get; set; }
        public uint Interval { get; set; }
        public uint IntervalVariation { get; set; }
        public NWLocalizedString LocalizedName { get; set; }
        public bool IsLooping { get; set; }
        public float MaxDistance { get; set; }
        public float MinDistance { get; set; }
        public float PitchVariation { get; set; }
        public bool IsPositional { get; set; }
        public byte Priority { get; set; }
        public bool IsPlaylistRandom { get; set; }
        public bool HasRandomPosition { get; set; }
        public float RandomRangeX { get; set; }
        public float RandomRangeY { get; set; }
        public List<string> Sounds { get; set; } 
        public string Tag { get; set; }
        public TimeOfDayType TimeOfDay { get; set; }
        public byte Volume { get; set; }
        public byte VolumeVariation { get; set; }
        
        public string Comment { get; set; }
        public byte PaletteID { get; set; }
        public string TemplateResRef { get; set; }
        public bool IsGenerated { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float ZPosition { get; set; }

        public NWSound()
        {
            Sounds = new List<string>();
        }

        public static NWSound FromGff(GffStruct source)
        {
            NWSound sound = new NWSound();
            sound.IsActive = Convert.ToBoolean(source["Active"].ByteValue);
            sound.IsContinuous = Convert.ToBoolean(source["Continuous"].ByteValue);
            sound.Elevation = source["Elevation"].FloatValue;
            sound.Hours = source["Hours"].DWordValue;
            sound.Interval = source["Interval"].DWordValue;
            sound.IntervalVariation = source["IntervalVrtn"].DWordValue;
            sound.LocalizedName = source["LocName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["LocName"].LocalizedStrings[0];
            sound.IsLooping = Convert.ToBoolean(source["Looping"].ByteValue);
            sound.MaxDistance = source["MaxDistance"].FloatValue;
            sound.MinDistance = source["MinDistance"].FloatValue;
            sound.PitchVariation = source["PitchVariation"].FloatValue;
            sound.IsPositional = Convert.ToBoolean(source["Positional"].ByteValue);
            sound.Priority = source["Priority"].ByteValue;
            sound.IsPlaylistRandom = Convert.ToBoolean(source["Random"].ByteValue);
            sound.HasRandomPosition = Convert.ToBoolean(source["RandomPosition"].ByteValue);
            sound.RandomRangeX = source["RandomRangeX"].FloatValue;
            sound.RandomRangeY = source["RandomRangeY"].FloatValue;
            sound.Tag = source["Tag"].StringValue;
            sound.TemplateResRef = source["TemplateResRef"].ResrefValue;
            sound.TimeOfDay = (TimeOfDayType)source["Times"].ByteValue;
            sound.Volume = source["Volume"].ByteValue;
            sound.VolumeVariation = source["VolumeVrtn"].ByteValue;
            sound.Comment = source.ContainsKey("Comment") ? source["Comment"].StringValue : string.Empty;
            sound.PaletteID = source.ContainsKey("PaletteID") ? source["PaletteID"].ByteValue : (byte)0;
            sound.IsGenerated = source.ContainsKey("GeneratedType") && Convert.ToBoolean(source["GeneratedType"].ByteValue);
            sound.XPosition = source.ContainsKey("XPosition") ? source["XPosition"].FloatValue : 0.0f;
            sound.YPosition = source.ContainsKey("YPosition") ? source["YPosition"].FloatValue : 0.0f;
            sound.ZPosition = source.ContainsKey("ZPosition") ? source["ZPosition"].FloatValue : 0.0f;


            foreach (GffStruct @struct in source["Sounds"].ListValue)
            {
                sound.Sounds.Add(@struct["Sound"].ResrefValue);
            }

            return sound;
        }

        public GffStruct ToGff()
        {
            GffStruct gff = new GffStruct();

            gff.Add("Active", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsActive) });
            gff.Add("Continuous", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsContinuous) });
            gff.Add("Elevation", new GffField(GffFieldType.Float) { FloatValue = Elevation });
            gff.Add("Hours", new GffField(GffFieldType.DWord) { DWordValue = Hours });
            gff.Add("Interval", new GffField(GffFieldType.DWord) { DWordValue = Interval });
            gff.Add("IntervalVrtn", new GffField(GffFieldType.DWord) { DWordValue = IntervalVariation });
            
            GffField tempField = new GffField(GffFieldType.CExoLocString);
            tempField.LocalizedStrings.Add(LocalizedName);
            gff.Add("LocName", tempField);

            gff.Add("Looping", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsLooping) });
            gff.Add("MaxDistance", new GffField(GffFieldType.Float) { FloatValue = MaxDistance });
            gff.Add("MinDistance", new GffField(GffFieldType.Float) { FloatValue = MinDistance });
            gff.Add("PitchVariation", new GffField(GffFieldType.Float) { FloatValue = PitchVariation });
            gff.Add("Positional", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsPositional) });
            gff.Add("Priority", new GffField(GffFieldType.Byte) { ByteValue = Priority });
            gff.Add("Random", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsPlaylistRandom) });
            gff.Add("RandomPosition", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(HasRandomPosition) });
            gff.Add("RandomRangeX", new GffField(GffFieldType.Float) { FloatValue = RandomRangeX });
            gff.Add("RandomRangeY", new GffField(GffFieldType.Float) { FloatValue = RandomRangeY });
            gff.Add("Tag", new GffField(GffFieldType.CExoString) { StringValue = Tag });
            gff.Add("TemplateResRef", new GffField(GffFieldType.ResRef) { ResrefValue = TemplateResRef });
            gff.Add("Times", new GffField(GffFieldType.Byte) { ByteValue = (byte)TimeOfDay });
            gff.Add("Volume", new GffField(GffFieldType.Byte) { ByteValue = Volume });
            gff.Add("VolumeVrtn", new GffField(GffFieldType.Byte) { ByteValue = VolumeVariation });
            gff.Add("Comment", new GffField(GffFieldType.CExoString) { StringValue = Comment });
            gff.Add("PaletteID", new GffField(GffFieldType.Byte) { ByteValue = PaletteID });
            gff.Add("GeneratedType", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsGenerated) });
            gff.Add("XPosition", new GffField(GffFieldType.Float) { FloatValue = XPosition });
            gff.Add("YPosition", new GffField(GffFieldType.Float) { FloatValue = YPosition });
            gff.Add("ZPosition", new GffField(GffFieldType.Float) { FloatValue = ZPosition });

            return gff;
        }
    }
}
