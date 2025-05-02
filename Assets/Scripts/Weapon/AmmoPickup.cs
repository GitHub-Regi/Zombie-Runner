//using System;
//using Unity.VisualScripting;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount;

    [SerializeField] AudioClip ammoSound;

    WeaponSwitcher weaponSwitcher;

    void Start()
    {
        weaponSwitcher = FindFirstObjectByType<WeaponSwitcher>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);

            PlayPickupSound();

            Destroy(gameObject);
        }
    }

    void PlayPickupSound()
    {
        if (ammoSound != null)
        {
            AudioSource.PlayClipAtPoint(ammoSound, weaponSwitcher.transform.position, 0.1f);
        }
    }
}
