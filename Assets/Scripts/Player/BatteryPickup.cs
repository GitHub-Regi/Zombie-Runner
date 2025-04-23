using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreIntensity;
    [SerializeField] float restoreAngle;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<Flashlight>().RestoreLightIntensity(restoreIntensity);
            other.GetComponentInChildren<Flashlight>().RestoreLightAngle(restoreAngle);

            Destroy(gameObject);
        }
    }
}
