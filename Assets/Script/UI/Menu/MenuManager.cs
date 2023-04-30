using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private bool isApplication;
    [SerializeField] private GameObject webglMenu;
    [SerializeField] private GameObject appMenu;

    [Header("Sound Settings\b")] 
    [SerializeField] private AudioMixer master;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [Header("Graphic Setttings \b")]

    private List<string> fullscreenOptions = new List<string>();

    void Start() {
        webglMenu.SetActive(!isApplication);
        appMenu.SetActive(isApplication);
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    public void Quit() {
       /* if (Application.isEditor) EditorApplication.isPlaying = false;
        else Application.Quit();*/
       Application.Quit();
    }

    

    public void MasterVolume(float masterLvl) {
        master.SetFloat("GlobalVolume", masterLvl);
    }
    
    public void MusicVolume() {
        master.SetFloat("MusicVolume", musicSlider.value);
    }
    
    public void SFXVolume(float SFXLvl) {
        master.SetFloat("SFXVolume", soundSlider.value);
    }

    public void OnFullscreenOptionSelectionChanged(int index) {
        switch (index) {
            case 0 :
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1 :
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
}
