using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Combat;

public class EnemyKillChecker : MonoBehaviour
{
    private bool EnemyKnight;
    private bool Bandit;
    private bool EliteWarrior;
    private bool Stout;
    private bool Kilixis;
    private bool GithUb;

    private static EnemyKillChecker instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than 1 instance of the EnemyKillChecker found...");
        }
        instance = this;
    }

    private void Start()
    {
        EnemyKnight = false;
        Bandit = false;
        EliteWarrior = false;
        Stout = false;
        Kilixis = false;
        GithUb = false;
    }

    public void OnEnable()
    {
        Enemy.EnemyKillExpEvent += CheckEnemyDeath;
    }

    public void OnDisable()
    {
        Enemy.EnemyKillExpEvent -= CheckEnemyDeath;
    }

    public static EnemyKillChecker GetInstance()
    {
        return instance;
    }

    public void CheckEnemyDeath(object sender, InfoEventArgs<(int, int, string)> e)
    {
        switch (e.info.Item3)
        {
            case "EnemyKnight":
                Debug.Log("EnemyKnight killed from kill checker");
                EnemyKnight = true;
                break;
            case "Bandit":
                Debug.Log("Bandit killed from kill checker");

                Bandit = true;
                break;
            case "EliteWarrior":
                Debug.Log("EliteWarrior killed from kill checker");

                EliteWarrior = true;
                break;
            case "Stout":
                Debug.Log("Stout killed from kill checker");

                Stout = true;
                break;
            case "Kilixis":
                Debug.Log("Kilixis killed from kill checker");

                Kilixis = true;
                break;
            case "Gith'Ub":
                Debug.Log("GithUb killed from kill checker");

                GithUb = true;
                break;
            default:
                break;
        }
    }

    public bool KnightDead() { return EnemyKnight; }
    public bool BanditDead() { return Bandit; }
    public bool EliteWarriorDead() { return EliteWarrior; }
    public bool StoutDead() { return Stout; }
    public bool KilixisDead() { return Kilixis; }
    public bool GithUbDead() { return GithUb; }

}
