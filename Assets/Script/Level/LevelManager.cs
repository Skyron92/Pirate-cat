using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> steps = new List<GameObject>();
    private int _currentIndex;
    private CatsManager CatsManager => CatsManager.instance;
    private LevelsManager LevelsManager => LevelsManager.Instance;

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

    [Header("Level Win settings")] 
    [SerializeField] private TextMeshProUGUI goldWonTMP;
    public int goldWin;
    private int _countFPS = 30;
    private float _duration = 1f;
    private int _value;

    public int Value {
        get {
            return _value;
        }
        set {
            UpdateNumber(value);
            _value = value;
        }
    }

    private Coroutine _countingCoroutine;


    private void Awake() {
        _currentIndex = 0;
    }

    private void OnDestroy() {
        LevelsManager.ReturnToMenu();
    }

    private void Update() {
        if(!boatMove.enabled) boatMove.enabled = true;
        VerifyCurrentStep();
        Debug.Log(CurrentStepIsLoose());
    }

    private void VerifyCurrentStep() {
        if (CurrentStepIsLoose()) {
            GameOver();
            return;
        }
        if (CurrentStepIsEnded()) SwitchStep();
    }

    private bool CurrentStepIsEnded (){
        switch (_currentIndex) {
            case 0 :
                return aiBoat.DistanceWithPlayer < aiBoat.fieldOfDefeat;
            case 1 :
                return aiCanonBoat.currentHP <= 0.1;
            case 2 :
                return !aiTeamManager.crew.Exists(x => x.hp > 0);
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
            case 1 :
                return boatPlayer.currentHP <= 0;
            case 2 :
                return !teamManager.Crew.Exists(x => x.hp > 0);
            default: return false;
        }
    }

    private void GameOver() {
        gameOverPanel.SetActive(true);
        for (int i = 0; i < gameOverTextList.Count; i++) {
            gameOverTextList[i].SetActive(i == _currentIndex);
        }
    }

    public void DestroyInstance() {
        if (_currentIndex == 3) CatsManager.gold += Value;
        Destroy(gameObject);
    }
    
    //Cette partie sert à l'affichage animé de l'or gagné
    
    private void UpdateNumber(int value) {
        if (_countingCoroutine != null) {
            StopCoroutine(_countingCoroutine);
        }
        _countingCoroutine = StartCoroutine(CountText(value));
    }

    private IEnumerator CountText(int value)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f / _countFPS);
        int previousValue = _value;
        int stepAmount;
        
        stepAmount = value - previousValue < 0
            ? Mathf.FloorToInt((value - previousValue) / (_countFPS * _duration))
            : Mathf.CeilToInt((value - previousValue) / (_countFPS * _duration));

        if (previousValue < value) {
            while (previousValue < value) {
                previousValue += stepAmount;
                if (previousValue > value) previousValue = value;

                goldWonTMP.SetText(previousValue.ToString("N"));
                
                yield return waitForSeconds;
            }
        }
        else {
            while (previousValue > value) {
                previousValue += stepAmount;
                if (previousValue < value) previousValue = value;

                goldWonTMP.SetText(previousValue.ToString("N"));
                
                yield return waitForSeconds;
            }
        }
        
    }
}