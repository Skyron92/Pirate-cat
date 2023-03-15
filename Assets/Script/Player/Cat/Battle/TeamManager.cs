using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private Transform playersCatPosition;
    [SerializeField] private GameObject catPrefab;
    private Animator _animator;
    private float animationTimer;
    private bool isInAnimation;
    private Cat catEntity;
    public Cat currentCat;
    public AITeamManager ennemy;
    public bool IsYourTurn;
    public List<Cat> crew = CatsManager.Team;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button switchButton;
    private float timer;
    private bool isSwitchingTurn;

    private void Awake() {
        PutCat();
    }

    private void Update() {
        Die();
        DisableButton();
        TimeAnimation();
        SwitchTurn();
    }

    public void PutCat() {
        GameObject cat = Instantiate(catPrefab, playersCatPosition);
        catEntity = cat.GetComponent<Cat>();
        crew[0].SetNewCat(catEntity);
        currentCat = crew[0];
        _animator = cat.GetComponent<Animator>();
    }
    
    public void PutNewCat(int index) {
        // GameObject cat = Instantiate(catPrefab, playersCatPosition);
        // catEntity = cat.GetComponent<Cat>();
        crew[index].SetNewCat(catEntity);
        currentCat = crew[index];
        // _animator = cat.GetComponent<Animator>();
    }

    public void Attack() {
        _animator.SetBool("IsAttacking", true);
        isInAnimation = true;
        ennemy.TakeDamage(currentCat.EscrimeurStat);
        IsYourTurn = !IsYourTurn;
        isSwitchingTurn = true;
    }

    public void TakeDamage(int damage) {
        _animator.SetBool("IsTakingDamage", true);
        isInAnimation = true;
        currentCat.hp -= damage;
        //Debug.Log("Max : " + _currentCat.maxHp + ", damage : " + damage);
    }

    public void Die() {
        if(currentCat.hp > 0) return;
        _animator.SetBool("IsTakingDamage", false);
        _animator.SetBool("IsDead", true);
        isInAnimation = true;
    }

    void TimeAnimation() {
        if (!isInAnimation) return;
        animationTimer += Time.deltaTime;
        if (animationTimer >= _animator.GetCurrentAnimatorStateInfo(0).length) {
            isInAnimation = false;
            animationTimer = 0;
            _animator.SetBool("IsAttacking", false);
            _animator.SetBool("IsTakingDamage", false);
            _animator.SetBool("IsDead", false);
        }
    }

    void DisableButton() {
        attackButton.interactable = IsYourTurn;
        switchButton.interactable = IsYourTurn;
    }

    void SwitchTurn() {
        if (!isSwitchingTurn) return;
        timer += Time.deltaTime;
        if (timer >= 2f) {
            ennemy.IsYourTurn = true;
            isSwitchingTurn = false;
            timer = 0;
        }
    }
}
