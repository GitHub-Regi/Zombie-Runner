using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreIntensity;
    [SerializeField] float restoreAngle;

    [SerializeField] AudioClip batterySound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<Flashlight>().RestoreLightIntensity(restoreIntensity);
            other.GetComponentInChildren<Flashlight>().RestoreLightAngle(restoreAngle);

            PlayPickupSound();

            Destroy(gameObject);
        }
    }

    void PlayPickupSound()
    {
        if (batterySound != null)
        {
            AudioSource.PlayClipAtPoint(batterySound, transform.position, 0.3f);
        }
    }
}
