using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] float hitPoints;

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        healthText.text = "Health : " + hitPoints;

        switch (hitPoints)
        {
            case <= 200 and > 125:
                healthText.color = Color.green;
                break;
            case <= 125 and > 50:
                healthText.color = Color.yellow;
                break;
            case <= 50:
                healthText.color = Color.red;
                break;
        }

        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
