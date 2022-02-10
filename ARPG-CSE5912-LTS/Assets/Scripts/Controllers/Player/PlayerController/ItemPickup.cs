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
		if (EnteredTrigger) {
			PickUp();
			EnteredTrigger = false;
		}
	}

	// Pick up the item
	void PickUp()
	{
		//Debug.Log("Picking up " + item.name);
		Inventory.instance.Add(item);   // Add to inventory

		Destroy(gameObject);    // Destroy item from scene
	}

}