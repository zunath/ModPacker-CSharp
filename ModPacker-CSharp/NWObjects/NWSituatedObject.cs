using System;
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

    }
}
