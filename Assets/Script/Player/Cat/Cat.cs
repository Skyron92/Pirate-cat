using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cat : MonoBehaviour {

    [HideInInspector] public int hp;
    public int maxHp = 15;
    [HideInInspector] public int NavigateurStat;
    [HideInInspector] public int CanonnierStat;
    [HideInInspector] public int EscrimeurStat;
    public string Name;
    public List<GameObject> Hats = new List<GameObject>();
    [HideInInspector] public int hatIndex;
    public List<GameObject> Eyepatches = new List<GameObject>();
    [HideInInspector] public int EyepatchIndex;
    public Fur FurCat;
    [HideInInspector] public int furIndex;
    public Material Clothes;
    [HideInInspector] public int clotheIndex;
    [SerializeField] private MeshRenderer Body;
    [SerializeField] private MeshRenderer Face;
    [SerializeField] private MeshRenderer Ear;
    [SerializeField] private MeshRenderer LeftHand;
    [SerializeField] private MeshRenderer RightHand;
    [SerializeField] private SkinnedMeshRenderer Tail;

    public int CatID => furIndex & clotheIndex & hatIndex & EyepatchIndex & NavigateurStat & CanonnierStat &
                        EscrimeurStat;

    private void Awake() {
        hp = maxHp;
        for(int i = 0; i < Hats.Count; i++) Hats[i].SetActive(i == hatIndex);
        for(int i = 0; i < Eyepatches.Count; i++) Eyepatches[i].SetActive(i == EyepatchIndex);
        SetNewCat(this);
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
        newCat.furIndex = furIndex;
        newCat.clotheIndex = clotheIndex;
        newCat.hatIndex = hatIndex;
        newCat.EyepatchIndex = EyepatchIndex;
        newCat.Clothes = Clothes;
        for (int i = 0; i < Hats.Count; i++) {
            newCat.Hats[i].SetActive(Hats[i].activeSelf);
        }
        for (int i = 0; i < Eyepatches.Count; i++) {
            newCat.Eyepatches[i].SetActive(Eyepatches[i].activeSelf);
        }
    }
}