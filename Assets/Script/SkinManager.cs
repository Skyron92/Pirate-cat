using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;
    [SerializeField] private List<Fur> furs = new List<Fur>();
    [SerializeField] private List<Material> clothes = new List<Material>();
    [SerializeField] private Fur redFur;
    [SerializeField] private Material redClothe;

    
    private List<int> _emotionList = new List<int>();
    [HideInInspector] public int normal = 0;
    [HideInInspector] public int colère = 1;
    [HideInInspector] public int tristesse = 2;
    [HideInInspector] public int peur = 3;
    [HideInInspector] public int surprise = 4;
    [HideInInspector] public int mort = 5;
    [HideInInspector] public int sommeil = 6;
    
    [Header("Expressions\b")] 
    [Space]
    [SerializeField] private List<Texture> leftEyePatch= new List<Texture>();
    [SerializeField] private List<Texture> rightEyepatch = new List<Texture>();
    [SerializeField] private List<Texture> bothEyepatch = new List<Texture>();
    [SerializeField] private List<Texture> noEyepatch = new List<Texture>();
    private List<List<Texture>> _allTextures = new List<List<Texture>>();

    private Renderer _renderer;

    private void Awake() {
        Instance = this;
        _emotionList.Add(normal);
        _emotionList.Add(colère);
        _emotionList.Add(tristesse);
        _emotionList.Add(peur);
        _emotionList.Add(surprise);
        _emotionList.Add(mort);
        _emotionList.Add(sommeil);
        _allTextures.Add(noEyepatch);
        _allTextures.Add(leftEyePatch);
        _allTextures.Add(rightEyepatch);
        _allTextures.Add(bothEyepatch);
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

    public void SetColorRed(Cat cat) {
        cat.Clothes = redClothe;
        cat.FurCat = redFur;
    }

    public void SetEmotion(Cat cat,int emotionIndex) {
        _renderer = cat.gameObject.GetComponent<Renderer>();
        _renderer.material.EnableKeyword("_DETAIL_MULX2");
        cat.Face.material.SetTexture("_DETAIL_MULX2", _allTextures[cat.EyepatchIndex][emotionIndex]);
    }
}