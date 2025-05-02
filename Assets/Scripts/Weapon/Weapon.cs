//using System;
//using Mono.Cecil;
using StarterAssets;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] Camera POVCamera;
    [SerializeField] GameObject hitVFX;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource[] audioSourcePool;
    [SerializeField] int audioSourcePoolSize;
    [SerializeField] AudioClip gunSound;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float fireRate;

    StarterAssetsInputs inputs;

    float nextTimeToFire = 0f;
    int currentAudioIndex = 0;

    void Start()
    {
        inputs = FindFirstObjectByType<StarterAssetsInputs>();
        ammoSlot = GetComponentInParent<Ammo>();

        if (inputs == null)
        {
            Debug.LogError("StarterAssetsInputs not found in scene.");
        }

        audioSourcePool = new AudioSource[audioSourcePoolSize];

        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            GameObject audioObj = new GameObject("GunshotAudioSource_" + i);
            audioObj.transform.parent = transform;
            audioObj.transform.localPosition = Vector3.zero;
            audioSourcePool[i] = audioObj.AddComponent<AudioSource>();
            audioSourcePool[i].spatialBlend = 1f; 
        }
    }

    void Update()
    {
        DisplayAmmo();
        
        if (inputs != null && inputs.fire && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = "Ammo : " + currentAmmo.ToString();
    }

    void Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            PlayGunshotSound();

            ammoSlot.ReduceCurrentAmmo(ammoType);
            
            bool flowControl = ProcessRaycast();

            if (!flowControl)
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void PlayGunshotSound()
    {
        if (gunSound != null)
        {
            AudioSource currentSource = audioSourcePool[currentAudioIndex];

            if (ammoType == AmmoType.Five_seveN_Bullets)
            {
                currentSource.PlayOneShot(gunSound, 0.1f);
            }
            else if (ammoType == AmmoType.AK47_Bullets)
            {
                currentSource.PlayOneShot(gunSound, 0.03f);
            }
            else if (ammoType == AmmoType.M16A1_Bullets)
            {
                currentSource.PlayOneShot(gunSound, 0.05f);
            }

            currentAudioIndex = (currentAudioIndex + 1) % audioSourcePoolSize;
        }
    }

    bool ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(POVCamera.transform.position, POVCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target == null) return false;

            target.TakeDamage(damage);
        }
        else
        {
            return false;
        }

        return true;
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);
    }
}
