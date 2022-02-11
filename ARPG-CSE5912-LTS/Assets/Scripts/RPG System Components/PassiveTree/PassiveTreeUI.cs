using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveTreeUI : MonoBehaviour
{
    [SerializeField] Player player;
    private PassiveSkills passiveSkills;
    public GameObject skillNodes;
    private void Awake()
    {
        passiveSkills = new PassiveSkills(player);
        foreach(Transform child in skillNodes.transform)
        {
            Button btn = child.GetComponent<Button>();
            var something = btn.gameObject.transform.Find("Background");
            btn.onClick.AddListener(delegate { TaskOnClick(child.name, btn.gameObject.transform.Find("Background")); });
        }
    }
	void TaskOnClick(string name, Transform background){
        Debug.Log(name);
        passiveSkills.UnlockPassive(name, background);
	}
}
