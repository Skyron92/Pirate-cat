using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField] private CatsManager catsManager;
    public Transform viewportTransform;
    public List<Transform> teamPlacement = new List<Transform>();
    [HideInInspector] public List<CatSquare> currentTeam;
    [SerializeField] private GameObject catSquarePrefab;

    private void Awake()
    {
        for (int i =0; i < catsManager.team.Count; i++) {
            GameObject instance = Instantiate(catSquarePrefab, transform);
            instance.transform.position = teamPlacement[i].transform.position;
            CatSquare catSquare = instance.GetComponent<CatSquare>();
            catSquare.cat = catsManager.team[i];
        }

        foreach (var cat in catsManager.hiredCats) {
            if(catsManager.team.Contains(cat)) continue;
            Instantiate(catSquarePrefab, viewportTransform).GetComponent<CatSquare>().cat = cat;
        }
    }

    public void ValidateTeam() {
        catsManager.team.Clear();
        foreach (var catSquare in currentTeam) { 
            
            catsManager.team.Add(catSquare.cat);
        }
        catsManager.hiredCats.Clear();
        for (int i = 0; i < viewportTransform.childCount; i++) {
            CatSquare catSquare = viewportTransform.GetChild(i).GetComponent<CatSquare>(); 
            
            catsManager.hiredCats.Add(catSquare.cat);
        }
        
        Debug.Log(catsManager.team.Count);
    }
}