using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIBoat : MonoBehaviour
{
    [Header("AI SETTINGS \b")] [SerializeField]
    private bool showFields;
    [Range(0, 30)] [SerializeField] private int fieldOfView;
    [Range(0, 30)] public int fieldOfDefeat;
    [Range(0, 30)] public int fieldOfEscape;
    [Range(0,100)] [SerializeField] private float speedRun;
    [Range(0,100)] [SerializeField] private float speedCalm;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform player;
    [SerializeField] private Animator _animator;
    public float DistanceWithPlayer => Vector3.Distance(transform.position, player.position);
    private Vector3 DirectionOfPlayer => player.transform.position - transform.position;
    private Vector3 target;
    private Vector3 DirectionPatroil => target - transform.position;
    private bool hasReachedDestination => Vector3.Distance(target, transform.position) < 1f;
    System.Random value = new System.Random();


    private void Update() {
        if(DistanceWithPlayer <= fieldOfView) RunAway();
        else Patroil();
        if (fieldOfDefeat >= fieldOfView) fieldOfDefeat = fieldOfView - 1;
    }

    private void OnDrawGizmos() {
        if(!showFields) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fieldOfView);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfDefeat);
    }

    private void RunAway() {
        if (DistanceWithPlayer < fieldOfDefeat) {
            Decelerate();
            RotateToFight();
            return;
        }
        _rigidbody.AddForce(transform.forward * speedRun * Time.deltaTime, ForceMode.Impulse);
        if(_rigidbody.velocity != transform.forward) _rigidbody.velocity = transform.forward * speedRun;
        if (transform.forward == -DirectionOfPlayer.normalized) return;
        float angle = Vector3.Angle(Vector3.forward, -DirectionOfPlayer.normalized);
        Quaternion rotation = Quaternion.Euler(0,angle,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.05f);
    }
    
    private void Decelerate() {
        float deceleration = Mathf.Lerp(_rigidbody.velocity.z, 0, 0.05f);
        _rigidbody.velocity = new Vector3(0, 0, deceleration);
        if(_rigidbody.velocity.z < 0) _rigidbody.velocity = Vector3.zero;
    }

    private void Patroil() {
        if(hasReachedDestination) SetDestination();
        _rigidbody.AddForce(transform.forward * speedCalm * Time.deltaTime, ForceMode.Impulse);
        if(_rigidbody.velocity != transform.forward) _rigidbody.velocity = transform.forward;
        if (transform.forward == DirectionPatroil.normalized) return;
        float angle = Vector3.Angle(Vector3.forward, DirectionPatroil.normalized);
        Quaternion rotation = Quaternion.Euler(0,angle,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.05f);
    }

    private void SetDestination() {
        Random.seed = DateTime.Now.Millisecond;
        float rangeX = value.Next(0, fieldOfView);
        float rangeZ = value.Next(0, fieldOfView);
        target = new Vector3(rangeX, 0, rangeZ);
    }

    private void RotateToFight() {
        float rangle = Vector3.Angle(Vector3.right, DirectionOfPlayer.normalized);
        float langle = Vector3.Angle(Vector3.left, DirectionOfPlayer.normalized);
        Quaternion rotation = new Quaternion();
        rotation = langle < rangle ? Quaternion.Euler(0,langle,0) : Quaternion.Euler(0,rangle,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.05f);
    }
}
