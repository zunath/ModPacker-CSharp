using System;
using System.Collections.Generic;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWItem
    {
        public string FileName { get; set; }
        public uint AdditionalCost { get; set; }
        public int BaseItemID { get; set; }
        public byte Charges { get; set; }
        public uint Cost { get; set; }
        public bool IsCursed { get; set; }
        public NWLocalizedString IdentifiedDescription { get; set; }
        public NWLocalizedString UnidentifiedDescription { get; set; }
        public NWLocalizedString LocalizedName { get; set; }
        public bool IsPlot { get; set; }
        public List<NWItemProperty> ItemProperties { get; set; }
        public ushort StackSize { get; set; }
        public bool IsStolen { get; set; }
        public string Tag { get; set; }
        public string TemplateResref { get; set; }

        public byte Cloth1Color { get; set; }
        public byte Cloth2Color { get; set; }
        public byte Leather1Color { get; set; }
        public byte Leather2Color { get; set; }
        public byte Metal1Color { get; set; }
        public byte Metal2Color { get; set; }
        public byte ModelPart1 { get; set; }
        public byte ModelPart2 { get; set; }
        public byte ModelPart3 { get; set; }

        public byte ArmorPart_Belt { get; set; }
        public byte ArmorPart_LeftBicep { get; set; }
        public byte ArmorPart_LeftForearm { get; set; }
        public byte ArmorPart_LeftFoot { get; set; }
        public byte ArmorPart_LeftHand { get; set; }
        public byte ArmorPart_LeftShin { get; set; }
        public byte ArmorPart_LeftShoulder { get; set; }
        public byte ArmorPart_LeftThigh { get; set; }
        public byte ArmorPart_Neck { get; set; }
        public byte ArmorPart_Pelvis { get; set; }
        public byte ArmorPart_RightBicep { get; set; }
        public byte ArmorPart_RightForearm { get; set; }
        public byte ArmorPart_RightFoot { get; set; }
        public byte ArmorPart_RightHand { get; set; }
        public byte ArmorPart_RightShin { get; set; }
        public byte ArmorPart_RightShoulder { get; set; }
        public byte ArmorPart_RightThigh { get; set; }
        public byte ArmorPart_Torso { get; set; }
        public byte ArmorPart_Robe { get; set; }


        public string Comment { get; set; }
        public byte PaletteID { get; set; }
        public float XOrientation { get; set; }
        public float YOrientation { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float ZPosition { get; set; }

        public NWItem()
        {
            ItemProperties = new List<NWItemProperty>();
        }


        public static NWItem FromGff(GffStruct source)
        {
            NWItem item = new NWItem();
            item.AdditionalCost = source["AddCost"].DWordValue;
            item.BaseItemID = source["BaseItem"].IntValue;
            item.Charges = source["Charges"].ByteValue;
            item.Cost = source["Cost"].DWordValue;
            item.IsCursed = Convert.ToBoolean(source["Cursed"].ByteValue);
            item.IdentifiedDescription = source["DescIdentified"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["DescIdentified"].LocalizedStrings[0];
            item.UnidentifiedDescription = source["Description"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["Description"].LocalizedStrings[0];
            item.LocalizedName = !source.ContainsKey("LocaName") || source["LocName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["LocName"].LocalizedStrings[0];
            item.IsPlot = Convert.ToBoolean(source["Plot"].ByteValue);
            item.StackSize = source["StackSize"].WordValue;
            item.IsStolen = Convert.ToBoolean(source["Stolen"].ByteValue);
            item.Tag = source["Tag"].StringValue;
            item.TemplateResref = source["TemplateResRef"].ResrefValue;

            item.Cloth1Color = source.ContainsKey("Cloth1Color") ? source["Cloth1Color"].ByteValue : (byte)0;
            item.Cloth2Color = source.ContainsKey("Cloth2Color") ? source["Cloth2Color"].ByteValue  : (byte)0;
            item.Leather1Color = source.ContainsKey("Leather1Color") ? source["Leather1Color"].ByteValue  : (byte)0;
            item.Leather2Color = source.ContainsKey("Leather2Color") ? source["Leather2Color"].ByteValue  : (byte)0;
            item.Metal1Color = source.ContainsKey("Metal1Color") ? source["Metal1Color"].ByteValue  : (byte)0;
            item.Metal2Color = source.ContainsKey("Metal2Color") ? source["Metal2Color"].ByteValue  : (byte)0;

            item.ModelPart1 = source.ContainsKey("ModelPart1") ? source["ModelPart1"].ByteValue : (byte) 0;
            item.ModelPart2 = source.ContainsKey("ModelPart2") ? source["ModelPart2"].ByteValue : (byte)0;
            item.ModelPart3 = source.ContainsKey("ModelPart3") ? source["ModelPart3"].ByteValue : (byte)0;

            item.ArmorPart_LeftBicep = source.ContainsKey("ArmorPart_LBicep") ? source["ArmorPart_LBicep"].ByteValue : (byte)0;
            item.ArmorPart_LeftForearm = source.ContainsKey("ArmorPart_LFArm") ? source["ArmorPart_LFArm"].ByteValue : (byte)0;
            item.ArmorPart_LeftFoot = source.ContainsKey("ArmorPart_LFoot") ? source["ArmorPart_LFoot"].ByteValue : (byte)0;
            item.ArmorPart_LeftHand = source.ContainsKey("ArmorPart_LHand") ? source["ArmorPart_LHand"].ByteValue : (byte)0;
            item.ArmorPart_LeftShin = source.ContainsKey("ArmorPart_LShin") ? source["ArmorPart_LShin"].ByteValue : (byte)0;
            item.ArmorPart_LeftShoulder = source.ContainsKey("ArmorPart_LShoul") ? source["ArmorPart_LShoul"].ByteValue : (byte)0;
            item.ArmorPart_LeftThigh = source.ContainsKey("ArmorPart_LThigh") ? source["ArmorPart_LThigh"].ByteValue : (byte)0;
            item.ArmorPart_Neck = source.ContainsKey("ArmorPart_Neck") ? source["ArmorPart_Neck"].ByteValue : (byte)0;
            item.ArmorPart_Pelvis = source.ContainsKey("ArmorPart_Pelvis") ? source["ArmorPart_Pelvis"].ByteValue : (byte)0;
            item.ArmorPart_RightBicep = source.ContainsKey("ArmorPart_RBicep") ? source["ArmorPart_RBicep"].ByteValue : (byte)0;
            item.ArmorPart_RightForearm = source.ContainsKey("ArmorPart_RFArm") ? source["ArmorPart_RFArm"].ByteValue : (byte)0;
            item.ArmorPart_RightFoot = source.ContainsKey("ArmorPart_RFoot") ? source["ArmorPart_RFoot"].ByteValue : (byte)0;
            item.ArmorPart_RightHand = source.ContainsKey("ArmorPart_RHand") ? source["ArmorPart_RHand"].ByteValue: (byte)0;
            item.ArmorPart_Robe = source.ContainsKey("ArmorPart_Robe") ? source["ArmorPart_Robe"].ByteValue : (byte)0;
            item.ArmorPart_RightShin = source.ContainsKey("ArmorPart_RShin") ? source["ArmorPart_RShin"].ByteValue : (byte)0;
            item.ArmorPart_RightShoulder = source.ContainsKey("ArmorPart_RShoul") ? source["ArmorPart_RShoul"].ByteValue: (byte)0;
            item.ArmorPart_RightThigh = source.ContainsKey("ArmorPart_RThigh") ? source["ArmorPart_RThigh"].ByteValue: (byte)0;
            item.ArmorPart_Torso = source.ContainsKey("ArmorPart_Torso") ? source["ArmorPart_Torso"].ByteValue: (byte)0;
            

            foreach (GffStruct @struct in source["PropertiesList"].ListValue)
            {
                NWItemProperty prop = new NWItemProperty
                {
                    ChanceAppear = @struct.ContainsKey("ChanceAppear") ? @struct["ChanceAppear"].ByteValue : (byte)0,
                    CostTable = @struct.ContainsKey("CostTable") ? @struct["CostTable"].ByteValue : (byte)0,
                    CostValue = @struct.ContainsKey("CostValue") ? @struct["CostValue"].WordValue : (ushort)0,
                    Param1 = @struct.ContainsKey("Param1") ? @struct["Param1"].ByteValue : (byte)0,
                    Param1Value = @struct.ContainsKey("Param1Value") ? @struct["Param1Value"].ByteValue : (byte)0,
                    Param2 = @struct.ContainsKey("Param2") ? @struct["Param2"].ByteValue : (byte)0,
                    Param2Value = @struct.ContainsKey("Param2Value") ? @struct["Param2Value"].ByteValue : (byte)0,
                    PropertyName = @struct.ContainsKey("PropertyName") ? @struct["PropertyName"].WordValue : (ushort)0,
                    Subtype = @struct.ContainsKey("Subtype") ? @struct["Subtype"].WordValue: (ushort)0,
                };

                item.ItemProperties.Add(prop);
            }


            return item;
        }

        public Gff ToGff()
        {
            throw new System.NotImplementedException();
        }
    }
}
