using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelShow : MonoBehaviour
{
    public Text lvlText;
    public LevelController levelController;

    void Awake()
    {
        Debug.Log("okk");
        lvlText.text = "Level:" + levelController.LVL + "Total Exp:" + levelController.EXP + "Now Level Exp:" + LevelController.currentLevelExp(levelController.EXP,levelController.LVL) + "ToNext:"+ LevelController.currentLevelExpToNext(levelController.LVL);
    }


    void Update()
    {
        lvlText.text = "Level:" + levelController.LVL + "Total Exp:" + levelController.EXP + "Now Level Exp:" + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "ToNext:" + LevelController.currentLevelExpToNext(levelController.LVL);
    }
}
