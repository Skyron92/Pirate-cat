using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    Resolution[] resolutions;
    private Dropdown dropdownMenu;

    private void Awake() {
        dropdownMenu = GetComponent<Dropdown>();
    }

    void Start() {
        resolutions = Screen.resolutions;
        dropdownMenu.onValueChanged.AddListener(delegate {
            Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height,
                Screen.fullScreen);
            label.text = ResToString(resolutions[dropdownMenu.value]);
        });
        for (int i = 0; i < resolutions.Length; i++) {
            if(dropdownMenu.options.Exists(x => x.text == ResToString(resolutions[i]))) continue;
            dropdownMenu.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
            dropdownMenu.options[i].text = ResToString(resolutions[i]);
            dropdownMenu.value = i;
        }
    }

    public void UpdateItemsName() {
        Transform content = GameObject.Find("Dropdown List").transform.GetChild(0).GetChild(0);
        for (int i = 0; i < content.childCount; i++) {
            if (i == 0) continue;
            Text text = content.GetChild(i).GetChild(2).GetComponent<Text>();
            text.text = ResToString(resolutions[i-1]);
        }
    }


    string ResToString(Resolution res) {
        return res.width + " x " + res.height;
    }
}

