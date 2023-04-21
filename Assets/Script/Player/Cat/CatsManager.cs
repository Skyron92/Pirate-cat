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
    public static List<Cat> HiredCats = new List<Cat>();
    public List<Cat> hiredCats => HiredCats;
    public static List<Cat> Team = new List<Cat>();
    public List<Cat> team => Team;
    public static int Gold;

    public int gold => Gold;

    public CatsManager(string name) {
        Name = name;
    }
    public void SetGame(CatsManager newCatsManager) {
        Name = newCatsManager.gameName;
        Gold = newCatsManager.gold;
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
