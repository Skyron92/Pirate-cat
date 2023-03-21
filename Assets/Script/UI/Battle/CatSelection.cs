using System;
using TMPro;
using UnityEngine;
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
        text.text = player.crew[index].Name;
    }

    private void Update() {
        currentCatButton.interactable = player.crew[index] != player.currentCat;
        life.value = player.crew[index].hp;
    }

    public void SwitchCat() {
        player.currentIndex = index;
        player.PutNewCat(index);
    }
}
