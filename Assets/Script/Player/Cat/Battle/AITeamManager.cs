using System.Collections.Generic;
using UnityEngine;

public class AITeamManager : MonoBehaviour {
    
    public TeamManager player;
    public bool IsYourTurn;
    [SerializeField] private Transform AIsCatPosition;
    public List<Cat> crew = new List<Cat>();
    public Cat _currentCat;
    private int currentIndex;
    private Animator _animator;
    private float animationTimer;
    private bool isInAnimation;
    private Cat _catEntity;
    [SerializeField] private GameObject catPrefab;
    private GameObject _currentCatGameObject;
    private float timer;
    private bool isSwitchingTurn;
    private bool _catIsDead;
    
    void Awake() {
        foreach (var cat in crew) {
            cat.maxHp = 15;
        }
        PutCat();
    }
    
    void Update() {
        Die();
        TimeAnimation();
        if (_catIsDead && !isInAnimation) {
            for (int i = 0; i < crew.Count; i++) {
                if (crew[i].hp <= 0) continue;
                SwitchCat(i);
                _catIsDead = false;
                return;
            }
        }
        if(!HasOneOrMoreCatAlive()) return;
        Attack();
        SwitchTurn();
    }
    
    public void PutCat() {
        _currentCatGameObject = Instantiate(catPrefab, AIsCatPosition);
        _catEntity = _currentCatGameObject.GetComponent<Cat>();
        crew[0].Replace(_catEntity);
        _currentCat = crew[0];
        currentIndex = 0;
        _animator = _currentCatGameObject.GetComponent<Animator>();
    }
    
    public void Attack() {
        if(!IsYourTurn) return;
        _animator.SetBool("IsAttacking", true);
        player.TakeDamage(_currentCat.EscrimeurStat);
        isInAnimation = true;
        IsYourTurn = !IsYourTurn;
        isSwitchingTurn = true;
    }

    public void TakeDamage(int damage) {
        _animator.SetBool("IsTakingDamage", true);
        _currentCat.hp -= damage;
        isInAnimation = true;
    }

    public void Die() {
        if(_currentCat.hp > 0) return;
        _catIsDead = true;
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

    private bool HasOneOrMoreCatAlive() {
        return crew.Exists(x => x.hp > 0);
    }
    
    void SwitchTurn() {
        if (!isSwitchingTurn) return;
        timer += Time.deltaTime;
        if (timer >= 2f) {
            player.IsYourTurn = true;
            isSwitchingTurn = false;
            timer = 0;
        }
    }

    private void SwitchCat(int index) {
        Destroy(_currentCatGameObject);
        _currentCatGameObject = Instantiate(catPrefab, AIsCatPosition);
        _catEntity = _currentCatGameObject.GetComponent<Cat>();
        crew[index].Replace(_catEntity);
        _currentCat = crew[index];
        _animator = _currentCatGameObject.GetComponent<Animator>();
        Debug.Log("New cat is " + _currentCat.Name);
    }
}
