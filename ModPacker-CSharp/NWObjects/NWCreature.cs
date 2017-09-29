using System;
using System.Collections.Generic;
using ModPacker_CSharp.Enums;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public class NWCreature
    {
        public ushort AppearanceTypeID { get; set; }
        public byte BodyBagID { get; set; }
        public byte Charisma { get; set; }
        public float ChallengeRating { get; set; }
        public List<NWClass> Classes { get; set; } 
        public byte Constitution { get; set; }
        public string Conversation { get; set; }
        public int ChallengeRatingAdjustment { get; set; }
        public short CurrentHitPoints { get; set; }
        public uint DecayTime { get; set; }
        public string Deity { get; set; }
        public NWLocalizedString Description { get; set; }
        public byte Dexterity { get; set; }
        public bool IsDisarmable { get; set; }
        // TODO: Equip_ItemList
        public uint FactionID { get; set; }
        public List<ushort> FeatIDs { get; set; }
        public NWLocalizedString FirstName { get; set; }
        public short FortitudeBonus { get; set; }
        public GenderType Gender { get; set; }
        public byte GoodEvil { get; set; }
        public short HitPoints { get; set; }
        public byte Intelligence { get; set; }
        public bool IsConversationInterruptable { get; set; }
        public bool IsImmortal { get; set; }
        public bool IsPC { get; set; }
        public List<NWItem> InventoryItems { get; set; } 
        public NWLocalizedString LastName { get; set; }
        public byte LawfulChaotic { get; set; }
        public bool IsLootable { get; set; }
        public short MaxHitPoints { get; set; }
        public byte NaturalAC { get; set; }
        public bool HasNoPermanentDeath { get; set; }
        public byte PerceptionRangeID { get; set; }
        public int PhenotypeID { get; set; }
        public bool IsPlot { get; set; }
        public ushort PortraitID { get; set; }
        public byte RaceID { get; set; }
        public short ReflexBonus { get; set; }
        public string OnPhysicalAttacked { get; set; }
        public string OnDamaged { get; set; }
        public string OnDeath { get; set; }
        public string OnConversation { get; set; }
        public string OnInventoryDisturbed { get; set; }
        public string OnCombatRoundEnd { get; set; }
        public string OnHeartbeat { get; set; }
        public string OnBlocked { get; set; }
        public string OnPerception { get; set; }
        public string OnRested { get; set; }
        public string OnSpawn { get; set; }
        public string OnSpellCastAt { get; set; }
        public string OnUserDefined { get; set; }
        public List<byte> SkillRanks { get; set; } 
        public ushort SoundSetFileID { get; set; }
        public List<NWSpecialAbility> SpecialAbilities { get; set; } 
        public byte StartingPackageID { get; set; }
        public byte Strength { get; set; }
        public string Subrace { get; set; }
        public string Tag { get; set; }
        public byte TailID { get; set; }
        public int WalkRate { get; set; }
        public short WillBonus { get; set; }
        public byte WingsID { get; set; }
        
        public string Comment { get; set; }
        public byte PaletteID { get; set; }
        public string TemplateResref { get; set; }
        public float XOrientation { get; set; }
        public float YOrientation { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float ZPosition { get; set; }

        public NWCreature()
        {
            InventoryItems = new List<NWItem>();
            Classes = new List<NWClass>();
            FeatIDs = new List<ushort>();
            SpecialAbilities = new List<NWSpecialAbility>();
            SkillRanks = new List<byte>();
        }

        public static NWCreature FromGff(GffStruct source)
        {
            NWCreature creature = new NWCreature();
            creature.AppearanceTypeID = source["Appearance_Type"].WordValue;
            creature.BodyBagID = source["BodyBag"].ByteValue;
            creature.Charisma = source["Cha"].ByteValue;
            creature.ChallengeRating = source["ChallengeRating"].FloatValue;
            creature.Constitution = source["Con"].ByteValue;
            creature.ChallengeRatingAdjustment = source["CRAdjust"].IntValue;
            creature.CurrentHitPoints = source["CurrentHitPoints"].ShortValue;
            creature.DecayTime = source["DecayTime"].DWordValue;
            creature.Deity = source["Deity"].StringValue;
            creature.Description = source["Description"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["Description"].LocalizedStrings[0];
            creature.Dexterity = source["Dex"].ByteValue;
            creature.IsDisarmable = Convert.ToBoolean(source["Disarmable"].ByteValue);
            creature.FactionID = source["FactionID"].WordValue;
            creature.FirstName = source["FirstName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["FirstName"].LocalizedStrings[0];
            creature.FortitudeBonus = source["fortbonus"].ShortValue;
            creature.Gender = (GenderType)source["Gender"].ByteValue;
            creature.GoodEvil = source["GoodEvil"].ByteValue;
            creature.HitPoints = source["HitPoints"].ShortValue;
            creature.Intelligence = source["Int"].ByteValue;
            creature.IsConversationInterruptable = Convert.ToBoolean(source["Interruptable"].ByteValue);
            creature.IsImmortal = Convert.ToBoolean(source["IsImmortal"].ByteValue);
            creature.IsPC = Convert.ToBoolean(source["IsPC"].ByteValue);
            creature.LastName = source["LastName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["LastName"].LocalizedStrings[0];
            creature.LawfulChaotic = source["LawfulChaotic"].ByteValue;
            creature.IsLootable = Convert.ToBoolean(source["Lootable"].ByteValue);
            creature.MaxHitPoints = source["MaxHitPoints"].ShortValue;
            creature.NaturalAC = source["NaturalAC"].ByteValue;
            creature.HasNoPermanentDeath = Convert.ToBoolean(source["NoPermDeath"].ByteValue);
            creature.PerceptionRangeID = source["PerceptionRange"].ByteValue;
            creature.PhenotypeID = source["Phenotype"].IntValue;
            creature.IsPlot = Convert.ToBoolean(source["Plot"].ByteValue);
            creature.PortraitID = source["PortraitId"].WordValue;
            creature.RaceID = source["Race"].ByteValue;
            creature.ReflexBonus = source["refbonus"].ShortValue;
            creature.OnPhysicalAttacked = source["ScriptAttacked"].ResrefValue;
            creature.OnDamaged = source["ScriptDamaged"].ResrefValue;
            creature.OnDeath = source["ScriptDeath"].ResrefValue;
            creature.OnConversation = source["ScriptDialogue"].ResrefValue;
            creature.OnInventoryDisturbed = source["ScriptDisturbed"].ResrefValue;
            creature.OnCombatRoundEnd = source["ScriptEndRound"].ResrefValue;
            creature.OnHeartbeat = source["ScriptHeartbeat"].ResrefValue;
            creature.OnBlocked = source["ScriptOnBlocked"].ResrefValue;
            creature.OnPerception = source["ScriptOnNotice"].ResrefValue;
            creature.OnRested = source["ScriptRested"].ResrefValue;
            creature.OnSpawn = source["ScriptSpawn"].ResrefValue;
            creature.OnSpellCastAt = source["ScriptSpellAt"].ResrefValue;
            creature.OnUserDefined = source["ScriptUserDefine"].ResrefValue;
            creature.SoundSetFileID = source["SoundSetFile"].WordValue;
            creature.StartingPackageID = source["StartingPackage"].ByteValue;
            creature.Strength = source["Str"].ByteValue;
            creature.Subrace = source["Subrace"].StringValue;
            creature.Tag = source["Tag"].StringValue;
            creature.TailID = source["Tail_New"].ByteValue;
            creature.WalkRate = source["WalkRate"].IntValue;
            creature.WillBonus = source["willbonus"].ShortValue;
            creature.WingsID = source["Wings_New"].ByteValue;

            foreach (GffStruct @struct in source["ClassList"].ListValue)
            {
                NWClass @class = new NWClass
                {
                    ClassID = @struct["Class"].IntValue,
                    ClassLevel = @struct["ClassLevel"].ShortValue
                };

                creature.Classes.Add(@class);
            }
            
            foreach (GffStruct @struct in source["FeatList"].ListValue)
                creature.FeatIDs.Add(@struct["Feat"].WordValue);

            // TODO: Figure out this struct
            //if (source.ContainsKey("ItemList"))
            //    foreach (GffStruct @struct in source["ItemList"].ListValue)
            //        creature.InventoryItems.Add(NWItem.FromGff(@struct));
            
            foreach (GffStruct @struct in source["SkillList"].ListValue)
                creature.SkillRanks.Add(@struct["Rank"].ByteValue);
            
            foreach (GffStruct @struct in source["SpecAbilityList"].ListValue)
            {
                NWSpecialAbility ability = new NWSpecialAbility
                {
                    SpellCasterLevel = @struct["SpellCasterLevel"].ByteValue,
                    SpellID = @struct["Spell"].WordValue,
                    SpellFlags = (SpellFlagType)@struct["SpellFlags"].ByteValue
                };

                creature.SpecialAbilities.Add(ability);
            }

            return creature;
        }

    }
}
