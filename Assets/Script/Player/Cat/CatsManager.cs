using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        Debug.Log(Team.Count);
        Debug.Log(HiredCats.Count);
        Name = name;
        Gold = gold;
        hiredCats = HiredCats;
        team = Team;
    }
}
