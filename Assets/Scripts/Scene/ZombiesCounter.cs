//using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ZombiesCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI zombiesText;
    [SerializeField] Transform enemies;

    EnemyHealth enemyHealth;

    int numberOfZombies;

    void Awake()
    {
        numberOfZombies = enemies.childCount;
        zombiesText.text = "Zombies alive : " + numberOfZombies;
    }

    void Start()
    {
        enemyHealth = FindFirstObjectByType<EnemyHealth>();
    }

    public void KillZombie()
    {
        numberOfZombies--;
        zombiesText.text = "Zombies alive : " + numberOfZombies;

        if (numberOfZombies == 0)
        {
            StartCoroutine(HandleVictoryWithDelay());
        }
    }

    IEnumerator HandleVictoryWithDelay()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<VictoryHandler>().HandleVictory();
    }
}
