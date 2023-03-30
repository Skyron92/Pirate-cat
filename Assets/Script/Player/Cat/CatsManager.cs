using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CatsManager : MonoBehaviour
{
    public static string Name;
    public static List<Cat> HiredCats = new List<Cat>();
    public static List<Cat> Team = new List<Cat>();
    public static int Gold = 50;

    private void Update()
    {
        Debug.Log(Team.Count);
    }
}
