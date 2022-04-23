using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityShopController : MonoBehaviour
{
    public TextMeshProUGUI abilityPoint;
    public int skillPoint;
    public Player player;
    public Stats stats;
    public GameObject abilityCatelog;
    [SerializeField] List<Ability> Abilities;
    [SerializeField] List<Ability> PlayerAbilities;
    [SerializeField] GameObject abilityShopSlotPrefab;
    [SerializeField] GameObject viewContent;
    #region Singleton
    public static AbilityShopController instance;


    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
       foreach(Ability ab in abilityCatelog.GetComponentsInChildren<Ability>())
        {
            Abilities.Add(ab);
        }
        PlayerAbilities = player.abilitiesKnown;
    }
    #endregion
    // Update is called once per frame
    private void Update()
    {
        
    }

    
    public void PopulateAbilityShop()
    {
        ClearAbilityShop();
        skillPoint = stats[StatTypes.SkillPoints];
        abilityPoint.text = skillPoint.ToString();
        foreach (Ability ability in Abilities)
        {
            //Debug.Log("ability names:" +ability.name);
            if (!PlayerAbilities.Contains(ability)){
                GameObject abSlot = Instantiate(abilityShopSlotPrefab);
                abSlot.transform.SetParent(viewContent.transform);
                AbilityShopSlot abs = abSlot.transform.GetComponent<AbilityShopSlot>();
                abs.InitializeSlot(ability, player);
            }
            
        }
    }
    private void ClearAbilityShop()
    {
        foreach (Transform child in viewContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
