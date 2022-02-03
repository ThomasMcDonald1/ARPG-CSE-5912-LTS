using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemWorld : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject itemDropPrefab;
   
    private InventoryItems item;
    private SpriteRenderer spriteRenderer;

    //float x, y, z;
  
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
      
       
        //spawnItemByPlayer();
        //Debug.Log("1");
    }
    private void OnEnable()
    {
        //InputController.ClickCanceledEvent += OnClickCanceled;

    }
    private void OnClickCanceled(object sender, InfoEventArgs<RaycastHit> r)
    {
        SpawnItemWorld(r.info.point,item);
    }
    public  void  SpawnItemWorld(Vector3 position, InventoryItems item)
    {
        GameObject itemDrop = Instantiate(itemDropPrefab);
        itemDrop.transform.position = Camera.main.ScreenToWorldPoint(position);

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
