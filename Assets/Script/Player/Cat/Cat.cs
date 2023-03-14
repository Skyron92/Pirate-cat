using System;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

    public int hp;
    public int maxHp;
    public int NavigateurStat;
    public int CanonnierStat;
    public int EscrimeurStat;
    public string Name;
    public List<GameObject> Hats = new List<GameObject>();
    public List<GameObject> Eyepatches = new List<GameObject>();
    public Fur FurCat;
    public Material Clothes;
    [SerializeField] private MeshRenderer Body;
    [SerializeField] private MeshRenderer Face;
    [SerializeField] private MeshRenderer Ear;
    [SerializeField] private MeshRenderer LeftHand;
    [SerializeField] private MeshRenderer RightHand;
    [SerializeField] private SkinnedMeshRenderer Tail;

    private void Awake() {
        hp = maxHp;
    }

    private void Update() {
        SwitchSkin();
    }

    void SwitchSkin() {
        Body.material = Clothes;
        if(FurCat == null) return;
        Face.material = FurCat.Face;
        Ear.material = FurCat.Ear;
        LeftHand.material = FurCat.LeftHand;
        RightHand.material = FurCat.RightHand;
        Tail.material = FurCat.Tail;
    }

    public void SetName(string name) {
        Name = name;
    }

    public void SetNewCat(Cat newCat) {
        newCat.NavigateurStat = NavigateurStat;
        newCat.CanonnierStat = CanonnierStat;
        newCat.EscrimeurStat = EscrimeurStat;
        newCat.Name = Name;
        newCat.FurCat = FurCat;
        newCat.Clothes = Clothes;
        for (int i = 0; i < Hats.Count; i++) {
            newCat.Hats[i].SetActive(Hats[i].activeSelf);
        }
        for (int i = 0; i < Eyepatches.Count; i++) {
            newCat.Eyepatches[i].SetActive(Eyepatches[i].activeSelf);
        }
    }
}