using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AbilityShopSlot : MonoBehaviour
{
    public TextMeshProUGUI abilityName;
    public Button purchaseButton;
    public Image abilityImg;
    private Ability ability;
    public Player player;
    //[SerializeField] GameObject viewContent;
    public static event EventHandler<InfoEventArgs<int>> SkillpointUsedToBuyAbilityEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Purchase()
    {
        player.abilitiesKnown.Add(ability);
        AbilityShopController.instance.stats[StatTypes.SkillPoints] -= 1;
        AbilityShopController.instance.PopulateAbilityShop();
        Destroy(gameObject);
        SkillpointUsedToBuyAbilityEvent?.Invoke(this, new InfoEventArgs<int>(0));
    }
    public void InitializeSlot(Ability ab, Player player)
    {
        //Debug.Log("ability names:" +ab.name);
        ability = ab;
        abilityName.text = ab.name;
        abilityImg.sprite = ab.icon;
        if (AbilityShopController.instance.stats[StatTypes.SkillPoints] == 0)
        {
            purchaseButton.interactable = false;
        }
          
        this.player = player;
    }
}
