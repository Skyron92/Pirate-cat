using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem BoomEffect;
    private bool isPlaying => BoomEffect.isPlaying;
    private float timer;

    private void Update() {
        if (isPlaying) timer += Time.deltaTime;
        if (timer >= 1f) {
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other) {
        BoomEffect.Play();
    }
}
