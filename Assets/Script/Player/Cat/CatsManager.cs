using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class CatsManager : MonoBehaviour
{
    public static string Name;
    public static List<Cat> HiredCats = new List<Cat>();
    public List<Cat> hiredCats = new List<Cat>();
    public static List<Cat> Team = new List<Cat>();
    public List<Cat> team = new List<Cat>();
    public static int Gold;
    public string name;
    public int gold;

    private void Update() {
        Debug.Log(Team.Count);
        Debug.Log(HiredCats.Count);
        Name = name;
        Gold = gold;
        hiredCats = HiredCats;
        team = Team;
    }
    
    public void SetGame(CatsManager newCatsManager) {
        name = newCatsManager.name;
        gold = newCatsManager.gold;
        HiredCats.Clear();
        foreach (Cat cat in newCatsManager.hiredCats) {
            HiredCats.Add(cat);
        }
        Team.Clear();
        foreach (var cat in newCatsManager.team) {
            Team.Add(cat);
        }
    }
}
