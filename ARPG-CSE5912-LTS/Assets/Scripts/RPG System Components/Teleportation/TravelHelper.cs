using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelHelper : MonoBehaviour
{
    [SerializeField] Porter porter;
    [SerializeField] Lorekeeper loreKeeper;
    [SerializeField] GeneralStore generalStore;
    [SerializeField] Blacksmith blacksmith;
    [SerializeField] Player player;
    public void EnterRuinsOfYeager()
    {
        player.GetComponent<PlayerController>().DungeonNum = 1;
        InteractionManager.GetInstance().StopInteraction();
        
        LoadingStateController.Instance.LoadScene("Dungeon1");
        loreKeeper.gameObject.SetActive(false);
        generalStore.gameObject.SetActive(false);
        blacksmith.gameObject.SetActive(false);
        porter.gameObject.SetActive(false);
    }
}
