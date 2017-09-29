using System;
using System.Collections.Generic;
using System.Linq;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWModule
    {
        public bool UsesSOU { get; set; }
        public bool UsesHOTU { get; set; }
        public int CreatorID { get; set; }
        public string CustomTLK { get; set; }
        public byte DawnHour { get; set; }
        public byte DuskHour { get; set; }
        public byte StartDay { get; set; }
        public byte StartHour { get; set; }
        public byte StartMonth { get; set; }
        public uint StartYear { get; set; }
        public string StartMovie { get; set; }
        public string Tag { get; set; }
        public uint Version { get; set; }
        public byte XPScale { get; set; }

        public NWLocalizedString Description { get; set; }
        public string EntryAreaResref { get; set; }
        public float EntryDirectionX { get; set; }
        public float EntryDirectionY { get; set; }

        public float EntryPositionX { get; set; }
        public float EntryPositionY { get; set; }
        public float EntryPositionZ { get; set; }

        public int ModuleID { get; set; }
        public bool IsSaveGame { get; set; }
        public string MinimumGameVersion { get; set; }
        public byte MinutesPerHour { get; set; }
        public NWLocalizedString Name { get; set; }
        public string OnAcquireItem { get; set; }
        public string OnActivateItem { get; set; }
        public string OnClientEnter { get; set; }
        public string OnClientLeave { get; set; }
        public string OnCutsceneAbort { get; set; }
        public string OnHeartbeat { get; set; }
        public string OnModuleLoad { get; set; }
        public string OnModuleStart { get; set; }
        public string OnPlayerDeath { get; set; }
        public string OnPlayerDying { get; set; }
        public string OnEquipItem { get; set; }
        public string OnLevelUp { get; set; }
        public string OnPlayerRest { get; set; }
        public string OnUnequipItem { get; set; }
        public string OnPlayerRespawn { get; set; }
        public string OnUnacquireItem { get; set; }
        public string OnUserDefined { get; set; }

        public List<string> CachedScripts { get; set; } 
        public List<string> HakPaks { get; set; } 

        public List<NWLocalizedString> LocalizedModuleDescriptions { get; set; }
        public List<NWArea> Areas { get; set; }
        public List<NWCreature> PaletteCreatures { get; set; } 
        public List<NWDoor> PaletteDoors { get; set; } 
        public List<NWEncounter> PaletteEncounters { get; set; } 
        public List<NWItem> PaletteItems { get; set; }
        public List<NWPlaceable> PalettePlaceables { get; set; } 
        public List<NWSound> PaletteSounds { get; set; } 
        public List<NWStore> PaletteStores { get; set; } 
        public List<NWTrigger> PaletteTriggers { get; set; } 
        public List<NWWaypoint> PaletteWaypoints { get; set; } 

        public NWModule()
        {
            Areas = new List<NWArea>();
            CachedScripts = new List<string>();
            HakPaks = new List<string>();

            PaletteItems = new List<NWItem>();
            PaletteCreatures = new List<NWCreature>();
            PaletteDoors = new List<NWDoor>();
            PaletteEncounters = new List<NWEncounter>();
            PaletteItems = new List<NWItem>();
            PalettePlaceables = new List<NWPlaceable>();
            PaletteSounds = new List<NWSound>();
            PaletteStores = new List<NWStore>();
            PaletteTriggers = new List<NWTrigger>();
            PaletteWaypoints = new List<NWWaypoint>();
        }

        public static NWModule FromGff(List<Gff> source )
        {
            NWModule module = new NWModule();

            Gff ifo = source.Find(x => x.ResourceType == GffResourceType.IFO);
            List<Gff> areList = source.Where(x => x.ResourceType == GffResourceType.ARE).ToList();
            List<Gff> gitList = source.Where(x => x.ResourceType == GffResourceType.GIT).ToList();
            List<Gff> gicList = source.Where(x => x.ResourceType == GffResourceType.GIC).ToList();
            
            #region Module Fields
            module.UsesHOTU = ifo.RootStruct["Expansion_Pack"].WordValue == 3 || ifo.RootStruct["Expansion_Pack"].WordValue == 2;
            module.UsesSOU = ifo.RootStruct["Expansion_Pack"].WordValue == 3 || ifo.RootStruct["Expansion_Pack"].WordValue == 1;
            module.CreatorID = ifo.RootStruct["Mod_Creator_ID"].IntValue;
            module.CustomTLK = ifo.RootStruct["Mod_CustomTlk"].StringValue;
            module.DawnHour = ifo.RootStruct["Mod_DawnHour"].ByteValue;
            module.Description = ifo.RootStruct["Mod_Description"].LocalizedStrings[0];
            module.DuskHour = ifo.RootStruct["Mod_DuskHour"].ByteValue;
            module.EntryAreaResref = ifo.RootStruct["Mod_Entry_Area"].ResrefValue;
            module.EntryDirectionX = ifo.RootStruct["Mod_Entry_Dir_X"].FloatValue;
            module.EntryDirectionY = ifo.RootStruct["Mod_Entry_Dir_Y"].FloatValue;
            module.EntryPositionX = ifo.RootStruct["Mod_Entry_X"].FloatValue;
            module.EntryPositionY = ifo.RootStruct["Mod_Entry_Y"].FloatValue;
            module.EntryPositionZ = ifo.RootStruct["Mod_Entry_Z"].FloatValue;
            module.ModuleID = BitConverter.ToInt32(ifo.RootStruct["Mod_ID"].VoidDataValue, 0);
            module.IsSaveGame = Convert.ToBoolean(ifo.RootStruct["Mod_IsSaveGame"].ByteValue);
            module.MinimumGameVersion = ifo.RootStruct["Mod_MinGameVer"].StringValue;
            module.MinutesPerHour = ifo.RootStruct["Mod_MinPerHour"].ByteValue;
            module.Name = ifo.RootStruct["Mod_Name"].LocalizedStrings[0];
            module.OnAcquireItem = ifo.RootStruct["Mod_OnAcquirItem"].ResrefValue;
            module.OnActivateItem = ifo.RootStruct["Mod_OnActvtItem"].ResrefValue;
            module.OnClientEnter = ifo.RootStruct["Mod_OnClientEntr"].ResrefValue;
            module.OnClientLeave = ifo.RootStruct["Mod_OnClientLeav"].ResrefValue;
            module.OnCutsceneAbort = ifo.RootStruct["Mod_OnCutsnAbort"].ResrefValue;
            module.OnHeartbeat = ifo.RootStruct["Mod_OnHeartbeat"].ResrefValue;
            module.OnModuleLoad = ifo.RootStruct["Mod_OnModLoad"].ResrefValue;
            module.OnModuleStart = ifo.RootStruct["Mod_OnModStart"].ResrefValue;
            module.OnPlayerDeath = ifo.RootStruct["Mod_OnPlrDeath"].ResrefValue;
            module.OnPlayerDying = ifo.RootStruct["Mod_OnPlrDying"].ResrefValue;
            module.OnEquipItem = ifo.RootStruct["Mod_OnPlrEqItm"].ResrefValue;
            module.OnLevelUp = ifo.RootStruct["Mod_OnPlrLvlUp"].ResrefValue;
            module.OnUnequipItem = ifo.RootStruct["Mod_OnPlrUnEqItm"].ResrefValue;
            module.OnPlayerRest = ifo.RootStruct["Mod_OnPlrRest"].ResrefValue;
            module.OnPlayerRespawn = ifo.RootStruct["Mod_OnSpawnBtnDn"].ResrefValue;
            module.OnUnacquireItem = ifo.RootStruct["Mod_OnUnAqreItem"].ResrefValue;
            module.OnUserDefined = ifo.RootStruct["Mod_OnUsrDefined"].ResrefValue;
            module.StartDay = ifo.RootStruct["Mod_StartDay"].ByteValue;
            module.StartHour = ifo.RootStruct["Mod_StartHour"].ByteValue;
            module.StartMonth = ifo.RootStruct["Mod_StartMonth"].ByteValue;
            module.StartMovie = ifo.RootStruct["Mod_StartMovie"].ResrefValue;
            module.StartYear = ifo.RootStruct["Mod_StartYear"].DWordValue;
            module.Tag = ifo.RootStruct["Mod_Tag"].StringValue;
            module.Version = ifo.RootStruct["Mod_Version"].DWordValue;
            module.XPScale = ifo.RootStruct["Mod_XPScale"].ByteValue;

            // Load Module Areas
            for (int x = 0; x < areList.Count; x++)
            {
                Gff are = areList[x];
                Gff git = gitList[x];
                Gff gic = gicList[x];

                module.Areas.Add(NWArea.FromGff(are, git, gic));
            }

            // Load Cached Scripts
            foreach (GffStruct script in ifo.RootStruct["Mod_CacheNSSList"].ListValue)
            {
                module.CachedScripts.Add(script["ResRef"].ResrefValue);
            }

            // Load Hakpaks
            foreach (GffStruct hakpak in ifo.RootStruct["Mod_HakList"].ListValue)
            {
                module.HakPaks.Add(hakpak["Mod_Hak"].StringValue);
            }

            #endregion

            #region Palette Blueprint Loading
            
            // Creatures
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTC).ToList())
                module.PaletteCreatures.Add(NWCreature.FromGff(gff.RootStruct));

            // Doors
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTD).ToList())
                module.PaletteDoors.Add(NWDoor.FromGff(gff.RootStruct));

            // Encounters
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTE).ToList())
                module.PaletteEncounters.Add(NWEncounter.FromGff(gff.RootStruct));

            // Items
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTI).ToList())
                module.PaletteItems.Add(NWItem.FromGff(gff.RootStruct));

            // Placeables
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTP).ToList())
                module.PalettePlaceables.Add(NWPlaceable.FromGff(gff.RootStruct));

            // Sounds
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTS).ToList())
                module.PaletteSounds.Add(NWSound.FromGff(gff.RootStruct));

            // Stores
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTM).ToList())
                module.PaletteStores.Add(NWStore.FromGff(gff.RootStruct));

            // Triggers
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTT).ToList())
                module.PaletteTriggers.Add(NWTrigger.FromGff(gff.RootStruct));

            // Waypoints
            foreach (Gff gff in source.Where(x => x.ResourceType == GffResourceType.UTW).ToList())
                module.PaletteWaypoints.Add(NWWaypoint.FromGff(gff.RootStruct));


            #endregion

            return module;
        }

        public Gff ToGff()
        {
            throw new System.NotImplementedException();
        }
    }
}
