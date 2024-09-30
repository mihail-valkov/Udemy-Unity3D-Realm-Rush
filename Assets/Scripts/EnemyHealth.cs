
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;

    public ObjectPool Pool { get; internal set; }

    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        health = 5;
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            enemy.RewardOnDestroy();
            gameObject.SetActive(false);
        }
    }
}
