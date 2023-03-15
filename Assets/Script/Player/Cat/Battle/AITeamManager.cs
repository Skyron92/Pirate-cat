using System.Collections.Generic;
using UnityEngine;

public class AITeamManager : MonoBehaviour {
    public TeamManager player;
    public bool IsYourTurn;
    [SerializeField] private Transform AIsCatPosition;
    public List<Cat> crew = new List<Cat>();
    public Cat _currentCat;
    private Animator _animator;
    private float animationTimer;
    private bool isInAnimation;
    private Cat catEntity;
    [SerializeField] private GameObject catPrefab;
    private float timer;
    private bool isSwitchingTurn;
    void Awake()
    {
        PutCat();
    }
    
    void Update() {
        Die();
        TimeAnimation();
        Attack();
        SwitchTurn();
    }
    
    public void PutCat() {
        GameObject cat = Instantiate(catPrefab, AIsCatPosition);
        catEntity = cat.GetComponent<Cat>();
        crew[0].SetNewCat(catEntity);
        _currentCat = crew[0];
        _animator = cat.GetComponent<Animator>();
    }
    
    public void Attack() {
        if(!IsYourTurn) return;
        _animator.SetBool("IsAttacking", true);
        player.TakeDamage(_currentCat.EscrimeurStat);
        isInAnimation = true;
        IsYourTurn = !IsYourTurn;
        player.IsYourTurn = true;
        isSwitchingTurn = true;
    }

    public void TakeDamage(int damage) {
        _animator.SetBool("IsTakingDamage", true);
        _currentCat.hp -= damage;
        isInAnimation = true;
        Debug.Log("Max : " + _currentCat.maxHp + ", damage : " + damage);
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
    
    void SwitchTurn() {
        if (!isSwitchingTurn) return;
        timer += Time.deltaTime;
        if (timer >= 4f) {
            player.IsYourTurn = true;
            isSwitchingTurn = false;
            timer = 0;
        }
    }

    public void SwitchCat(int index) {
        _currentCat = crew[index];
    }
}
