using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    [SerializeField] private CatsManager _catsManager;
    [SerializeField] private Transform viewportTransform;
    [SerializeField] private List<Button> teamButtons = new List<Button>();


    public void ValidateTeam() {
        foreach (var button in teamButtons) {
            
        }
    }
}