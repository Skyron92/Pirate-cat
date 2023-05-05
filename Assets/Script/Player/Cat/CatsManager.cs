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

    private void Update() {
        Debug.Log(team.Count);
    }

    public void SetGame(CatsManager newCatsManager) {
        Name = newCatsManager.Name;
        gold = newCatsManager.gold;
        playerCat = new Cat();
        newCatsManager.playerCat.Replace(playerCat);
        hiredCats.Clear();
        hiredCats = newCatsManager.hiredCats;
        team.Clear();
        team = newCatsManager.team;
    }
}
