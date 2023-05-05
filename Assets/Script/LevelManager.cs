using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> steps = new List<GameObject>();
    private int _currentIndex;

    [Header("Game Over Settings")] 
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private List<GameObject> gameOverTextList = new List<GameObject>();

    [Header("Player Settings")]
    //[SerializeField] private BoatMove boatMove;
    [SerializeField] private BoatMove boatMove;
    [SerializeField] private BoatPlayer boatPlayer;
    [SerializeField] private TeamManager teamManager;
    
    [Header("AI Settings")]
    [SerializeField] private AIBoat aiBoat;
    [SerializeField] private AICanonBoat aiCanonBoat;
    [SerializeField] private AITeamManager aiTeamManager;

    private void Awake() {
        _currentIndex = 0;
    }

    private void OnDestroy() {
        GameObject MenuItem = GameObject.Find("----------MENU---------------------");
        MenuItem.SetActive(true);
    }

    private void Update() {
        if(!boatMove.enabled) boatMove.enabled = true;
        VerifyCurrentStep();
        Debug.Log(_currentIndex);
    }

    private void VerifyCurrentStep() {
        if (CurrentStepIsLoose()) {
            GameOver();
            return;
        }
        if (CurrentStepIsEnded()) SwitchStep();
    }

    private bool CurrentStepIsEnded (){
        switch (_currentIndex)
        {
            case 0 :
                return aiBoat.DistanceWithPlayer < aiBoat.fieldOfDefeat;
            break;
            case 1 :
                return aiCanonBoat.currentHP <= 0;
                break;
            case 2 :
                return !aiTeamManager.crew.Exists(x => x.hp > 0);
                break;
            default: return false;
        }
    }

    private void SwitchStep() {
        steps[_currentIndex].SetActive(false);
        _currentIndex++;
        steps[_currentIndex].SetActive(true);
    }

    private bool CurrentStepIsLoose() {
        switch (_currentIndex) {
            case 0 :
                return aiBoat.DistanceWithPlayer >= aiBoat.fieldOfEscape;
            break;
            case 1 :
                return boatPlayer.currentHP <= 0;
            break;
            case 2 :
                return !teamManager.Crew.Exists(x => x.hp > 0);
            break;
            default: return false;
        }
    }

    private void GameOver() {
        for (int i = 0; i < gameOverTextList.Count; i++) {
            gameOverTextList[i].SetActive(i == _currentIndex);
        }
    }

    public void DestroyInstance() {
        Destroy(gameObject);
    }
}
