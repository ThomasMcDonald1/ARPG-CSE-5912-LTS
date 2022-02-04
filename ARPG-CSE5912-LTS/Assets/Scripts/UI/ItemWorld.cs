using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemWorld : MonoBehaviour
{

    //[SerializeField] GameObject player;
    //[SerializeField] GameObject itemDropPrefab;
    [SerializeField] GameObject img;

    private InventoryItems item;
    private SpriteRenderer spriteRenderer;

    //float x, y, z;
  
    private void Awake()
    {
        spriteRenderer = img.gameObject.GetComponent<SpriteRenderer>();
     // item = gameObject.GetComponent<img>()
        //spawnItemByPlayer();
        //Debug.Log("1");
    }
    private void OnEnable()
    {
        //InputController.ClickCanceledEvent += OnClickCanceled;

    }
   
    public static void  SpawnItemWorld(Vector3 position, InventoryItems item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.setItem(item);

    }
   
    public void setItem(InventoryItems item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }
    public InventoryItems getItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }
   
    
}
