using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CatEditor : MonoBehaviour
{
    [Header("Personalisation Asset")]
    [SerializeField] private Cat cat;
    [SerializeField] private List<GameObject> hats = new List<GameObject>();
    [SerializeField] private List<GameObject> eyePatches = new List<GameObject>();
    [SerializeField] private List<Material> clothes = new List<Material>();
    [SerializeField] private List<Fur> furs = new List<Fur>();
    private int _hatIndex;
    private int _eyePatchesIndex;
    private int _clothesIndex;
    private int _furIndex;

    public void SwitchHat(int changeValue) {
        _hatIndex += changeValue;
        if (_hatIndex >= hats.Count) _hatIndex = 0;
        if (_hatIndex < 0) _hatIndex = hats.Count - 1;
        if (changeValue < 0) {
            cat.Hats[_hatIndex].SetActive(true);
            if (_hatIndex + 1 >= hats.Count) cat.Hats[0].SetActive(false);
            else cat.Hats[_hatIndex + 1].SetActive(false);
        }
        else {
            cat.Hats[_hatIndex].SetActive(true);
            if (_hatIndex - 1 < 0) cat.Hats[hats.Count - 1].SetActive(false);
            else cat.Hats[_hatIndex - 1].SetActive(false);
        }

        cat.hatIndex = _hatIndex;
    }
    
    public void SwitchClothes(int changeValue) {
        _clothesIndex += changeValue;
        if (_clothesIndex >= clothes.Count) _clothesIndex = 0;
        if (_clothesIndex < 0) _clothesIndex = clothes.Count - 1;
        cat.Clothes = clothes[_clothesIndex];
        cat.clotheIndex = _clothesIndex;
    }
    
    public void SwitchFur(int changeValue) {
        _furIndex += changeValue;
        if (_furIndex >= furs.Count) _furIndex = 0;
        if (_furIndex < 0) _furIndex = furs.Count - 1;
        cat.FurCat = furs[_furIndex];
        cat.furIndex = _furIndex;
    }
    
    public void SwitchEyePatch(int changeValue) {
        _eyePatchesIndex += changeValue;
        if (_eyePatchesIndex >= eyePatches.Count) _eyePatchesIndex = 0;
        if (_eyePatchesIndex < 0) _eyePatchesIndex = eyePatches.Count - 1;
        if (changeValue < 0) {
            cat.Eyepatches[_eyePatchesIndex].SetActive(true);
            if (_eyePatchesIndex + 1 >= eyePatches.Count) cat.Eyepatches[0].SetActive(false);
            else cat.Eyepatches[_eyePatchesIndex + 1].SetActive(false);
        }
        else {
            cat.Eyepatches[_eyePatchesIndex].SetActive(true);
            if (_eyePatchesIndex - 1 < 0) cat.Eyepatches[eyePatches.Count - 1].SetActive(false);
            else cat.Eyepatches[_eyePatchesIndex - 1].SetActive(false);
        }

        cat.EyepatchIndex = _eyePatchesIndex;
    }

    public void ValidateCat() {
        CatsManager.Team.Add(cat);
    }

}