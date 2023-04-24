using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class CatsManager : MonoBehaviour
{
    public static string Name;
    public string gameName => Name;
    public static Cat playerCat;
    public Cat PlayerCat => playerCat;
    public List<Cat> hiredCats;
   
    public List<Cat> team;
    public int gold;

    public CatsManager(string name) {
        Name = name;
    }

    private void Awake()
    {
        Debug.Log(team.Count);
        foreach (var VARIABLE in team)
        {
            Debug.Log(VARIABLE.Name);
        }
        Debug.Log(hiredCats.Count);
    }

    public void SetGame(CatsManager newCatsManager) {
        Name = newCatsManager.gameName;
        gold = newCatsManager.gold;
        hiredCats.Clear();
        foreach (Cat cat in newCatsManager.hiredCats) {
            hiredCats.Add(cat);
        }
        team.Clear();
        foreach (var cat in newCatsManager.team) {
            team.Add(cat);
        }
    }
}
