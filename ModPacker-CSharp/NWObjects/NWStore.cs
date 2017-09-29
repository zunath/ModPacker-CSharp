using System;
using System.Collections.Generic;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWStore
    {
        public bool IsBlackMarket { get; set; }
        public int BlackMarketMarkDown { get; set; }
        public int IdentifyPrice { get; set; }
        public bool CanIdentifyItems
        {
            get { return IdentifyPrice >= 0; }
        }
        public NWLocalizedString LocalizedName { get; set; }
        public int MarkDown { get; set; }
        public int MarkUp { get; set; }
        public int MaxBuyPrice { get; set; }
        public string OnOpenStore { get; set; }
        public string OnCloseStore { get; set; }
        public string Resref { get; set; }
        public int StoreGold { get; set; }
        public List<NWItem> StoreList { get; set; } 
        public List<int> WillNotBuyList { get; set; } 
        public List<int> WillOnlyBuyList { get; set; } 
        public string Tag { get; set; }

        public string Comment { get; set; }
        public byte PaletteID { get; set; }
        public string TemplateResref { get; set; }
        public float XOrientation { get; set; }
        public float YOrientation { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float ZPosition { get; set; }

        public NWStore()
        {
            StoreList = new List<NWItem>();
            WillNotBuyList = new List<int>();
            WillOnlyBuyList = new List<int>();
        }

        public static NWStore FromGff(GffStruct source)
        {
            NWStore store = new NWStore();
            store.IsBlackMarket = Convert.ToBoolean(source["BlackMarket"].ByteValue);
            store.BlackMarketMarkDown = source["BM_MarkDown"].IntValue;
            store.IdentifyPrice = source["IdentifyPrice"].IntValue;
            store.LocalizedName = source["LocName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["LocName"].LocalizedStrings[0];
            store.MarkDown = source["MarkDown"].IntValue;
            store.MarkUp = source["MarkUp"].IntValue;
            store.MaxBuyPrice = source["MaxBuyPrice"].IntValue;
            store.OnOpenStore = source["OnOpenStore"].ResrefValue;
            store.OnCloseStore = source["OnStoreClosed"].ResrefValue;
            store.Resref = source["ResRef"].ResrefValue;
            store.StoreGold = source["StoreGold"].IntValue;
            store.Tag = source["Tag"].StringValue;

            // TODO: figure out this structure.
            //foreach (GffStruct @struct in source["StoreList"].ListValue[0]["ItemList"].ListValue)
            //{
            //    store.StoreList.Add(NWItem.FromGff(@struct));
            //}

            foreach (GffStruct @struct in source["WillNotBuy"].ListValue)
            {
                store.WillNotBuyList.Add(@struct["BaseItem"].IntValue);
            }

            foreach (GffStruct @struct in source["WillOnlyBuy"].ListValue)
            {
                store.WillOnlyBuyList.Add(@struct["BaseItem"].IntValue);
            }

            return store;
        }
    }
}
