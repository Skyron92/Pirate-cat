using System;
using UnityEngine;

public class Cat : MonoBehaviour {
    public int NavigateurStat;
    public int CanonnierStat;
    public int EscrimeurStat;
    [HideInInspector] public GameObject Hat;
    [HideInInspector] public GameObject EyePatch;
    [HideInInspector] public Fur FurCat;
    [HideInInspector] public Material Clothes;
    [SerializeField] private MeshRenderer Body;
    [SerializeField] private MeshRenderer Face;
    [SerializeField] private MeshRenderer Ear;
    [SerializeField] private MeshRenderer LeftHand;
    [SerializeField] private MeshRenderer RightHand;
    [SerializeField] private MeshRenderer Tail;

    private void Update() {
        SwitchSkin();
    }

    void SwitchSkin() {
        Body.material = Clothes;
        Face.material = FurCat.Face;
        Ear.material = FurCat.Ear;
        LeftHand.material = FurCat.LeftHand;
        RightHand.material = FurCat.RightHand;
        Tail.material = FurCat.Tail;
    }
}