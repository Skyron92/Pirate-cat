using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Canon : MonoBehaviour
{
    [Header("PREFAB\b")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem poofEffect;
   /* public Cat Tireur;         En attendant de continuer cette phase...
    private float ReloadingTime => 8 / Tireur.CanonnierStat;*/
    private float ReloadingTime => 5;
    private float timer;
    private bool IsReloading;
    [Header("INTERFACE\b")]
    [SerializeField] private Slider ReloadingJauge;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button fireButton;
    [SerializeField] private Sprite FireButtonSprite, ReloadingButtonSprite;
    [SerializeField] private GameObject slider;
    [SerializeField] private Camera _camera;
    
    
    [Header("AUDIO\b")]
    [SerializeField] private AudioSource fireSFX;

    private void Update() {
        Reload();
        DisplayTime();
    }

    public void Fire() {
        if(IsReloading) return;
        if(BoatPlayer.IsEnded) return;
        GameObject instance = Instantiate(bullet, transform);
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(1,1,0) * Random.Range(3.8f, 5.2f), ForceMode.Impulse);
        _camera.DOShakePosition(0.3f, 0.2f);
        poofEffect.Play();
        fireSFX.Play();
        text.text = "Reloading...";
        IsReloading = true;
    }
    

    void Reload() {
        if(!IsReloading) return;
        timer += Time.deltaTime; {
            if (timer >= ReloadingTime) {
                IsReloading = false;
                text.text = "Fire !";
                timer = 0;
            }
        }
    }

    void DisplayTime() {
        slider.SetActive(IsReloading);
        ReloadingJauge.maxValue = ReloadingTime;
        ReloadingJauge.value = timer;
    }
}