using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Ite : ScriptableObject
{

	new public string name = "New Item";    // Name of the item
	public Sprite icon = null;              // Item icon
	public bool showInInventory = true;
	public bool stackable;
	public int amount = 1;
	public GearTypes type;

	public enum GearTypes
	{
		Lance,
		Gloves,
		Shoulders,
		Belt,
		Shoes,
		Shield
		//All your characters gear slots, head, feet, weapon
	}
	// Called when the item is pressed in the inventory
	public virtual void Use()
	{
		// Use the item
		// Something may happen
		Debug.Log("using " + name);
	}

	// Call this method to remove the item from inventory
	//public void RemoveFromInventory()
	//{
	//	Inventory.instance.Remove(this);
	//}
	
}