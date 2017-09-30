using System;
using System.Collections.Generic;
using System.Linq;
using ModPacker_CSharp.Enums;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWArea
    {
        // ARE Properties
        public int ChanceOfLightning { get; set; }
        public int ChanceOfRain { get; set; }
        public int ChanceOfSnow { get; set; }
        public string Comments { get; set; }
        public int CreatorID { get; set; }
        public bool HasDayNightCycle { get; set; }
        public bool IsInterior { get; set; }
        public bool IsUnderground { get; set; }
        public bool IsNatural { get; set; }
        public int Height { get; set; }
        public int AreaID { get; set; }
        public bool IsNight { get; set; }
        public byte LightingScheme { get; set; }
        public ushort LoadScreenID { get; set; }
        public int ModListenCheck { get; set; }
        public int ModSpotCheck { get; set; }
        public uint MoonAmbientColor { get; set; }
        public uint MoonDiffuseColor { get; set; }
        public byte MoonFogAmount { get; set; }
        public uint MoonFogColor { get; set; }
        public bool HasMoonShadows { get; set; }
        public NWLocalizedString Name { get; set; }
        public bool IsRestingAllowed { get; set; }
        public string OnEnter { get; set; }
        public string OnExit { get; set; }
        public string OnHeartbeat { get; set; }
        public string OnUserDefined { get; set; }
        public PVPSettingType PVPSetting { get; set; }
        public string Resref { get; set; }
        public byte SkyBoxID { get; set; }
        public byte ShadowOpacity { get; set; }
        public uint SunAmbientColor { get; set; }
        public uint SunDiffuseColor { get; set; }
        public byte SunFogAmount { get; set; }
        public uint SunFogColor { get; set; }
        public bool HasSunShadows { get; set; }
        public string Tag { get; set; }
        // TileList to be added here
        public string TileSet { get; set; }
        public uint Version { get; set; }
        public int Width { get; set; }
        public WindPowerType WindPower { get; set; }

        // GIT Properties
        public int AmbientSoundDay { get; set; }
        public int AmbientSoundDayVolume { get; set; }
        public int AmbientSoundNight { get; set; }
        public int AmbientSoundNightVolume { get; set; }
        public int EnvironmentAudio { get; set; }
        public int MusicBattle { get; set; }
        public int MusicDay { get; set; }
        public int MusicDelay { get; set; }
        public int MusicNight { get; set; }

        public List<NWCreature> Creatures { get; set; } 
        public List<NWDoor> Doors { get; set; } 
        public List<NWEncounter> Encounters { get; set; } 
        public List<NWItem> Items { get; set; } 
        public List<NWPlaceable> Placeables { get; set; } 
        public List<NWSound> Sounds { get; set; } 
        public List<NWStore> Stores { get; set; } 
        public List<NWTrigger> Triggers { get; set; } 
        public List<NWWaypoint> Waypoints { get; set; }

        public NWArea()
        {
            Creatures = new List<NWCreature>();
            Doors = new List<NWDoor>();
            Encounters = new List<NWEncounter>();
            Items = new List<NWItem>();
            Placeables = new List<NWPlaceable>();
            Sounds = new List<NWSound>();
            Stores = new List<NWStore>();
            Triggers = new List<NWTrigger>();
            Waypoints = new List<NWWaypoint>();
        }
        

        public static NWArea FromGff(Gff areSource, Gff gitSource, Gff gicSource)
        {
            if(areSource.ResourceType != GffResourceType.ARE)
                throw new Exception("areSource must be of type ARE");
            if(gitSource.ResourceType != GffResourceType.GIT)
                throw new Exception("gitSource must be of type GIT");
            if(gicSource.ResourceType != GffResourceType.GIC)
                throw new Exception("gicSource must be of type GIC");

            NWArea area = new NWArea();

            #region ARE Fields
            area.ChanceOfLightning = areSource.RootStruct["ChanceLightning"].IntValue;
            area.ChanceOfRain = areSource.RootStruct["ChanceRain"].IntValue;
            area.ChanceOfSnow = areSource.RootStruct["ChanceSnow"].IntValue;
            area.Comments = areSource.RootStruct["Comments"].StringValue;
            area.CreatorID = areSource.RootStruct["Creator_ID"].IntValue;
            area.HasDayNightCycle = Convert.ToBoolean(areSource.RootStruct["DayNightCycle"].ByteValue);
            
            AreaFlagType type = (AreaFlagType)areSource.RootStruct["Flags"].DWordValue;
            if ((type & AreaFlagType.Interior) != 0)
                area.IsInterior = true;
            if ((type & AreaFlagType.Natural) != 0)
                area.IsNatural = true;
            if ((type & AreaFlagType.Underground) != 0)
                area.IsUnderground = true;

            area.Height = areSource.RootStruct["Creator_ID"].IntValue;

            area.AreaID = areSource.RootStruct["ID"].IntValue;
            area.IsNight = Convert.ToBoolean(areSource.RootStruct["IsNight"].ByteValue);
            area.LightingScheme = areSource.RootStruct["LightingScheme"].ByteValue;
            area.LoadScreenID = areSource.RootStruct["LoadScreenID"].WordValue;
            area.ModListenCheck = areSource.RootStruct["ModListenCheck"].IntValue;
            area.ModSpotCheck = areSource.RootStruct["ModSpotCheck"].IntValue;
            area.MoonAmbientColor = areSource.RootStruct["MoonAmbientColor"].DWordValue;
            area.MoonDiffuseColor = areSource.RootStruct["MoonDiffuseColor"].DWordValue;
            area.MoonFogAmount = areSource.RootStruct["MoonFogAmount"].ByteValue;
            area.MoonFogColor = areSource.RootStruct["MoonFogColor"].DWordValue;
            area.HasMoonShadows = Convert.ToBoolean(areSource.RootStruct["MoonShadows"].ByteValue);
            area.Name = areSource.RootStruct["Name"].LocalizedStrings[0];
            area.IsRestingAllowed = !Convert.ToBoolean(areSource.RootStruct["NoRest"].ByteValue);
            area.OnEnter = areSource.RootStruct["OnEnter"].ResrefValue;
            area.OnExit = areSource.RootStruct["OnExit"].ResrefValue;
            area.OnHeartbeat = areSource.RootStruct["OnHeartbeat"].ResrefValue;
            area.OnUserDefined = areSource.RootStruct["OnUserDefined"].ResrefValue;
            area.PVPSetting = (PVPSettingType) areSource.RootStruct["PlayerVsPlayer"].ByteValue;
            area.Resref = areSource.RootStruct["ResRef"].ResrefValue;
            area.SkyBoxID = areSource.RootStruct["SkyBox"].ByteValue;
            area.ShadowOpacity = areSource.RootStruct["ShadowOpacity"].ByteValue;
            area.SunAmbientColor = areSource.RootStruct["SunAmbientColor"].DWordValue;
            area.SunDiffuseColor = areSource.RootStruct["SunDiffuseColor"].DWordValue;
            area.SunFogAmount = areSource.RootStruct["SunFogAmount"].ByteValue;
            area.SunFogColor = areSource.RootStruct["SunFogColor"].DWordValue;
            area.HasSunShadows = Convert.ToBoolean(areSource.RootStruct["SunShadows"].ByteValue);
            area.Tag = areSource.RootStruct["Tag"].StringValue;
            // TODO: TileList loading here.
            area.TileSet = areSource.RootStruct["Tileset"].ResrefValue;
            area.Version = areSource.RootStruct["Version"].DWordValue;
            area.Width = areSource.RootStruct["Width"].IntValue;
            area.WindPower = (WindPowerType)areSource.RootStruct["WindPower"].IntValue;

            #endregion

            #region GIT Fields
            // GIT files contain object lists + properties available for changing via scripts.
            GffStruct areaProperties = gitSource.RootStruct["AreaProperties"].StructValue;
            area.AmbientSoundDay = areaProperties["AmbientSndDay"].IntValue;
            area.AmbientSoundNight = areaProperties["AmbientSndNight"].IntValue;
            area.AmbientSoundDayVolume = areaProperties["AmbientSndDayVol"].IntValue;
            area.AmbientSoundNightVolume = areaProperties["AmbientSndNitVol"].IntValue;
            area.EnvironmentAudio = areaProperties["EnvAudio"].IntValue;
            area.MusicBattle = areaProperties["MusicBattle"].IntValue;
            area.MusicDay = areaProperties["MusicDay"].IntValue;
            area.MusicNight = areaProperties["MusicNight"].IntValue;
            area.MusicDelay = areaProperties["MusicDelay"].IntValue;
            
            foreach (GffStruct @struct in gitSource.RootStruct["Creature List"].ListValue)
                area.Creatures.Add(NWCreature.FromGff(@struct));

            foreach (GffStruct @struct in gitSource.RootStruct["Door List"].ListValue)
                area.Doors.Add(NWDoor.FromGff(@struct));

            foreach (GffStruct @struct in gitSource.RootStruct["Encounter List"].ListValue)
                area.Encounters.Add(NWEncounter.FromGff(@struct));

            foreach (GffStruct @struct in gitSource.RootStruct["List"].ListValue)
                area.Items.Add(NWItem.FromGff(@struct));

            foreach (GffStruct @struct in gitSource.RootStruct["Placeable List"].ListValue)
                area.Placeables.Add(NWPlaceable.FromGff(@struct));

            foreach (GffStruct @struct in gitSource.RootStruct["SoundList"].ListValue)
                area.Sounds.Add(NWSound.FromGff(@struct));

            foreach (GffStruct @struct in gitSource.RootStruct["StoreList"].ListValue)
                area.Stores.Add(NWStore.FromGff(@struct));

            foreach (GffStruct @struct in gitSource.RootStruct["TriggerList"].ListValue)
                area.Triggers.Add(NWTrigger.FromGff(@struct));

            foreach (GffStruct @struct in gitSource.RootStruct["WaypointList"].ListValue)
                area.Waypoints.Add(NWWaypoint.FromGff(@struct));

            #endregion

            #region GIC Fields
            // GIC files contain only comments.
            for (int x = 0; x < gicSource.RootStruct["Creature List"].ListValue.Count; x++)
                area.Creatures[x].Comment = gicSource.RootStruct["Creature List"].ListValue[x]["Comment"].StringValue;

            for (int x = 0; x < gicSource.RootStruct["Door List"].ListValue.Count; x++)
                area.Doors[x].Comment = gicSource.RootStruct["Door List"].ListValue[x]["Comment"].StringValue;

            // 2015-12-31: There appears to be a defect in NWN where encounter comments aren't saved correctly.
            // I can't process this data, so I commented it out.
            //for (int x = 0; x < gicSource.RootStruct["Encounter List"].ListValue.Count; x++)
            //    area.Encounters[x].Comment = gicSource.RootStruct["Encounter List"].ListValue[x]["Comment"].StringValue;

            for (int x = 0; x < gicSource.RootStruct["List"].ListValue.Count; x++)
                area.Items[x].Comment = gicSource.RootStruct["List"].ListValue[x]["Comment"].StringValue;
            
            for (int x = 0; x < gicSource.RootStruct["Placeable List"].ListValue.Count; x++)
                area.Placeables[x].Comment = gicSource.RootStruct["Placeable List"].ListValue[x]["Comment"].StringValue;
            
            for (int x = 0; x < gicSource.RootStruct["SoundList"].ListValue.Count; x++)
                area.Sounds[x].Comment = gicSource.RootStruct["SoundList"].ListValue[x]["Comment"].StringValue;
            
            for (int x = 0; x < gicSource.RootStruct["StoreList"].ListValue.Count; x++)
                area.Stores[x].Comment = gicSource.RootStruct["StoreList"].ListValue[x]["Comment"].StringValue;
            
            for (int x = 0; x < gicSource.RootStruct["TriggerList"].ListValue.Count; x++)
                area.Triggers[x].Comment = gicSource.RootStruct["TriggerList"].ListValue[x]["Comment"].StringValue;
            
            for (int x = 0; x < gicSource.RootStruct["WaypointList"].ListValue.Count; x++)
                area.Waypoints[x].Comment = gicSource.RootStruct["WaypointList"].ListValue[x]["Comment"].StringValue;


            #endregion


            return area;
        }

        public Tuple<Gff, Gff, Gff> ToGff()
        {
            Gff areSource = new Gff();
            Gff gitSource = new Gff();
            Gff gicSource = new Gff();

            #region ARE Fields
            areSource.RootStruct = new GffStruct();
            areSource.RootStruct.Add("ChanceLightning", new GffField { IntValue = ChanceOfLightning });
            areSource.RootStruct.Add("ChanceRain", new GffField { IntValue = ChanceOfRain });
            areSource.RootStruct.Add("ChanceSnow", new GffField { IntValue = ChanceOfSnow });
            areSource.RootStruct.Add("Comments", new GffField { StringValue = Comments});
            areSource.RootStruct.Add("Creator_ID", new GffField { IntValue = CreatorID });
            areSource.RootStruct.Add("DayNightCycle", new GffField { ByteValue = Convert.ToByte(HasDayNightCycle)});

            uint type = 0x0000;
            if (IsInterior) type += 0x0001;
            if (IsUnderground) type += 0x0002;
            if (IsNatural) type += 0x0004;

            areSource.RootStruct.Add("Flags", new GffField { DWordValue = type });
            areSource.RootStruct.Add("Creator_ID", new GffField { IntValue = CreatorID });
            areSource.RootStruct.Add("ID", new GffField { IntValue = AreaID });
            areSource.RootStruct.Add("IsNight", new GffField { ByteValue = Convert.ToByte(IsNight) });
            areSource.RootStruct.Add("LightingScheme", new GffField { ByteValue = LightingScheme });
            areSource.RootStruct.Add("LoadScreenID", new GffField { WordValue = LoadScreenID });
            areSource.RootStruct.Add("ModListenCheck", new GffField { IntValue = ModListenCheck });
            areSource.RootStruct.Add("ModSpotCheck", new GffField { IntValue = ModSpotCheck });

            areSource.RootStruct.Add("MoonAmbientColor", new GffField { DWordValue = MoonAmbientColor });
            areSource.RootStruct.Add("MoonDiffuseColor", new GffField { DWordValue = MoonDiffuseColor });
            areSource.RootStruct.Add("MoonFogAmount", new GffField { ByteValue = MoonFogAmount });
            areSource.RootStruct.Add("MoonFogColor", new GffField { DWordValue = MoonFogColor });
            areSource.RootStruct.Add("MoonShadows", new GffField { ByteValue = Convert.ToByte(HasMoonShadows) });
            
            GffField tempField = new GffField();
            tempField.LocalizedStrings.Add(Name);
            areSource.RootStruct.Add("Name", tempField);

            areSource.RootStruct.Add("NoRest", new GffField { ByteValue = Convert.ToByte(!IsRestingAllowed) });
            areSource.RootStruct.Add("OnEnter", new GffField { ResrefValue = OnEnter });
            areSource.RootStruct.Add("OnExit", new GffField { ResrefValue = OnExit });
            areSource.RootStruct.Add("OnHeartbeat", new GffField { ResrefValue = OnHeartbeat });
            areSource.RootStruct.Add("OnUserDefined", new GffField { ResrefValue = OnUserDefined });
            areSource.RootStruct.Add("PlayerVsPlayer", new GffField { ByteValue = (byte)PVPSetting });
            areSource.RootStruct.Add("ResRef", new GffField { ResrefValue = Resref });
            areSource.RootStruct.Add("SkyBox", new GffField { ByteValue = SkyBoxID });
            areSource.RootStruct.Add("ShadowOpacity", new GffField { ByteValue = ShadowOpacity });
            areSource.RootStruct.Add("SunAmbientColor", new GffField { DWordValue = SunAmbientColor });
            areSource.RootStruct.Add("SunDiffuseColor", new GffField { DWordValue = SunDiffuseColor });
            areSource.RootStruct.Add("SunFogAmount", new GffField { ByteValue = SunFogAmount });
            areSource.RootStruct.Add("SunFogColor", new GffField { DWordValue = SunFogColor });
            areSource.RootStruct.Add("SunShadows", new GffField { ByteValue = Convert.ToByte(HasSunShadows) });
            areSource.RootStruct.Add("Tag", new GffField { StringValue = Tag });
            // TODO: TileList
            areSource.RootStruct.Add("Tileset", new GffField { ResrefValue = TileSet });
            areSource.RootStruct.Add("Version", new GffField { DWordValue = Version });
            areSource.RootStruct.Add("Width", new GffField { IntValue = Width });
            areSource.RootStruct.Add("WindPower", new GffField { IntValue = (int)WindPower });


            #endregion

            #region GIT Fields
            gitSource.RootStruct = new GffStruct();
            GffStruct areaProperties = new GffStruct();
            areaProperties.Add("AmbientSndDay", new GffField { IntValue = AmbientSoundDay });
            areaProperties.Add("AmbientSndNight", new GffField { IntValue = AmbientSoundNight });
            areaProperties.Add("AmbientSndDayVol", new GffField { IntValue = AmbientSoundDayVolume });
            areaProperties.Add("AmbientSndNitVol", new GffField { IntValue = AmbientSoundNightVolume });
            areaProperties.Add("EnvAudio", new GffField { IntValue = EnvironmentAudio });
            areaProperties.Add("MusicBattle", new GffField { IntValue = MusicBattle });
            areaProperties.Add("MusicDay", new GffField { IntValue = MusicDay });
            areaProperties.Add("MusicNight", new GffField { IntValue = MusicNight });
            areaProperties.Add("MusicDelay", new GffField { IntValue = MusicDelay });

            gitSource.RootStruct.Add("AreaProperties", new GffField{StructValue = areaProperties});

            gitSource.RootStruct.Add("Creature List", new GffField
            {
                ListValue = Creatures.Select(creature => creature.ToGff()).ToList()
            });

            gitSource.RootStruct.Add("Door List", new GffField
            {
                ListValue = Doors.Select(door => door.ToGff()).ToList()
            });

            gitSource.RootStruct.Add("Encounter List", new GffField
            {
                ListValue = Encounters.Select(encounter => encounter.ToGff()).ToList()
            });

            gitSource.RootStruct.Add("List", new GffField
            {
                ListValue = Items.Select(item => item.ToGff()).ToList()
            });

            gitSource.RootStruct.Add("Placeable List", new GffField
            {
                ListValue = Placeables.Select(placeable => placeable.ToGff()).ToList()
            });

            gitSource.RootStruct.Add("SoundList", new GffField
            {
                ListValue = Sounds.Select(sound => sound.ToGff()).ToList()
            });

            gitSource.RootStruct.Add("StoreList", new GffField
            {
                ListValue = Stores.Select(store => store.ToGff()).ToList()
            });

            gitSource.RootStruct.Add("TriggerList", new GffField
            {
                ListValue = Triggers.Select(trigger => trigger.ToGff()).ToList()
            });

            gitSource.RootStruct.Add("WaypointList", new GffField
            {
                ListValue = Waypoints.Select(waypoint => waypoint.ToGff()).ToList()
            });
            
            #endregion

            #region GIC Fields
            gicSource.RootStruct = new GffStruct();

            gicSource.RootStruct.Add("Creature List", new GffField
            {
                ListValue = Creatures.Select(creature => creature.ToGff()).ToList()
            });

            // TODO: Write + test GIC files

            #endregion

            return new Tuple<Gff, Gff, Gff>(areSource, gitSource, gicSource);
        }
    }
}
