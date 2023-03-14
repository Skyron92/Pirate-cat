using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private Transform playersCatPosition;
    [SerializeField] private GameObject catPrefab;
    private Animator _animator;
    private float animationTimer;
    private bool isInAnimation;
    private Cat catEntity;
    public Cat _currentCat;
    public AITeamManager ennemy;
    public List<Cat> crew = CatsManager.Team;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button switchButton;

    private void Awake() {
        PutCat();
    }

    private void Update() {
        Die();
        DisableButton();
        TimeAnimation();
    }

    public void PutCat() {
        GameObject cat = Instantiate(catPrefab, playersCatPosition);
        catEntity = cat.GetComponent<Cat>();
        crew[0].SetNewCat(catEntity);
        _currentCat = crew[0];
        _animator = cat.GetComponent<Animator>();
    }

    public void Attack() {
        _animator.SetBool("IsAttacking", true);
        isInAnimation = true;
        ennemy.TakeDamage(_currentCat.EscrimeurStat);
    }

    public void TakeDamage(int damage) {
        _animator.SetBool("IsTakingDamage", true);
        isInAnimation = true;
        _currentCat.hp -= damage;
        //Debug.Log("Max : " + _currentCat.maxHp + ", damage : " + damage);
    }

    public void Die() {
        if(_currentCat.hp > 0) return;
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
        attackButton.interactable = !isInAnimation;
        switchButton.interactable = !isInAnimation;
    }
}
