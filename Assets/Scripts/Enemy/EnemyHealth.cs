using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints;

    ZombiesCounter zombiesCounter;

    bool isDead = false;

    public bool IsDead => isDead;

    void Start()
    {
        zombiesCounter = FindFirstObjectByType<ZombiesCounter>();
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");

        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        
        GetComponent<Animator>().SetTrigger("Death");
        
        zombiesCounter.KillZombie();
    }
}
