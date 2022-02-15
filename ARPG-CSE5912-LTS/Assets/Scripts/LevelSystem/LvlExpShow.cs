using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlExpShow : MonoBehaviour
{
    public Text lvlText;
    public Text expText;
    public Text nextExpText;
    //public Text playerLevelText;
    public LevelController levelController;

    void Awake()
    {
        Debug.Log("okk");
        //lvlText.text = "Level:" + levelController.LVL + "Total Exp:" + levelController.EXP + "Now Level Exp:" + LevelController.currentLevelExp(levelController.EXP,levelController.LVL) + "ToNext:"+ LevelController.currentLevelExpToNext(levelController.LVL);
        lvlText.text = "LV: " + levelController.LVL + "/ 99";
        //playerLevelText = "LV: " + levelController.LVL;
        expText.text = "EXP: " + levelController.EXP + "/ 999999";
        nextExpText.text = "EXP: " + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "/" + LevelController.currentLevelExpToNext(levelController.LVL);
    }


    void Update()
    {
        //lvlText.text = "Level:" + levelController.LVL + "Total Exp:" + levelController.EXP + "Now Level Exp:" + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "ToNext:" + LevelController.currentLevelExpToNext(levelController.LVL);
        lvlText.text = "LV: " + levelController.LVL + "/ 99";
        //playerLevelText = "LV: " + levelController.LVL;
        expText.text = "EXP: " + levelController.EXP + "/ 999999";
        nextExpText.text = "EXP: " + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "/" + LevelController.currentLevelExpToNext(levelController.LVL);
    }
}
