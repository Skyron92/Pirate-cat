using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMove : MonoBehaviour
{

    private CatsManager CatsManager => CatsManager.instance;
    [Header("Move Settings \b")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    private float speed;
    [Range(0, 100)] [SerializeField] private float rotationSpeed;
    private float angle;
    private float _inputMove;
    private bool decelerate;
    private float _inputRotation;
    [SerializeField] private GameObject catPrefab;

    [SerializeField] private List<Transform> catsPositions = new List<Transform>();

    private void Awake() {
        speed = 70;
        foreach (var cat in CatsManager.team) {
            speed += 3*cat.NavigateurStat;
        }

        for (int i = 0; i < CatsManager.team.Count; i++) {
            Cat cat = Instantiate(catPrefab, catsPositions[i]).GetComponent<Cat>();
            if(i == 0) CatsManager.playerCat.Replace(cat);
            else {
                CatsManager.team[i].Replace(cat);
            }
        }
    }

    private void Update() {
        WriteMove();
        WriteRotation();
    }

    public void ReadMove(InputAction.CallbackContext context) {
        _inputMove = context.ReadValue<float>();
        decelerate = context.canceled;
    }

    private void WriteMove() {
        _rigidbody.AddForce(transform.forward * _inputMove * speed * Time.deltaTime, ForceMode.Impulse);
        if(_rigidbody.velocity != transform.forward) _rigidbody.velocity = transform.forward * _inputMove; 
        if(decelerate) Decelerate();
    }

    private void Decelerate() {
        float deceleration = Mathf.Lerp(_rigidbody.velocity.z, 0, 0.1f);
        _rigidbody.velocity = new Vector3(0, 0, deceleration);
        if(_rigidbody.velocity.z < 0) _rigidbody.velocity = Vector3.zero;
    }

    public void ReadRotation(InputAction.CallbackContext context) {
        _inputRotation = context.ReadValue<float>();
        _animator.SetFloat("Side", _inputRotation);
    }

    private void WriteRotation() {
        angle += Time.deltaTime * _inputRotation * rotationSpeed;
        Quaternion rotation = Quaternion.Euler(0,angle,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 10f);
    }
}
