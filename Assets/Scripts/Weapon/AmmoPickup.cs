using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);

            Destroy(gameObject);
        }
    }
}
