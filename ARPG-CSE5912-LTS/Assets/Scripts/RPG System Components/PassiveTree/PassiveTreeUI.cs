using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveTreeUI : MonoBehaviour
{
    [SerializeField] Player player;
    private PassiveSkills passiveSkills;
    private void Awake()
    {
        passiveSkills = new PassiveSkills(player);
        foreach(Transform child in transform)
        {
            Button btn = child.GetComponent<Button>();
            var something = btn.gameObject.transform.Find("Background");
            Debug.Log(something);
            btn.onClick.AddListener(delegate { TaskOnClick(child.name, btn.gameObject.transform.Find("Background")); });
        }
    }
	void TaskOnClick(string name, Transform background){
        Debug.Log(name);
        passiveSkills.UnlockPassive(name, background);
	}
}
