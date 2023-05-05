using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;
    [SerializeField] private List<Fur> furs = new List<Fur>();
    [SerializeField] private List<Material> clothes = new List<Material>();

    private void Awake() {
        Instance = this;
    }

    public void DressUp(Cat cat) {
        cat.Clothes = clothes[cat.clotheIndex];
        cat.FurCat = furs[cat.furIndex];
        cat.Hats[cat.hatIndex].SetActive(true);
        if(cat.EyepatchIndex != 3) cat.Eyepatches[cat.EyepatchIndex].SetActive(true);
        else {
            cat.Eyepatches[1].SetActive(true);
            cat.Eyepatches[2].SetActive(true);
        }
    }
}
