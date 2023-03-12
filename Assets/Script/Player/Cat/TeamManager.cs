using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private Transform playersCatPosition;
    [SerializeField] private Transform AIsCatPosition;
    [SerializeField] private GameObject catPrefab;
    private Cat catEntity;
    private Cat _currentCat;

    private void Awake() {
        PutCat();
    }
    
    

    public void PutCat() {
        GameObject cat = Instantiate(catPrefab, playersCatPosition);
        catEntity = cat.GetComponent<Cat>();
        CatsManager.Team[0].SetNewCat(catEntity);
    }
}
