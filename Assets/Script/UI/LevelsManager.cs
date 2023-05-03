using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private int _levelIndex;
    [SerializeField] private List<GameObject> levelPrefab = new List<GameObject>();

    public void SetIndex(int index) {
        _levelIndex = index;
    }

    public void LoadLevel() {
        Instantiate(levelPrefab[_levelIndex]);
    }
}
