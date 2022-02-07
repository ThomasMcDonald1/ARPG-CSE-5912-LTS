using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region Singleton

	public static Inventory instance;

	void Awake()
	{
		instance = this;
	}

	#endregion
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;
	// Our current list of items in the inventory
	public List<Ite> items = new List<Ite>();
	public int space = 24;

	// Add a new item if enough room
	public void Add(Ite item)
	{
		Debug.Log("add was called");
		if (item.showInInventory)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return;
            }

            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }

	// Remove an item
	public void Remove(Ite item)
	{
		items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
