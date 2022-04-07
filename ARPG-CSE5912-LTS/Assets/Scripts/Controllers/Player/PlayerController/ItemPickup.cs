using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public bool EnteredTrigger = false;

	public Ite item;   // Item to put in the inventory if picked up
	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			EnteredTrigger = true;
	}


	// When the player interacts with the item
	public void Update()
	{
		//Debug.Log("Inventory weapon amt: " + Inventory.instance.weaponItems.Count);
		if (EnteredTrigger)
		{
			PickUp();
			EnteredTrigger = false;
		}
	}

	// Pick up the item
	void PickUp()
	{
		//Debug.Log("Picking up " + item.name);
		switch (item.type)
		{
			case Ite.ItemType.armor:
				if(Inventory.instance.armorItems.Count >= Inventory.instance.space)
                {
					return;
                }
				Inventory.instance.Add(item, Inventory.instance.armorItems);
				break;
			case Ite.ItemType.weapon:
				if (Inventory.instance.weaponItems.Count >= Inventory.instance.space)
				{
					return;
				}
				Inventory.instance.Add(item, Inventory.instance.weaponItems);
				break;
			case Ite.ItemType.utility:
				if (Inventory.instance.utilItems.Count >= Inventory.instance.space)
				{
					return;
				}
				Inventory.instance.Add(item, Inventory.instance.utilItems);
				break;

		}

		Destroy(gameObject);    // Destroy item from scene
	}

}