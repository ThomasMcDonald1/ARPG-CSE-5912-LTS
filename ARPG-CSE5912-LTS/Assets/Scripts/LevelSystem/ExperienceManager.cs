using System.Collections;
using System.Collections.Generic;
using ARPG.Combat;
using UnityEngine;
using System;

public class ExperienceManager : MonoBehaviour
{
    public static event EventHandler<InfoEventArgs<(int, int)>> ExpWillBeGivenEvent;
    public static event EventHandler<InfoEventArgs<(int, int)>> ExpHasBeenGivenEvent;

    private void OnEnable()
    {
        Debug.Log("able");
        Enemy.EnemyKillExpEvent += OnEnemyExpKill;
    }
    private void OnDisable()
    {
        Enemy.EnemyKillExpEvent -= OnEnemyExpKill;
    }
    public void OnEnemyExpKill(object sender, InfoEventArgs<(int, int)> e)
    {
        Debug.Log("enemy killed");
        ExpWillBeGivenEvent?.Invoke(this, new InfoEventArgs<(int, int)>(e.info));
        ExpHasBeenGivenEvent?.Invoke(this, new InfoEventArgs<(int, int)>(e.info));
    }


}
