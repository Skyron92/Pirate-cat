using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private Transform playersCatPosition;
    [SerializeField] private GameObject catPrefab;
    private GameObject _currentCatGameObject;
    private Animator _animator;
    private float _animationTimer;
    private bool _isInAnimation;
    private Cat _catEntity;
    public Cat currentCat;
    public AITeamManager ennemy;
    public bool IsYourTurn;
    private CatsManager CatsManager => CatsManager.instance;
    public List<Cat> Crew => CatsManager.team;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button switchButton;
    private float timer;
    private bool isSwitchingTurn;
    private bool _catIsDead;

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
        _currentCatGameObject = Instantiate(catPrefab, playersCatPosition);
        _catEntity = _currentCatGameObject.GetComponent<Cat>();
        Crew[0].Replace(_catEntity);
        currentCat = Crew[0];
        _animator = _currentCatGameObject.GetComponent<Animator>();
    }
    
    public void PutNewCat(int index) {
        Destroy(_currentCatGameObject);
        _currentCatGameObject = Instantiate(catPrefab, playersCatPosition);
        _catEntity = _currentCatGameObject.GetComponent<Cat>();
        Crew[index].Replace(_catEntity);
        currentCat = Crew[index];
        _animator = _currentCatGameObject.GetComponent<Animator>();
        IsYourTurn = !IsYourTurn;
        isSwitchingTurn = true;
        if (_catIsDead) {
            _catIsDead = false;
            return;
        }
        SwitchTurn();
    }

    public void Attack() {
        _animator.SetBool("IsAttacking", true);
        _isInAnimation = true;
        ennemy.TakeDamage(currentCat.EscrimeurStat);
        IsYourTurn = !IsYourTurn;
        isSwitchingTurn = true;
    }

    public void TakeDamage(int damage) {
        _animator.SetBool("IsTakingDamage", true);
        _isInAnimation = true;
        currentCat.hp -= damage;
    }

    public void Die() {
        if(currentCat.hp > 0) return;
        _catIsDead = true;
        _animator.SetBool("IsTakingDamage", false);
        _animator.SetBool("IsDead", true);
        _isInAnimation = true;
    }

    void TimeAnimation() {
        if (!_isInAnimation) return;
        _animationTimer += Time.deltaTime;
        if (_animationTimer >= _animator.GetCurrentAnimatorStateInfo(0).length) {
            _isInAnimation = false;
            _animationTimer = 0;
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
