using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanelController : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerMoney;
    public TextMeshProUGUI playerLevel;
    public PlayerMoney Gold;
    public Stats stats;
    public GameObject PlayerStatsUI;
    public TextMeshProUGUI[] PlayerStats;
    public CustomCharacter playerInfo;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerStats = PlayerStatsUI.GetComponentsInChildren<TextMeshProUGUI>();
        showCharacterStates();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void showCharacterStates()
    {
        playerName.text = playerInfo.charName;
        playerMoney.text = Gold.money.ToString();
      
        playerLevel.text = "Level: " + stats[StatTypes.LVL].ToString();

        PlayerStats[0].text = "EXP: "+ stats[StatTypes.EXP].ToString();
        PlayerStats[1].text = "Damage: " + stats[StatTypes.PHYATK].ToString();
        PlayerStats[2].text = "Attacking Range: " + stats[StatTypes.AttackRange].ToString();
        PlayerStats[3].text = "Attacking Speed: " + stats[StatTypes.AtkSpeed].ToString();
        PlayerStats[4].text = "Evasion: " + stats[StatTypes.Evasion].ToString();
        PlayerStats[5].text = "Max HP: " + stats[StatTypes.MaxHP].ToString();
        PlayerStats[6].text = "Max Mana: " + stats[StatTypes.MaxMana].ToString();
        PlayerStats[7].text = "Run Speed: " + stats[StatTypes.RunSpeed].ToString();
    }
}
