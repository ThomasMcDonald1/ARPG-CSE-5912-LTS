using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    

	
    public static void DropItem(Vector3 pos, Ite item)
    {
        Vector3 randomVector = new Vector3(pos.x + Random.Range(-5.0f, 5.0f), pos.y, pos.z + Random.Range(-5.0f, 5.0f));
        Instantiate(item.prefab, randomVector, Quaternion.identity);

    }

}