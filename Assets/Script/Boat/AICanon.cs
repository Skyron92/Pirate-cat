using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AICanon : MonoBehaviour
{
    private bool IsReloading = true;
    [SerializeField] private float ReloadingTime;
    [Tooltip("Donner un temps différent aux deux cannons pour qu'ils tirent de façon désynchronisés.")] [SerializeField] private float timer;
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem poofEffect;
    
    [Header("AUDIO")]
    [SerializeField] private AudioSource fireSFX;


    void Update() {
        Fire();
        Reload();
    }
    
    public void Fire() {
        if(IsReloading) return;
        if(BoatPlayer.IsEnded) return;
        GameObject instance = Instantiate(bullet, transform);
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-1,1,0) * Random.Range(3.8f, 4.5f), ForceMode.Impulse);
        poofEffect.Play();
        fireSFX.Play();
        IsReloading = true;
    }
    
    void Reload() {
        if(!IsReloading) return;
        timer += Time.deltaTime; {
            if (timer >= ReloadingTime) {
                IsReloading = false;
                timer = 0;
            }
        }
    }
}
