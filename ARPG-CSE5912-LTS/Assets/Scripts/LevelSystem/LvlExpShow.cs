using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlExpShow : MonoBehaviour
{
    public Text lvlUIText;
    public Text lvlText;
    public Text expText;
    public Text nextExpText;
    public Text nextExpUIText;
    //public Text playerLevelText;
    public Slider expSlider;
    public Slider nextExpSlider;
    public LevelController levelController;

    void Awake()
    {
        Debug.Log("okk");
        //lvlText.text = "Level:" + levelController.LVL + "Total Exp:" + levelController.EXP + "Now Level Exp:" + LevelController.currentLevelExp(levelController.EXP,levelController.LVL) + "ToNext:"+ LevelController.currentLevelExpToNext(levelController.LVL);
        lvlUIText.text = "LV." + levelController.LVL;
        lvlText.text = "LV: " + levelController.LVL + "/ 99";
        //playerLevelText = "LV: " + levelController.LVL;
        expText.text = "EXP: " + levelController.EXP + "/ 999999";
        nextExpText.text = "EXP: " + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "/" + LevelController.currentLevelExpToNext(levelController.LVL);
        nextExpUIText.text = "EXP: " + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "/" + LevelController.currentLevelExpToNext(levelController.LVL);
        expSlider.value = levelController.TotalExperiencePercent;
        nextExpSlider.value = levelController.CurrentExperiencePercent;
    }


    void Update()
    {
        //lvlText.text = "Level:" + levelController.LVL + "Total Exp:" + levelController.EXP + "Now Level Exp:" + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "ToNext:" + LevelController.currentLevelExpToNext(levelController.LVL);
        lvlUIText.text = "LV." + levelController.LVL;
        lvlText.text = "LV: " + levelController.LVL + "/ 99";
        //playerLevelText = "LV: " + levelController.LVL;
        expText.text = "EXP: " + levelController.EXP + "/ 999999";
        nextExpText.text = "EXP: " + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "/" + LevelController.currentLevelExpToNext(levelController.LVL);
        nextExpUIText.text = "EXP: " + LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + "/" + LevelController.currentLevelExpToNext(levelController.LVL);
        expSlider.value = levelController.TotalExperiencePercent;
        nextExpSlider.value = levelController.CurrentExperiencePercent;
    }
}
