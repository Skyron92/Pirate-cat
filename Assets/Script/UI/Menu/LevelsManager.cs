using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private int _levelIndex;
    [SerializeField] private List<GameObject> levelPrefab = new List<GameObject>();
    public static LevelsManager Instance;

    private void Awake() {
        Instance = this;
    }

    public void SetIndex(int index) {
        _levelIndex = index;
    }

    public void ReturnToMenu() {
        _levelIndex = 0;
        levelPrefab[0].SetActive(true);
    }

    public void LoadLevel() {
        Instantiate(levelPrefab[_levelIndex]);
    }
}
