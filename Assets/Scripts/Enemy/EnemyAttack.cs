using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage;

    PlayerHealth target;

    void Start()
    {
        target = FindFirstObjectByType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (target == null) return;

        target.TakeDamage(damage);
        target.GetComponent<DisplayDamage>().ShowDamage();
    }
}
