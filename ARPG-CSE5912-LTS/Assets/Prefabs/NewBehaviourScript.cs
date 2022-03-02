using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Enemy = (GameObject)Instantiate(questPrefab, player.transform.position, Quaternion.identity);
        GameObject Enemy2 = (GameObject)Instantiate(questPrefab, player.transform.position + new Vector3(5,5,5), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
