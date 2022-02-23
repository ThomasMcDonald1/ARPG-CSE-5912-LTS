using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityCost : MonoBehaviour
{
    public int cost;
    //[HideInInspector] public GameplayStateController gameplayStateController;
    //[HideInInspector] public HealthBarController healthBarController;
    //[HideInInspector] public EnergyBarController energyBarController;

    //private void Awake()
    //{
    //    gameplayStateController = GetComponentInParent<GameplayStateController>();
    //}

    //private void Start()
    //{
    //    healthBarController = gameplayStateController.healthBarController;
    //    energyBarController = gameplayStateController.energyBarController;
    //}

    public abstract void DeductResourceFromCaster(Character caster);

    public abstract bool CheckCharacterHasResourceCostForCastingAbility(Character caster);
}
