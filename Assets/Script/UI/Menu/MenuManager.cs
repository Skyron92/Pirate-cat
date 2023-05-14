using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private bool isApplication;
    [SerializeField] private GameObject webglMenu;
    [SerializeField] private GameObject appMenu;

    [SerializeField] private CatsManager catsManager;

    [Header("Sound Settings\b")] 
    [SerializeField] private AudioMixer master;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [Header("General setting\b")] 
    [SerializeField] private TextMeshProUGUI version;

    [Header("Gold settings")]
    [SerializeField] private TextMeshProUGUI gold;


    void Start() {
        webglMenu.SetActive(!isApplication);
        appMenu.SetActive(isApplication);
        gold.text = catsManager.gold.ToString();
    }

    private void Awake() {
        SetProjectVersion();
    }


    public void Quit() {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void MasterVolume(float masterLvl) {
        master.SetFloat("GlobalVolume", masterLvl);
    }
    
    public void MusicVolume() {
        master.SetFloat("MusicVolume", musicSlider.value);
    }
    
    public void SFXVolume() {
        master.SetFloat("SFXVolume", soundSlider.value);
    }

    public void OnFullscreenToggleChanged(Toggle toggle) {
        Screen.fullScreen = toggle.isOn;
    }

    private void SetProjectVersion() {
        version.text = "Pirate Cat : Goldfin root - 2023 " + Application.version;
    }
}
