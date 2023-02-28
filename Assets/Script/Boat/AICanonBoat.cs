using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AICanonBoat : MonoBehaviour
{
    [Header("HP")] 
    private float currentHP;
    private float nextHP;
    [Range(1,10)] [SerializeField] private int maxHP;
    private bool hit => !(nextHP == currentHP);

    [Header("INTERFACE")] [SerializeField] private Slider sliderHP;

    private void Awake() {
        currentHP = maxHP;
        nextHP = currentHP;
    }

    private void Update() {
        DisplayHP();
        if (hit) {
            currentHP = Mathf.Lerp(currentHP, nextHP, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        TakeDamage();
    }

    private void TakeDamage() {
        nextHP--;
        if (nextHP < 1) {
            nextHP = 0;
            BoatPlayer.IsEnded = true;
        }
    }

    private void DisplayHP() {
        sliderHP.maxValue = maxHP;
        sliderHP.value = currentHP;
    }
}
