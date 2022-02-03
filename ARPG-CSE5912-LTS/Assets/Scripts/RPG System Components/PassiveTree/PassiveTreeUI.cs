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
            btn.onClick.AddListener(delegate { TaskOnClick(child.name); });
        }
    }
	void TaskOnClick(string name){
        Debug.Log(name);
        passiveSkills.UnlockPassive(name);
	}
}
