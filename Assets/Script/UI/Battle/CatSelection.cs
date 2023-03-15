using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class CatSelection : MonoBehaviour {
    [Range(0,2)] [SerializeField] private int index;
    [SerializeField] private TeamManager player;
    [SerializeField] private Button currentCatButton;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider life;

    private void Awake() {
        life.maxValue = player.crew[index].maxHp;
        life.value = player.crew[index].hp;
        currentCatButton.interactable = player.crew[index] != player.currentCat;
        text.text = player.crew[index].Name;
    }

    public void SwitchCat() {
        // player.currentCat.gameObject.SetActive(false);
        player.PutNewCat(index);
    }
}
