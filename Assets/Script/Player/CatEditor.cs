using System;
using System.Collections.Generic;
using UnityEngine;

public class CatEditor : MonoBehaviour
{
    [SerializeField] private Cat cat;
    [SerializeField] private List<GameObject> hats = new List<GameObject>();
    [SerializeField] private List<GameObject> eyePatches = new List<GameObject>();
    [SerializeField] private List<Material> clothes = new List<Material>();
    [SerializeField] private List<Fur> furs = new List<Fur>();
    private int hatIndex;
    private int eyePatchesIndex;
    private int clothesIndex;
    private int furIndex;

    public void SwitchHat(int ChangeValue) {
        hatIndex += ChangeValue;
        if (hatIndex >= hats.Count) hatIndex = 0;
        if (hatIndex < 0) hatIndex = hats.Count - 1;
        if (ChangeValue < 0) {
            cat.Hats[hatIndex].SetActive(true);
            if (hatIndex + 1 >= hats.Count) cat.Hats[0].SetActive(false);
            else cat.Hats[hatIndex + 1].SetActive(false);
        }
        else {
            cat.Hats[hatIndex].SetActive(true);
            if (hatIndex - 1 < 0) cat.Hats[hats.Count - 1].SetActive(false);
            else cat.Hats[hatIndex - 1].SetActive(false);
        }
    }
    
    public void SwitchClothes(int ChangeValue) {
        clothesIndex += ChangeValue;
        if (clothesIndex >= clothes.Count) clothesIndex = 0;
        if (clothesIndex < 0) clothesIndex = clothes.Count - 1;
        cat.Clothes = clothes[clothesIndex];
    }
    
    public void SwitchFur(int ChangeValue) {
        furIndex += ChangeValue;
        if (furIndex >= furs.Count) furIndex = 0;
        if (furIndex < 0) furIndex = furs.Count - 1;
        cat.FurCat = furs[furIndex];
    }
    
    public void SwitchEyePatch(int ChangeValue) {
        eyePatchesIndex += ChangeValue;
        if (eyePatchesIndex >= eyePatches.Count) eyePatchesIndex = 0;
        if (eyePatchesIndex < 0) eyePatchesIndex = eyePatches.Count - 1;
        cat.EyePatch = eyePatches[eyePatchesIndex];
    }

}