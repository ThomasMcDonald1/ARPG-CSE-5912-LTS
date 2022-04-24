[System.Serializable]
public class AbilityData
{
    //Ability
    public string name;
    public string description;
    public string icon;
    public string castingVfx;
    public bool createsProjectileVFX;

    //Ability Power
    public string powerType;
    public float baseDamageOrHealing;

    //Ability Range
    public string rangeType;
    public float range;

    //Ability Area
    public string aoeType;
    public int aoeRadius;

    //Ability Cost
    public string costType;
    public int cost;

    //Ability Cast Type
    public string castType;
    public float castTime;

    //Ability Cooldown
    public float cooldown;

    //Conditionals
    public bool abilityRequiresCursorSelection;
    public bool abilityRequiresCharacterUnderCursor;

    //Movement
    public string abilityMovement;

    //Ability Effects
    public AbilityEffectData[] effects;
}
