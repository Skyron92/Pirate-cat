using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private CatsManager catsManager;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private GameObject catPrefab;
    private Cat cat;
    [SerializeField] private List<Fur> furList = new List<Fur>();
    [SerializeField] private List<GameObject> hatList = new List<GameObject>();
    [SerializeField] private List<GameObject> eyePatchList = new List<GameObject>();
    [SerializeField] private List<Material> clothList = new List<Material>();

    [SerializeField] [Range(0, 500)] private int cost;

    public void UpdateCostDisplay(TextMeshProUGUI textMeshProUGUI) {
        textMeshProUGUI.text = cost.ToString();
    }

    public void UpdateGold(TextMeshProUGUI textMeshProUGUI) {
        textMeshProUGUI.text = catsManager.gold.ToString();
    }

    public void HireCat() {
        if (catsManager.gold >= cost) {
            GameObject newCat = Instantiate(catPrefab);
            cat = newCat.GetComponent<Cat>();
            cat.clotheIndex = new System.Random().Next(0, clothList.Count);
            int chance = Random.Range(0, 100);
            if (chance >= 95) cat.EyepatchIndex = 3;
            if(chance <= 15) cat.EyepatchIndex = new System.Random().Next(1, eyePatchList.Count);
            cat.hatIndex = new System.Random().Next(0, hatList.Count);
            cat.furIndex = new System.Random().Next(0, furList.Count);
            CreateStatistic();
            catsManager.gold -= cost;
        }
    }


    public void ValidateName(TextMeshProUGUI textMeshProUGUI) {
        cat.Name = textMeshProUGUI.text;
        Debug.Log(cat.Name);
        catsManager.hiredCats.Add(cat);
        dataManager.SaveCatsManager(catsManager);
    }

    private void CreateStatistic() {
        int total = 15;
        int firstStat = Random.Range(5, 10);
        total -= firstStat;
        int secondStat = Random.Range(1, 5);
        total -= secondStat;
        int thirdStat = total;

        int order = Random.Range(1, 3);
        int nextOrder = Random.Range(1, 2);
        switch (order) {
            case 1: {
                cat.NavigateurStat = firstStat;
                if (nextOrder == 1) {
                    cat.CanonnierStat = secondStat;
                    cat.EscrimeurStat = thirdStat;
                }
                else {
                    cat.CanonnierStat = thirdStat;
                    cat.EscrimeurStat = secondStat;
                }
                break;
            }
            case 2: {
                cat.CanonnierStat = firstStat;
                if (nextOrder == 1) {
                    cat.EscrimeurStat = secondStat;
                    cat.NavigateurStat = thirdStat;
                }
                else {
                    cat.EscrimeurStat = thirdStat;
                    cat.NavigateurStat = secondStat;
                }
                break;
            }
            case 3: {
                cat.EscrimeurStat = firstStat;
                if (nextOrder == 1) {
                    cat.NavigateurStat = secondStat;
                    cat.CanonnierStat = thirdStat;
                }
                else {
                    cat.NavigateurStat = thirdStat;
                    cat.CanonnierStat= secondStat;
                }
                break;
            }
        }
    }
}
