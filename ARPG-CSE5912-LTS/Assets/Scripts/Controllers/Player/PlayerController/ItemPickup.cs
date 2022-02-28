using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public bool EnteredTrigger = false;

	public Ite item;
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
				Inventory.instance.Add(item, Inventory.instance.armorItems);
				break;
			case Ite.ItemType.weapon:
				Inventory.instance.Add(item, Inventory.instance.weaponItems);
				break;
			case Ite.ItemType.utility:
				Inventory.instance.Add(item, Inventory.instance.utilItems);
				break;

		}

		Destroy(gameObject);    // Destroy item from scene
	}

}