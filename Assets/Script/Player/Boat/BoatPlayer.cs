using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoatPlayer : MonoBehaviour 
{
    public static bool IsEnded;
    private CatsManager CatsManager => CatsManager.instance;
    [SerializeField] private Transform right, left;
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private Camera _camera;
    [Range(1,10)] public int maxHp;
    [HideInInspector] public float currentHP;
    private float nextHP;
    [SerializeField] private Slider sliderLife;
    private bool hit => !(nextHP == currentHP);
    [SerializeField] private Animator _animator;

    private void Awake() {
        _animator.SetBool("IsBattleShip", true);
        currentHP = maxHp;
        nextHP = currentHP;
        SetCats();
    }

    private void Update() {
        DisplayLife();
        if (hit) {
            currentHP = Mathf.Lerp(currentHP, nextHP, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        _camera.DOShakePosition(0.8f, 0.3f);
        nextHP--;
        if (nextHP < 1) {
            nextHP = 0;
            IsEnded = true;
        }
    }

    private void DisplayLife() {
        sliderLife.maxValue = maxHp;
        sliderLife.value = currentHP;
    }

    private void SetCats() {
        if (CatsManager.team[0] != null) {
            Cat cat = Instantiate(catPrefab, right.position, Quaternion.identity).GetComponent<Cat>();
            CatsManager.team[0].Replace(cat);
        }
        if (CatsManager.team[1] != null) {
            Cat cat = Instantiate(catPrefab, left.position, Quaternion.identity).GetComponent<Cat>();
            CatsManager.team[1].Replace(cat);
        }
    }
}
