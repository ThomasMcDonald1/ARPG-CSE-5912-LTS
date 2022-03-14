using System.IO;
using UnityEditor;
using UnityEngine;

public class AbilityParser
{
    [MenuItem("AbilityParsing/ParseAbility")]
    public static void GetAbilityFilePath()
    {
        AbilityData abilityData;
        string path = EditorUtility.OpenFilePanel("Select Ability Json File", "", "json");
        if (path.Length != 0)
        {
            Debug.Log("Parsing json located at: " + path);
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                abilityData = JsonUtility.FromJson<AbilityData>(json);
            }
            Parse(abilityData);
        }
    }

    static void Parse(AbilityData abilityData)
    {
        GameObject ability = new GameObject(abilityData.name);
        ability.AddComponent<Ability>();
        Ability abilityObj = ability.GetComponent<Ability>();
        abilityObj.description = abilityData.description;
        ParseAbilityIcon(abilityObj, abilityData);
        ParseAbilityCastingVFX(abilityObj, abilityData);
        ParseAbilityPower(ability, abilityData);
        ParseAbilityRange(ability, abilityData);
        ParseAbilityAOE(ability, abilityData);
        ParseAbilityCost(ability, abilityData);
        ParseAbilityCast(ability, abilityData);
        ParseAbilityCooldown(ability, abilityData);
        ParseAbilityConditionals(ability, abilityData);
        ParseAbilityEffects(ability, abilityData);
        CreateAbilityPrefab(abilityData.name, ability);
    }

    static void ParseAbilityIcon(Ability ability, AbilityData abilityData)
    {
        if (abilityData.icon != null)
        {
            string path = "Assets/ImportedAssets/SmallImports/2000_Icons/";
            path += abilityData.icon + ".png";
            Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (icon != null)
                Debug.Log("Icon name: " + icon.name);
            ability.icon = icon;
        }
    }

    static void ParseAbilityCastingVFX(Ability ability, AbilityData abilityData)
    {
        if (abilityData.castingVfx != null)
        {
            string path = "Assets/Spells Pack/LWRP(URP)/Particles_LWRP/Prefabs/Projectiles/Casting/";
            path += abilityData.castingVfx + ".prefab";
            Debug.Log("Loading VFX at path: " + path);
            GameObject vfxPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (vfxPrefab != null)
                Debug.Log("vfxPrefab: " + vfxPrefab.name);
            ability.spellcastVFXObj = vfxPrefab;
        }
    }

    static void ParseAbilityPower(GameObject ability, AbilityData abilityData)
    {
        switch (abilityData.powerType.ToLower())
        {
            case "physical":
                ability.AddComponent<PhysicalAbilityPower>();
                break;
            case "magical":
                ability.AddComponent<MagicalAbilityPower>();
                break;
            default:
                break;
        }
        BaseAbilityPower power = ability.GetComponent<BaseAbilityPower>();
        if (power != null)
            power.baseDamageOrHealing = abilityData.baseDamageOrHealing;
    }

    static void ParseAbilityRange(GameObject ability, AbilityData abilityData)
    {
        switch (abilityData.rangeType.ToLower())
        {
            case "constant":
                ability.AddComponent<ConstantAbilityRange>();
                break;
            case "self":
                ability.AddComponent<SelfAbilityRange>();
                break;
            default:
                break;
        }
        BaseAbilityRange range = ability.GetComponent<BaseAbilityRange>();
        if (range != null)
            range.range = abilityData.range;
    }

    static void ParseAbilityAOE(GameObject ability, AbilityData abilityData)
    {
        switch (abilityData.aoeType.ToLower())
        {
            case "specify":
                ability.AddComponent<SpecifyAbilityArea>();
                break;
            case "single":
                ability.AddComponent<SingleCharacterAbilityArea>();
                break;
            case "pb":
                ability.AddComponent<PointBlankAbilityArea>();
                break;
            default:
                break;
        }
        BaseAbilityArea aa = ability.GetComponent<BaseAbilityArea>();
        if (aa is SpecifyAbilityArea || aa is PointBlankAbilityArea)
        {
            aa.aoeRadius = abilityData.aoeRadius;
        }
    }

    static void ParseAbilityCost(GameObject ability, AbilityData abilityData)
    {
        switch (abilityData.costType.ToLower())
        {
            case "mana":
                ability.AddComponent<AbilityEnergyCost>();
                break;
            case "energy":
                ability.AddComponent<AbilityEnergyCost>();
                break;
            case "health":
                ability.AddComponent<AbilityHealthCost>();
                break;
            default:
                break;
        }
        BaseAbilityCost cost = ability.GetComponent<BaseAbilityCost>();
        cost.cost = abilityData.cost;
    }

    static void ParseAbilityCast(GameObject ability, AbilityData abilityData)
    {
        switch (abilityData.castType.ToLower())
        {
            case "timer":
                ability.AddComponent<CastTimerCastType>();
                break;
            case "instant":
                ability.AddComponent<InstantCastType>();
                break;
            default:
                break;
        }
        BaseCastType castType = ability.GetComponent<BaseCastType>();
        if (castType is CastTimerCastType)
            castType.castTime = abilityData.castTime;
    }

    static void ParseAbilityCooldown(GameObject ability, AbilityData abilityData)
    {
        if (abilityData.cooldown > 0)
            ability.AddComponent<AbilityCooldown>();
        AbilityCooldown abilityCooldown = ability.GetComponent<AbilityCooldown>();
        if (abilityCooldown != null)
            abilityCooldown.abilityCooldown = abilityData.cooldown;
    }

    static void ParseAbilityConditionals(GameObject ability, AbilityData abilityData)
    {
        if (abilityData.abilityRequiresCursorSelection)
            ability.AddComponent<AbilityRequiresCursorSelection>();
        if (abilityData.abilityRequiresCharacterUnderCursor)
            ability.AddComponent<AbilityRequiresCharacterUnderCursor>();
    }

    static void ParseAbilityEffects(GameObject ability, AbilityData abilityData)
    {
        for (int i = 0; i < abilityData.effects.Length; i++)
        {
            GameObject effect = new GameObject(abilityData.effects[i].name);
            effect.transform.SetParent(ability.transform);
            ParseAbilityEffect(effect, abilityData.effects[i]);
            ParseAbilityEffectHitRate(effect, abilityData.effects[i]);
            ParseAbilityEffectTarget(effect, abilityData.effects[i]);
            ParseAbilityEffectElement(effect, abilityData.effects[i]);
        }
    }

    static void ParseAbilityEffect(GameObject effectObj, AbilityEffectData effectData)
    {
        switch (effectData.effect.ToLower())
        {
            case "damage":
                effectObj.AddComponent<DamageAbilityEffect>();
                break;
            case "delayed damage":
                effectObj.AddComponent<DelayedDamageAbilityEffect>();
                break;
            case "heal":
                effectObj.AddComponent<HealingAbilityEffect>();
                break;
            case "charge character":
                effectObj.AddComponent<ChargeCharacterAbilityEffect>();
                break;
            case "charge ground":
                effectObj.AddComponent<ChargeGroundAbilityEffect>();
                break;
            case "knockback":
                effectObj.AddComponent<KnockbackAbilityEffect>();
                break;
            case "leap":
                effectObj.AddComponent<LeapAbilityEffect>();
                break;
            case "pull":
                effectObj.AddComponent<PullAbilityEffect>();
                break;
            default:
                break;
        }
        BaseAbilityEffect effect = effectObj.GetComponent<BaseAbilityEffect>();
        ParseOrigin(effect, effectData);
        ParseVFX(effect, effectData);
        if (effect is KnockbackAbilityEffect knockbackAbilityEffect)
        {
            knockbackAbilityEffect.knockbackXZDisplacement = effectData.xzDisplacement;
            knockbackAbilityEffect.knockbackHeight = effectData.height;
        }
    }

    static void ParseOrigin(BaseAbilityEffect effect, AbilityEffectData effectData)
    {
        if (effectData.origin != null)
            effect.effectOrigin = effectData.origin;
    }
    static void ParseVFX(BaseAbilityEffect effect, AbilityEffectData effectData)
    {
        if (effectData.vfx != null)
        {
            string path = "Assets/Spells Pack/LWRP(URP)/Particles_LWRP/Prefabs/";
            path += effectData.vfx + ".prefab";
            Debug.Log("Loading VFX at path: " + path);
            GameObject vfxPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (vfxPrefab != null)
                Debug.Log("Effect vfxPrefab: " + vfxPrefab.name);
            effect.effectVFXObj = vfxPrefab;
        }
    }

    static void ParseAbilityEffectHitRate(GameObject effect, AbilityEffectData effectData)
    {
        switch (effectData.hitRateType.ToLower())
        {
            case "evasion":
                effect.AddComponent<HitRateAgainstEvasion>();
                break;
            case "guaranteed":
                effect.AddComponent<HitRateGuaranteed>();
                break;
            default:
                break;
        }
    }

    static void ParseAbilityEffectTarget(GameObject effect, AbilityEffectData effectData)
    {
        switch (effectData.targetType.ToLower())
        {
            case "living":
                effect.AddComponent<AnythingLivingAbilityEffectTarget>();
                break;
            case "friendly":
                effect.AddComponent<FriendlyAbilityEffectTarget>();
                break;
            case "rival":
                effect.AddComponent<RivalAbilityEffectTarget>();
                break;
            default:
                break;
        }
    }

    static void ParseAbilityEffectElement(GameObject effect, AbilityEffectData effectData)
    {
        if (effectData.element != null)
        {
            switch (effectData.element.ToLower())
            {
                case "fire":
                    effect.AddComponent<FireAbilityEffectElement>();
                    break;
                case "cold":
                    effect.AddComponent<ColdAbilityEffectElement>();
                    break;
                case "lightning":
                    effect.AddComponent<LightningAbilityEffectElement>();
                    break;
                case "poison":
                    effect.AddComponent<PoisonAbilityEffectElement>();
                    break;
                default:
                    break;
            }
        }
    }

    static void CreateAbilityPrefab(string name, GameObject ability)
    {
        string path = string.Format("Assets/Resources/Abilities/{0}.prefab", name);
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (obj == null)
            Create(path, ability);
        else
            Debug.LogError("Prefab with that name already exists.");
    }

    static GameObject Create(string path, GameObject ability)
    {
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(ability, path);
        GameObject.DestroyImmediate(ability);
        return prefab;
    }
}
