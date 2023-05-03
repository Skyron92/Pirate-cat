using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CatsManager : MonoBehaviour
{
    public static CatsManager instance = new CatsManager();
    public string Name;
    public Cat playerCat;
    public Cat PlayerCat => playerCat;
    public List<Cat> hiredCats;
   
    public List<Cat> team;
    public int gold;

    public CatsManager(string name) {
        Name = name;
    }

    public CatsManager() {
        
    }

    private void Awake() {
        instance = this;
    }


    private void Update()
    {
        Debug.Log("Team : " + team.Count);
        foreach (var VARIABLE in team)
        {
            Debug.Log(VARIABLE.Name);
        }
        Debug.Log("Stock : "+hiredCats.Count);
        foreach (var VARIABLE in hiredCats)
        {
            Debug.Log(VARIABLE.Name);
        }
    }

    public void SetGame(CatsManager newCatsManager) {
        Name = newCatsManager.Name;
        gold = newCatsManager.gold;
        hiredCats.Clear();
        hiredCats = newCatsManager.hiredCats;
        team.Clear();
        team = newCatsManager.team;
    }
}
