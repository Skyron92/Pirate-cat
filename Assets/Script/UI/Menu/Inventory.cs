using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField] private CatsManager catsManager;
    [SerializeField] private DataManager dataManager;
    public Transform viewportTransform;
    public List<Transform> teamPlacement = new List<Transform>();
    [HideInInspector] public List<CatSquare> currentTeam;
    [HideInInspector] public List<CatSquare> stock;
    [SerializeField] private GameObject catSquarePrefab;

    private void Awake()
    {
        if (catsManager.team.Count > 0) {
            for (int i = 0; i < catsManager.team.Count; i++) {
                GameObject instance = Instantiate(catSquarePrefab, transform);
                instance.transform.position = teamPlacement[i].transform.position;
                CatSquare catSquare = instance.GetComponent<CatSquare>();
                catSquare.cat = catsManager.team[i];
                currentTeam.Add(catSquare);
            }
        }

        if (catsManager.hiredCats.Count > 0) {
            foreach (var cat in catsManager.hiredCats) {
                GameObject instance = Instantiate(catSquarePrefab, viewportTransform);
                CatSquare catSquare = instance.GetComponent<CatSquare>();
                catSquare.cat = cat;
                stock.Add(catSquare);
            }
        }
    }

    public void ValidateTeam() {
        catsManager.team.Clear();
        foreach (var catSquare in currentTeam) {
            catsManager.team.Add(catSquare.cat);
        }
        catsManager.hiredCats.Clear();
        foreach (var catSquare in stock) {
            catsManager.hiredCats.Add(catSquare.cat);
        }
        dataManager.SaveCatsManager(catsManager);
    }
}