using UnityEngine;
using Random = UnityEngine.Random;

public class GoldUpdater : MonoBehaviour {
    [SerializeField] private LevelManager levelManager;

    public void Awake() {
        if (levelManager.goldWin == 0) levelManager.goldWin = Random.Range(50, 150);
        levelManager.Value = levelManager.goldWin;
    }
}