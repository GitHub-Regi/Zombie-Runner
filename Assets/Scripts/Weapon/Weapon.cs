using System;
using Mono.Cecil;
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
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float fireRate;

    StarterAssetsInputs inputs;

    float nextTimeToFire = 0f;

    void Start()
    {
        inputs = FindFirstObjectByType<StarterAssetsInputs>();
        ammoSlot = GetComponentInParent<Ammo>();

        if (inputs == null)
        {
            Debug.LogError("StarterAssetsInputs not found in scene.");
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
        ammoText.text = currentAmmo.ToString();
    }

    void Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();

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
