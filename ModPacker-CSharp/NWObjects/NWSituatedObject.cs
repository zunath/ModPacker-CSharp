﻿using System;
using ModPacker_CSharp.Enums;
using ModPacker_CSharp.GFFParser;

namespace ModPacker_CSharp.NWObjects
{
    public abstract class NWSituatedObject
    {
        public AnimationStateType AnimationState { get; set; }
        public uint AppearanceID { get; set; }
        public bool AutoRemoveKey { get; set; }
        public byte CloseLockDC { get; set; }
        public string Conversation { get; set; }
        public short CurrentHitPoints { get; set; }
        public NWLocalizedString Description { get; set; }
        public byte DisarmDC { get; set; }
        public uint FactionID { get; set; }
        public byte FortitudeSave { get; set; }
        public byte Hardness { get; set; }
        public short MaxHitPoints { get; set; }
        public bool IsConversationInterruptable { get; set; }
        public bool IsLockable { get; set; }
        public bool IsLocked { get; set; }
        public NWLocalizedString Name { get; set; }
        public string OnClosed { get; set; }
        public string OnDamaged { get; set; }
        public string OnDeath { get; set; }
        public string OnDisarm { get; set; }
        public string OnHeartbeat { get; set; }
        public string OnLock { get; set; }
        public string OnMeleeAttacked { get; set; }
        public string OnOpen { get; set; }
        public string OnSpellCastAt { get; set; }
        public string OnTrapTriggered { get; set; }
        public string OnUnlock { get; set; }
        public string OnUserDefined { get; set; }
        public byte OpenLockDC { get; set; }
        public bool IsPlot { get; set; }
        public ushort PortraitID { get; set; }
        public byte ReflexSave { get; set; }
        public string Tag { get; set; }
        public string TemplateResref { get; set; }
        public bool IsTrapDetectable { get; set; }
        public byte TrapDetectDC { get; set; }
        public bool IsTrapDisarmable { get; set; }
        public bool IsTrapped { get; set; }
        public bool IsTrapOneShot { get; set; }
        public byte TrapTypeID { get; set; }
        public byte WillSave { get; set; }

        public string Comment { get; set; }
        public byte PaletteID { get; set; }
        public float Bearing { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        protected NWSituatedObject SharedFieldsFromGff(GffStruct source)
        {
            AnimationState = (AnimationStateType)source["AnimationState"].ByteValue;
            AppearanceID = source["Appearance"].DWordValue;
            AutoRemoveKey = Convert.ToBoolean(source["AutoRemoveKey"].ByteValue);
            CloseLockDC = source["CloseLockDC"].ByteValue;
            Conversation = source["Conversation"].ResrefValue;
            CurrentHitPoints = source["CurrentHP"].ShortValue;
            Description = source["Description"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["Description"].LocalizedStrings[0];
            DisarmDC = source["DisarmDC"].ByteValue;
            FactionID = source["Faction"].DWordValue;
            FortitudeSave = source["Fort"].ByteValue;
            Hardness = source["Hardness"].ByteValue;
            MaxHitPoints = source["HP"].ShortValue;
            IsConversationInterruptable = Convert.ToBoolean(source["Interruptable"].ByteValue);
            IsLockable = Convert.ToBoolean(source["Lockable"].ByteValue);
            IsLocked = Convert.ToBoolean(source["Locked"].ByteValue);
            Name = source["LocName"].LocalizedStrings.Count <= 0 ? new NWLocalizedString() : source["LocName"].LocalizedStrings[0];
            OnClosed = source["OnClosed"].ResrefValue;
            OnDamaged = source["OnDamaged"].ResrefValue;
            OnDeath = source["OnDeath"].ResrefValue;
            OnDisarm = source["OnDisarm"].ResrefValue;
            OnHeartbeat = source["OnHeartbeat"].ResrefValue;
            OnLock = source["OnLock"].ResrefValue;
            OnMeleeAttacked = source["OnMeleeAttacked"].ResrefValue;
            OnOpen = source["OnOpen"].ResrefValue;
            OnSpellCastAt = source["OnSpellCastAt"].ResrefValue;
            OnTrapTriggered = source["OnTrapTriggered"].ResrefValue;
            OnUnlock = source["OnUnlock"].ResrefValue;
            OnUserDefined = source["OnUserDefined"].ResrefValue;
            OpenLockDC = source["OpenLockDC"].ByteValue;
            IsPlot = Convert.ToBoolean(source["Plot"].ByteValue);
            PortraitID = source["PortraitId"].WordValue;
            ReflexSave = source["Ref"].ByteValue;
            Tag = source["Tag"].StringValue;
            TemplateResref = source["TemplateResRef"].ResrefValue;
            IsTrapDetectable = Convert.ToBoolean(source["TrapDetectable"].ByteValue);
            TrapDetectDC = source["TrapDetectDC"].ByteValue;
            IsTrapDisarmable = Convert.ToBoolean(source["TrapDisarmable"].ByteValue);
            IsTrapped = Convert.ToBoolean(source["TrapFlag"].ByteValue);
            IsTrapOneShot = Convert.ToBoolean(source["TrapOneShot"].ByteValue);
            TrapTypeID = source["TrapType"].ByteValue;
            WillSave = source["Will"].ByteValue;

            Comment = source.ContainsKey("Comment") ? source["Comment"].StringValue : string.Empty;
            PaletteID = source.ContainsKey("PaletteID") ? source["PaletteID"].ByteValue : (byte)0;
            Bearing = source.ContainsKey("Bearing") ? source["Bearing"].FloatValue : 0.0f;
            PositionX = source.ContainsKey("X") ? source["X"].FloatValue : 0.0f;
            PositionY = source.ContainsKey("Y") ? source["Y"].FloatValue : 0.0f;
            PositionZ = source.ContainsKey("Z") ? source["Z"].FloatValue : 0.0f;

            return this;
        }

        protected GffStruct SharedFieldsToGff()
        {
            GffStruct gff = new GffStruct();

            gff.Add("AnimationState", new GffField(GffFieldType.Byte) { ByteValue = (byte)AnimationState });
            gff.Add("Appearance", new GffField(GffFieldType.DWord) { DWordValue = AppearanceID });
            gff.Add("AutoRemoveKey", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(AutoRemoveKey) });
            gff.Add("CloseLockDC", new GffField(GffFieldType.Byte) { ByteValue = CloseLockDC });
            gff.Add("Conversation", new GffField(GffFieldType.ResRef) { ResrefValue = Conversation });
            gff.Add("CurrentHP", new GffField(GffFieldType.Short) {ShortValue = CurrentHitPoints });

            GffField tempField = new GffField(GffFieldType.CExoLocString);
            tempField.LocalizedStrings.Add(Description);
            gff.Add("Description", tempField);

            gff.Add("DisarmDC", new GffField(GffFieldType.Byte) { ByteValue = DisarmDC });
            gff.Add("Faction", new GffField(GffFieldType.DWord) { DWordValue = FactionID });
            gff.Add("Fort", new GffField(GffFieldType.Byte) { ByteValue = FortitudeSave });
            gff.Add("Hardness", new GffField(GffFieldType.Byte) { ByteValue = Hardness });
            gff.Add("HP", new GffField(GffFieldType.Short) {ShortValue = MaxHitPoints });
            gff.Add("Interruptable", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsConversationInterruptable) });
            gff.Add("Lockable", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsLockable) });
            gff.Add("Locked", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsLocked) });

            tempField = new GffField(GffFieldType.CExoLocString);
            tempField.LocalizedStrings.Add(Name);
            gff.Add("LocName", tempField);

            gff.Add("OnClosed", new GffField(GffFieldType.ResRef) { ResrefValue = OnClosed });
            gff.Add("OnDamaged", new GffField(GffFieldType.ResRef) { ResrefValue = OnDamaged });
            gff.Add("OnDeath", new GffField(GffFieldType.ResRef) { ResrefValue = OnDeath });
            gff.Add("OnDisarm", new GffField(GffFieldType.ResRef) { ResrefValue = OnDisarm });
            gff.Add("OnHeartbeat", new GffField(GffFieldType.ResRef) { ResrefValue = OnHeartbeat });
            gff.Add("OnLock", new GffField(GffFieldType.ResRef) { ResrefValue = OnLock });
            gff.Add("OnMeleeAttacked", new GffField(GffFieldType.ResRef) { ResrefValue = OnMeleeAttacked });
            gff.Add("OnOpen", new GffField(GffFieldType.ResRef) { ResrefValue = OnOpen });
            gff.Add("OnSpellCastAt", new GffField(GffFieldType.ResRef) { ResrefValue = OnSpellCastAt });
            gff.Add("OnTrapTriggered", new GffField(GffFieldType.ResRef) { ResrefValue = OnTrapTriggered });
            gff.Add("OnUnlock", new GffField(GffFieldType.ResRef) { ResrefValue = OnUnlock });
            gff.Add("OnUserDefined", new GffField(GffFieldType.ResRef) { ResrefValue = OnUserDefined });
            gff.Add("OpenLockDC", new GffField(GffFieldType.Byte) { ByteValue = OpenLockDC });
            gff.Add("Plot", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsPlot) });
            gff.Add("PortraitId", new GffField(GffFieldType.Word) { WordValue = PortraitID });
            gff.Add("Ref", new GffField(GffFieldType.Byte) { ByteValue = ReflexSave });
            gff.Add("Tag", new GffField(GffFieldType.CExoString) { StringValue = Tag });
            gff.Add("TemplateResRef", new GffField(GffFieldType.ResRef) { ResrefValue = TemplateResref });
            gff.Add("TrapDetectable", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsTrapDetectable) });
            gff.Add("TrapDetectDC", new GffField(GffFieldType.Byte) { ByteValue = TrapDetectDC });
            gff.Add("TrapDisarmable", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsTrapDisarmable) });
            gff.Add("TrapFlag", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsTrapped) });
            gff.Add("TrapOneShot", new GffField(GffFieldType.Byte) { ByteValue = Convert.ToByte(IsTrapOneShot) });
            gff.Add("TrapType", new GffField(GffFieldType.Byte) { ByteValue = TrapTypeID });
            gff.Add("Will", new GffField(GffFieldType.Byte) { ByteValue = WillSave });

            gff.Add("Comment", new GffField(GffFieldType.CExoString) { StringValue = Comment });
            gff.Add("PaletteID", new GffField(GffFieldType.Byte) { ByteValue = PaletteID });
            gff.Add("Bearing", new GffField(GffFieldType.Float) { FloatValue = Bearing });
            gff.Add("X", new GffField(GffFieldType.Float) { FloatValue = PositionX });
            gff.Add("Y", new GffField(GffFieldType.Float) { FloatValue = PositionY });
            gff.Add("Z", new GffField(GffFieldType.Float) { FloatValue = PositionZ });

            return gff;
        }

    }
}
