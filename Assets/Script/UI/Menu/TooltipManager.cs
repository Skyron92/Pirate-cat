using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField] private TextMeshProUGUI catName;
    [SerializeField] private Slider navigationSlider;
    [SerializeField] private Slider canonSlider;
    [SerializeField] private Slider saberSlider;

    void Awake() {
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        else Instance = this;
    }


    private void Start() {
        gameObject.SetActive(false);
    }

    void Update() {
        transform.position = Input.mousePosition;
    }

    public void ShowToolTip(Cat cat) {
        gameObject.SetActive(true);
        catName.text = cat.Name;
        navigationSlider.value = cat.NavigateurStat;
        canonSlider.value = cat.CanonnierStat;
        saberSlider.value = cat.EscrimeurStat;
    }

    public void HideToolTip() {
        gameObject.SetActive(false);
    }
}