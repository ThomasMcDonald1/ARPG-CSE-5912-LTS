using System.Collections;
using System.Collections.Generic;
using ARPG.Combat;
using UnityEngine;
using System;

public class ExperienceManager : MonoBehaviour
{
    public static event EventHandler<InfoEventArgs<(int, int)>> ExpWillBeGivenEvent;
    public static event EventHandler<InfoEventArgs<(int, int)>> ExpHasBeenGivenEvent;

    public static event EventHandler<InfoEventArgs<(int, int)>> SavedExpEvent;
    public static event EventHandler<InfoEventArgs<(int, int)>> GetBackExpEvent;
    public static event EventHandler<InfoEventArgs<(int, int)>> EmptyExpEvent;

    private void OnEnable()
    {
        Debug.Log("able");
        Enemy.EnemyKillExpEvent += OnEnemyExpKill;
        GameoverState.SaveExpEvent += OnSavedExp;
        GameoverState.GetBackExpEvent += OnGetBackExp;
        GameoverState.EmptyExpEvent += OnEmptyExp;
    }
    private void OnDisable()
    {
        Enemy.EnemyKillExpEvent -= OnEnemyExpKill;
        GameoverState.SaveExpEvent -= OnSavedExp;
        GameoverState.GetBackExpEvent -= OnGetBackExp;
        GameoverState.EmptyExpEvent -= OnEmptyExp;
    }
    public void OnEnemyExpKill(object sender, InfoEventArgs<(int, int)> e)
    {
        Debug.Log("enemy killed");
        ExpWillBeGivenEvent?.Invoke(this, new InfoEventArgs<(int, int)>(e.info));
        ExpHasBeenGivenEvent?.Invoke(this, new InfoEventArgs<(int, int)>(e.info));
    }
    public void OnSavedExp(object sender, InfoEventArgs<(int, int)> e)
    {
        Debug.Log("enemy killed");
        SavedExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>(e.info));
    }
    public void OnGetBackExp(object sender, InfoEventArgs<(int, int)> e)
    {
        Debug.Log("go back");
        GetBackExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>(e.info));
    }
    public void OnEmptyExp(object sender, InfoEventArgs<(int, int)> e)
    {
        Debug.Log("empty");
        EmptyExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>(e.info));
    }


}
