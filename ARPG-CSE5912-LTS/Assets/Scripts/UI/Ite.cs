using UnityEngine;


/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Ite : ScriptableObject
{
	new public string name = "New Item";    // Name of the item
	public Sprite icon = null;              // Item icon
	public bool showInInventory = true;
	public bool stackable;
	public ItemType type;
	[SerializeField] public GameObject prefab;
	public int attackDamage;
	public int defendRate;
	public string utilityUsage;
	public int cost;
	public Stats playerStat;


	//public int amount = 1;
	public virtual void Use()
	{
		if(name == "Health")
        {
			playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>();
			useHealingPotion();

		}
		// Use the item
		// Something may happen
		Debug.Log("using " + name);
	}
	public void useHealingPotion()
    {
		//Debug.Log("ADD HEALTH!!");

		playerStat[StatTypes.HP] += 1000;
		playerStat[StatTypes.HP] = Mathf.Clamp(playerStat[StatTypes.HP], 0, playerStat[StatTypes.MaxHP]);
	}
	public enum ItemType
	{
		weapon,
		armor,
		utility

	}

	// Call this method to remove the item from inventory
	public void RemoveFromInventory()
	{
		Debug.Log("remove from inventory was called");
		Inventory.instance.RemoveEquip(this);

	}

}
