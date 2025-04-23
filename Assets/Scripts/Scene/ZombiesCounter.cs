using UnityEngine;

public class ZombiesCounter : MonoBehaviour
{
    [SerializeField] Transform enemies;

    EnemyHealth enemyHealth;

    int numberOfZombies;

    void Awake()
    {
        numberOfZombies = enemies.childCount;
        Debug.Log(numberOfZombies);
    }

    void Start()
    {
        enemyHealth = FindFirstObjectByType<EnemyHealth>();
    }

    public void KillZombie()
    {
        numberOfZombies--;
        Debug.Log(numberOfZombies);

        if (numberOfZombies == 0)
        {
            Time.timeScale = 0;
        }
    }
}
