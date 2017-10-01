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

        public List<Gff> ToGff()
        {
            List<Gff> gff = new List<Gff>();

            // Build IFO
            Gff ifo = new Gff
            {
                ResourceType = GffResourceType.IFO,
                Resref = "module",
                RootStruct = new GffStruct()
            };

            ushort expansion = 0;
            if (UsesSOU && UsesHOTU)
                expansion = 3;
            else if (UsesSOU && !UsesHOTU)
                expansion = 1;
            else if (!UsesSOU && UsesHOTU)
                expansion = 2;

            ifo.RootStruct.Add("Expansion_Pack", new GffField(GffFieldType.Word) { WordValue = expansion });
            ifo.RootStruct.Add("Mod_Creator_ID", new GffField(GffFieldType.Int) { IntValue = CreatorID });
            ifo.RootStruct.Add("Mod_CustomTlk", new GffField(GffFieldType.CExoString) { StringValue = CustomTLK });
            ifo.RootStruct.Add("Mod_DawnHour", new GffField(GffFieldType.Byte) { ByteValue = DawnHour});

            GffField tempField = new GffField(GffFieldType.CExoLocString);
            tempField.LocalizedStrings.Add(Description);
            ifo.RootStruct.Add("Mod_Description", tempField);

            ifo.RootStruct.Add("Mod_DuskHour", new GffField(GffFieldType.Byte) { ByteValue = DuskHour });
            ifo.RootStruct.Add("Mod_Entry_Area", new GffField(GffFieldType.ResRef) { ResrefValue = EntryAreaResref });
            ifo.RootStruct.Add("Mod_Entry_Dir_X", new GffField(GffFieldType.Float) { FloatValue = EntryDirectionX });
            ifo.RootStruct.Add("Mod_Entry_Dir_Y", new GffField(GffFieldType.Float) { FloatValue = EntryDirectionY });
            ifo.RootStruct.Add("Mod_Entry_X", new GffField(GffFieldType.Float) { FloatValue = EntryPositionX });
            ifo.RootStruct.Add("Mod_Entry_Y", new GffField(GffFieldType.Float) { FloatValue = EntryPositionY });
            ifo.RootStruct.Add("Mod_Entry_Z", new GffField(GffFieldType.Float) { FloatValue = EntryPositionZ });
            ifo.RootStruct.Add("Mod_ID", new GffField(GffFieldType.Void) { VoidDataValue = BitConverter.GetBytes(ModuleID) });
            ifo.RootStruct.Add("Mod_IsSaveGame", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsSaveGame) });
            ifo.RootStruct.Add("Mod_MinGameVer", new GffField(GffFieldType.CExoString) { StringValue = MinimumGameVersion});
            ifo.RootStruct.Add("Mod_MinPerHour", new GffField(GffFieldType.Byte) { ByteValue = MinutesPerHour });

            tempField = new GffField(GffFieldType.CExoLocString);
            tempField.LocalizedStrings.Add(Name);
            ifo.RootStruct.Add("Mod_Name", tempField);

            ifo.RootStruct.Add("Mod_OnAcquirItem", new GffField(GffFieldType.ResRef) { ResrefValue = OnAcquireItem });
            ifo.RootStruct.Add("Mod_OnActvtItem", new GffField(GffFieldType.ResRef) { ResrefValue = OnActivateItem });
            ifo.RootStruct.Add("Mod_OnClientEntr", new GffField(GffFieldType.ResRef) { ResrefValue = OnClientEnter });
            ifo.RootStruct.Add("Mod_OnClientLeav", new GffField(GffFieldType.ResRef) { ResrefValue = OnClientLeave });
            ifo.RootStruct.Add("Mod_OnCutsnAbort", new GffField(GffFieldType.ResRef) { ResrefValue = OnCutsceneAbort });
            ifo.RootStruct.Add("Mod_OnHeartbeat", new GffField(GffFieldType.ResRef) { ResrefValue = OnHeartbeat });
            ifo.RootStruct.Add("Mod_OnModLoad", new GffField(GffFieldType.ResRef) { ResrefValue = OnModuleLoad });
            ifo.RootStruct.Add("Mod_OnModStart", new GffField(GffFieldType.ResRef) { ResrefValue = OnModuleStart });
            ifo.RootStruct.Add("Mod_OnPlrDeath", new GffField(GffFieldType.ResRef) { ResrefValue = OnPlayerDeath });
            ifo.RootStruct.Add("Mod_OnPlrDying", new GffField(GffFieldType.ResRef) { ResrefValue = OnPlayerDying });
            ifo.RootStruct.Add("Mod_OnPlrEqItm", new GffField(GffFieldType.ResRef) { ResrefValue = OnEquipItem });
            ifo.RootStruct.Add("Mod_OnPlrLvlUp", new GffField(GffFieldType.ResRef) { ResrefValue = OnLevelUp });
            ifo.RootStruct.Add("Mod_OnPlrUnEqItm", new GffField(GffFieldType.ResRef) { ResrefValue = OnUnequipItem });
            ifo.RootStruct.Add("Mod_OnPlrRest", new GffField(GffFieldType.ResRef) { ResrefValue = OnPlayerRest });
            ifo.RootStruct.Add("Mod_OnSpawnBtnDn", new GffField(GffFieldType.ResRef) { ResrefValue = OnPlayerRespawn });
            ifo.RootStruct.Add("Mod_OnUnAqreItem", new GffField(GffFieldType.ResRef) { ResrefValue = OnUnacquireItem });
            ifo.RootStruct.Add("Mod_OnUsrDefined", new GffField(GffFieldType.ResRef) { ResrefValue = OnUserDefined });
            ifo.RootStruct.Add("Mod_StartDay", new GffField(GffFieldType.Byte) { ByteValue = StartDay });
            ifo.RootStruct.Add("Mod_StartHour", new GffField(GffFieldType.Byte) { ByteValue = StartHour });
            ifo.RootStruct.Add("Mod_StartMonth", new GffField(GffFieldType.Byte) { ByteValue = StartMonth });
            ifo.RootStruct.Add("Mod_StartMovie", new GffField(GffFieldType.ResRef) { ResrefValue = StartMovie });
            ifo.RootStruct.Add("Mod_StartYear", new GffField(GffFieldType.DWord) { DWordValue = StartYear });
            ifo.RootStruct.Add("Mod_Tag", new GffField(GffFieldType.CExoString) { StringValue = Tag });
            ifo.RootStruct.Add("Mod_Version", new GffField(GffFieldType.DWord) { DWordValue = Version });
            ifo.RootStruct.Add("Mod_XPScale", new GffField(GffFieldType.Byte) { ByteValue = XPScale });

            // Build cached scripts for IFO
            List<GffStruct> cachedScriptsList = new List<GffStruct>();
            foreach (var script in CachedScripts)
            {
                GffStruct gffScript = new GffStruct
                {
                    {"ResRef", new GffField(GffFieldType.ResRef){ResrefValue = script}}
                };
                cachedScriptsList.Add(gffScript);
            }
            ifo.RootStruct.Add("Mod_CacheNSSList", new GffField(GffFieldType.List) { ListValue = cachedScriptsList });

            // Build hakpak list for IFO
            List<GffStruct> hakpakList = new List<GffStruct>();
            foreach (var hakpak in HakPaks)
            {
                GffStruct gffHakpak = new GffStruct
                {
                    {"Mod_Hak", new GffField(GffFieldType.CExoString){StringValue = hakpak} }
                };
                hakpakList.Add(gffHakpak);
            }
            ifo.RootStruct.Add("Mod_HakList", new GffField(GffFieldType.List){ListValue = hakpakList});
            gff.Add(ifo);
            
            // Build areas
            foreach (var area in Areas)
            {
                var tuple = area.ToGff();
                gff.Add(tuple.Item1);
                gff.Add(tuple.Item2);
                gff.Add(tuple.Item3);
            }
            
            gff.AddRange(PaletteCreatures.Select(creature => new Gff
            {
                ResourceType = GffResourceType.UTC,
                Resref = creature.TemplateResref,
                RootStruct = creature.ToGff()
            }));

            gff.AddRange(PaletteDoors.Select(door => new Gff
            {
                ResourceType = GffResourceType.UTD,
                Resref = door.TemplateResref,
                RootStruct = door.ToGff()
            }));

            gff.AddRange(PaletteEncounters.Select(encounter => new Gff
            {
                ResourceType = GffResourceType.UTE,
                Resref = encounter.TemplateResref,
                RootStruct = encounter.ToGff()
            }));

            gff.AddRange(PaletteItems.Select(item => new Gff
            {
                ResourceType = GffResourceType.UTI,
                Resref = item.TemplateResref,
                RootStruct = item.ToGff()
            }));

            gff.AddRange(PalettePlaceables.Select(placeable => new Gff
            {
                ResourceType = GffResourceType.UTP,
                Resref = placeable.TemplateResref,
                RootStruct = placeable.ToGff()
            }));

            gff.AddRange(PaletteSounds.Select(sound => new Gff
            {
                ResourceType = GffResourceType.UTS,
                Resref = sound.TemplateResRef,
                RootStruct = sound.ToGff()
            }));

            gff.AddRange(PaletteStores.Select(store => new Gff
            {
                ResourceType = GffResourceType.UTM,
                Resref = store.TemplateResref,
                RootStruct = store.ToGff()
            }));

            gff.AddRange(PaletteTriggers.Select(trigger => new Gff
            {
                ResourceType = GffResourceType.UTT,
                Resref = trigger.TemplateResref,
                RootStruct = trigger.ToGff()
            }));

            gff.AddRange(PaletteWaypoints.Select(waypoint => new Gff
            {
                ResourceType = GffResourceType.UTW,
                Resref = waypoint.TemplateResRef,
                RootStruct = waypoint.ToGff()
            }));
            
            return gff;
        }
    }
}
