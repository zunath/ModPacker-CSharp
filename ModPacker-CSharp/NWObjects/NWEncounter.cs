using System;
using System.Collections.Generic;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWEncounter
    {
        public bool IsActive { get; set; }
        public int Difficulty { get; set; }
        public int DifficultyIndex { get; set; }
        public uint FactionID { get; set; }
        public NWLocalizedString Name { get; set; }
        public int MaximumCreatures { get; set; }
        public string OnEntered { get; set; }
        public string OnExhausted { get; set; }
        public string OnExit { get; set; }
        public string OnHeartbeat { get; set; }
        public string OnUserDefined { get; set; }
        public bool PlayerOnly { get; set; }
        public int RecommendedNumberOfCreatures { get; set; }
        public bool Respawns { get; set; }
        public int RespawnTime { get; set; }
        public int NumberOfRespawns { get; set; }
        public bool IsContinuousSpawn { get; set; }
        public string Tag { get; set; }
        public string TemplateResref { get; set; }


        public List<NWEncounterCreature> CreatureList { get; set; } 

        public string Comment { get; set; }
        public byte PaletteID { get; set; }
        public List<NWPoint> Geometry { get; set; }
        public List<NWEncounterSpawnPoint> SpawnPoints { get; set; } 
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float ZPosition { get; set; }

        public NWEncounter()
        {
            CreatureList = new List<NWEncounterCreature>();
            Geometry = new List<NWPoint>();
            SpawnPoints = new List<NWEncounterSpawnPoint>();
        }

        public static NWEncounter FromGff(GffStruct source)
        {
            NWEncounter encounter = new NWEncounter();
            encounter.IsActive = Convert.ToBoolean(source["Active"].ByteValue);
            encounter.Difficulty = source["Difficulty"].IntValue;
            encounter.DifficultyIndex = source["DifficultyIndex"].IntValue;
            encounter.FactionID = source["Faction"].DWordValue;
            encounter.Name = source["LocalizedName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["LocalizedName"].LocalizedStrings[0];
            encounter.MaximumCreatures = source["MaxCreatures"].IntValue;
            encounter.OnEntered = source["OnEntered"].ResrefValue;
            encounter.OnExhausted = source["OnExhausted"].ResrefValue;
            encounter.OnExit = source["OnExit"].ResrefValue;
            encounter.OnHeartbeat = source["OnHeartbeat"].ResrefValue;
            encounter.OnUserDefined = source["OnUserDefined"].ResrefValue;
            encounter.PlayerOnly = Convert.ToBoolean(source["PlayerOnly"].ByteValue);
            encounter.RecommendedNumberOfCreatures = source["RecCreatures"].IntValue;
            encounter.Respawns = Convert.ToBoolean(source["Reset"].ByteValue);
            encounter.RespawnTime = source["ResetTime"].IntValue;
            encounter.NumberOfRespawns = source["Respawns"].IntValue;
            encounter.IsContinuousSpawn = !Convert.ToBoolean(source["SpawnOption"].IntValue);
            encounter.Tag = source["Tag"].StringValue;
            encounter.TemplateResref = source["TemplateResRef"].ResrefValue;

            encounter.XPosition = source["XPosition"].FloatValue;
            encounter.YPosition = source["YPosition"].FloatValue;
            encounter.ZPosition = source["ZPosition"].FloatValue;
            
            foreach (GffStruct @struct in source["CreatureList"].ListValue)
            {
                NWEncounterCreature creature = new NWEncounterCreature
                {
                    Resref = @struct["ResRef"].ResrefValue,
                    AppearanceID = @struct["Appearance"].IntValue,
                    ChallengeRating = @struct["CR"].FloatValue,
                    IsSingleSpawn = Convert.ToBoolean(@struct["SingleSpawn"].ByteValue)
                };
                encounter.CreatureList.Add(creature);
            }
            
            foreach(GffStruct @struct in source["Geometry"].ListValue)
            {
                NWPoint point = new NWPoint
                {
                    X = @struct["X"].FloatValue,
                    Y = @struct["Y"].FloatValue,
                    Z = @struct["Z"].FloatValue
                };

                encounter.Geometry.Add(point);
            }
            
            foreach (GffStruct @struct in source["SpawnPointList"].ListValue)
            {
                NWEncounterSpawnPoint spawnPoint = new NWEncounterSpawnPoint
                {
                    PositionX = @struct["X"].FloatValue,
                    PositionY = @struct["Y"].FloatValue,
                    PositionZ = @struct["Z"].FloatValue,
                    Orientation = @struct["Orientation"].FloatValue
                };

                encounter.SpawnPoints.Add(spawnPoint);
            }
            
            return encounter;
        }

        public GffStruct ToGff()
        {
            GffStruct gff = new GffStruct();
            gff.Add("Active", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsActive) });
            gff.Add("Difficulty", new GffField(GffFieldType.Int) { IntValue = Difficulty });
            gff.Add("DifficultyIndex", new GffField(GffFieldType.Int) { IntValue = DifficultyIndex });
            gff.Add("Faction", new GffField(GffFieldType.DWord) { DWordValue = FactionID });
            
            GffField tempField = new GffField(GffFieldType.CExoLocString);
            tempField.LocalizedStrings.Add(Name);
            gff.Add("LocalizedName", tempField);
            
            gff.Add("MaxCreatures", new GffField(GffFieldType.Int) { IntValue = MaximumCreatures });
            gff.Add("OnEntered", new GffField(GffFieldType.ResRef) { ResrefValue = OnEntered });
            gff.Add("OnExhausted", new GffField(GffFieldType.ResRef) { ResrefValue = OnExhausted });
            gff.Add("OnExit", new GffField(GffFieldType.ResRef) { ResrefValue = OnExit });
            gff.Add("OnHeartbeat", new GffField(GffFieldType.ResRef) { ResrefValue = OnHeartbeat});
            gff.Add("OnUserDefined", new GffField(GffFieldType.ResRef) { ResrefValue = OnUserDefined });
            gff.Add("PlayerOnly", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(PlayerOnly) });
            gff.Add("RecCreatures", new GffField(GffFieldType.Int) { IntValue = RecommendedNumberOfCreatures });
            gff.Add("Reset", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(Respawns) });
            gff.Add("ResetTime", new GffField(GffFieldType.Int) { IntValue = RespawnTime });
            gff.Add("Respawns", new GffField(GffFieldType.Int) { IntValue = NumberOfRespawns });
            gff.Add("SpawnOption", new GffField(GffFieldType.Int) { IntValue = Convert.ToInt32(IsContinuousSpawn) });
            gff.Add("Tag", new GffField(GffFieldType.CExoString) { StringValue = Tag });
            gff.Add("TemplateResRef", new GffField(GffFieldType.ResRef) { ResrefValue = TemplateResref });

            gff.Add("XPosition", new GffField(GffFieldType.Float) { FloatValue = XPosition });
            gff.Add("YPosition", new GffField(GffFieldType.Float) { FloatValue = YPosition });
            gff.Add("ZPosition", new GffField(GffFieldType.Float) { FloatValue = ZPosition });

            GffField creatureList = new GffField(GffFieldType.List);

            foreach (var creature in CreatureList)
            {
                GffStruct gffCreature = new GffStruct
                {
                    {"ResRef", new GffField(GffFieldType.ResRef){ResrefValue = creature.Resref}},
                    {"Appearance", new GffField(GffFieldType.Int){IntValue = creature.AppearanceID}},
                    {"CR", new GffField(GffFieldType.Float){FloatValue = creature.ChallengeRating}},
                    {"SingleSpawn", new GffField(GffFieldType.Byte){ByteValue = Convert.ToByte(creature.IsSingleSpawn)}},
                };

                creatureList.ListValue.Add(gffCreature);
            }

            gff.Add("CreatureList", creatureList);

            GffField geometry = new GffField(GffFieldType.Struct);
            foreach (NWPoint point in Geometry)
            {
                GffStruct @struct = new GffStruct
                {
                    {"X", new GffField(GffFieldType.Float) {FloatValue = point.X}},
                    {"Y", new GffField(GffFieldType.Float) {FloatValue = point.Y}},
                    {"Z", new GffField(GffFieldType.Float) {FloatValue = point.Z}}
                };

                geometry.ListValue.Add(@struct);
            }

            gff.Add("Geometry", geometry);


            GffField spawnPointList = new GffField(GffFieldType.List);

            foreach (var spawnPoint in SpawnPoints)
            {
                GffStruct gffSpawnPoint = new GffStruct
                {
                    {"X", new GffField(GffFieldType.Float){FloatValue = spawnPoint.PositionX}},
                    {"Y", new GffField(GffFieldType.Float){FloatValue = spawnPoint.PositionY}},
                    {"Z", new GffField(GffFieldType.Float){FloatValue = spawnPoint.PositionZ}},
                    {"Orientation", new GffField(GffFieldType.Float){FloatValue = spawnPoint.Orientation}},
                };

                spawnPointList.ListValue.Add(gffSpawnPoint);
            }

            gff.Add("SpawnPointList", spawnPointList);


            return gff;
        }
    }
}
