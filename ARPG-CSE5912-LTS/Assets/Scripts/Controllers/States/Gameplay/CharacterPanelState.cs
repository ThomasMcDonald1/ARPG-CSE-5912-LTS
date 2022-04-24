using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterPanelState : BaseGameplayState
{

    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered character panel state");
        Time.timeScale = 0;
        gameplayStateController.characterPanelCanvas.enabled = true;
        foreach (Sound s in FindObjectOfType<AudioManager>().sounds)
        {
            if (s.name != "Theme") s.source.Stop();
        }
        exitPanelToGameButton.onClick.AddListener(() => OnBackButtonClicked());
        exitAbilityShopButton.onClick.AddListener(() => CloseAbilityShop());
        changeToAbilityMenu.onClick.AddListener(() => AbilityMenuStartUp());

    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.characterPanelCanvas.enabled = false;
        Time.timeScale = 1;
    }

    void OnBackButtonClicked()
    {
        gameplayStateController.ChangeState<GameplayState>();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }


    void AbilityMenuStartUp()
    {
        gameplayStateController.abilityCanvas.enabled = true;
        gameplayStateController.characterPanelCanvas.enabled = false;
        AbilityShopController.instance.PopulateAbilityShop();

    }
    void CloseAbilityShop()
    {
        gameplayStateController.abilityCanvas.enabled = false;

        gameplayStateController.ChangeState<GameplayState>();


        //characterPanel.enabled = false;


    }


    protected override void OnUIElementHovered(object sender, InfoEventArgs<List<RaycastResult>> e)
    {
        //figure out if the raycast results contain an item or ability
       foreach (RaycastResult result in e.info)
        {
            GameObject go = result.gameObject;
            //Debug.Log("GameObject: " + go);

            Button invButton = go.GetComponent<Button>();
            //Image itemImg = go.GetComponent<Image>()
            //Button xButton = invButton;
          
            if (invButton != null&&invButton.CompareTag("invSlotButton"))
            {

                InventorySlot invSlot = invButton.GetComponentInParent<InventorySlot>();
                if (invSlot!=null && invSlot.item != null)
                {
                    //Debug.Log("Item hovered over: " + invSlot.item.name);
                    TipManager.instance.ShowInventoryTooltip(invSlot.item);
                }
                else 
                {
                    TipManager.instance.HideWindow();
                }
            }
            else if(invButton != null && !invButton.CompareTag("invSlotButton"))
            {
                TipManager.instance.HideWindow();
            }
            EquipSlot equipSlot = go.GetComponent<EquipSlot>();
            if (equipSlot != null)
            {
                TipManager.instance.ShowInventoryTooltip(equipSlot.item);
            }
        }
    }

}
