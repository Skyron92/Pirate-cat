using System;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class CatSelection : MonoBehaviour {
    [Range(0,2)] [SerializeField] private int index;
    [SerializeField] private TeamManager player;
    private Button currentCatButton;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider life;

    private void Awake() {
        currentCatButton = GetComponent<Button>();
        life.maxValue = player.Crew[index].maxHp;
        life.value = player.Crew[index].hp;
        text.text = player.Crew[index].Name;
    }

    private void Update() {
        if (life.value == 0) currentCatButton.interactable = false;
    }

    public void SwitchCat() {
        player.PutNewCat(index);
    }
}
