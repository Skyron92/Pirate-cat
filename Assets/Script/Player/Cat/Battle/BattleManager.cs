using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    [SerializeField] private TeamManager Player;
    [SerializeField] private AITeamManager Ennemy;
    void Awake() {
        Player.ennemy = Ennemy;
        Ennemy.player = Player;
    }
    
}
