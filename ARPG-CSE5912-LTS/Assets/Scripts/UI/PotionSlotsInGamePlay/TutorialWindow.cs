using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialWindow : MonoBehaviour
{
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] RectTransform background;
    public TextMeshProUGUI text;
    public static TutorialWindow Instance;
    public string scrollAndQuestTutorial;
    public string itemAllocationTutorial;
    public string abilityUsageTutorial;
    public string itemDragTutorial;
    public string openPanelsTutorial;

    private void Awake()
    {
        Instance = this;
        scrollAndQuestTutorial = "Welcome to Necrolith. Throughout the game, NPCs will offer you quests. These NPCs have a !!! symbol above their head if they have a quest. If you have accepted the quest, the icon becomes a ?. Upon completing the quest, the ? will change color, indicating you may turn it in.\n\nIf you have trouble locating a quest giver, you may zoom in and out your camera with the mouse scroll wheel.";
        itemAllocationTutorial = "Before venturing forth, it's best to be prepared! You start the game with a few Health Potions. \n\nTo allocate potions to your keybinds (Q, W, E, R), right click a Potion Slot on the left of the Action Bar at the bottom of the screen. Then, left click the item in the menu that shows up. You may then press the associated keybind to use the potion.\n\n Certain NPCs will also offer to trade with you, so you can sell and buy items.";
        abilityUsageTutorial = "Upon leveling up, you may click on the '+' menu button to allocate points in the passive skills tree. Alternatively, you may choose to open the Character Panel to allocate the points in the Ability Shop. \n\nYou may right click an action bar slot at the bottom of the screen to open a menu, then left click an ability in that menu to assign it to that slot. It is then usable via the keybind associated with that slot.";
        itemDragTutorial = "You may click items in the inventory to equip them in the equip slots. You may also click and drag items from the inventory's Weapon and Armor tabs into the equip slots to equip gear. This is particularly useful if you want to dual wield. \n\nYou may also unequip gear by left clicking it in the Gameplay UI's equip slots in the upper left of the screen.";
        openPanelsTutorial = "You have received your first piece of equipment! To equip it, you need to open the Character Panel. This can be done by clicking on the red vertical menu buttons in the bottom right of the screen.\n\nIn order, these buttons are: Options, Character Panel, Quest Log.";
        HideCanvas();
    }

    public void ShowCanvas()
    {
        tutorialCanvas?.SetActive(true);
    }

    public void HideCanvas()
    {
        tutorialCanvas?.SetActive(false);
    }
}
