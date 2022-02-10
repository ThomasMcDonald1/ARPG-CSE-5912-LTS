using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;
    // Start is called before the first frame update
    InventorySlot[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        //inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
         UpdateUI();

    }
    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if( i  < inventory.items.Count)
            {
                Debug.Log(inventory.items[i].type + " index: "+i+" amount: " + inventory.items[i].amount);
                if (inventory.items[i].stackable)
                {
                    GameObject amount = slots[i].transform.GetChild(1).gameObject;
                    TextMeshProUGUI text = amount.GetComponent<TextMeshProUGUI>();
                    
                    text.SetText(inventory.items[i].amount.ToString());
                    amount.SetActive(true);

                }
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
