
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float shootingRange = 15f;

    GameObject target;

    private void Start() 
    {
        SetTarget(FindObjectOfType<EnemyMover>());
    }

    // Update is called once per frame
    void Update()
    {
        FindClosesEnemy();
        AimWeapon();
    }

    private void FindClosesEnemy()
    {
        if (target && target.activeInHierarchy) 
        { 
            return; 
        }

        EnemyMover[] enemies = FindObjectsOfType<EnemyMover>();
        if (enemies.Length == 0) { return; }

        EnemyMover closestEnemy = enemies[0];

        foreach (EnemyMover testEnemy in enemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy);
        }

        if (Vector3.Distance(transform.position, closestEnemy.transform.position) < shootingRange)
            SetTarget(closestEnemy);
        else
             SetTarget(null);

    }

    public void SetTarget(EnemyMover enemy)
    {
        if (enemy)
            target = enemy.gameObject;
        else
            target = null;
    }

    private EnemyMover GetClosest(EnemyMover enemy1, EnemyMover enemy2)
    {
        float distanceToEnemy1 = Vector3.Distance(transform.position, enemy1.transform.position);
        float distanceToEnemy2 = Vector3.Distance(transform.position, enemy2.transform.position);

        if (distanceToEnemy1 < distanceToEnemy2)
        {
            return enemy1;
        }
        else
        {
            return enemy2;
        }
    }

    private void AimWeapon()
    {
        if (!target || !target.activeInHierarchy) 
        {
            TurnParticleEmmision(projectileParticle, false); 
            return; 
        }        

        float targetDistance = Vector3.Distance(weapon.position, target.transform.position);

        if (targetDistance < shootingRange)
        {
            TurnParticleEmmision(projectileParticle, true);
            weapon.LookAt(target.transform);
        }
        else
        {
            TurnParticleEmmision(projectileParticle, false);
            target = null;
        }
    }

    public static void TurnParticleEmmision(ParticleSystem particle, bool enabled)
    {
        var emission = particle.emission;
        emission.enabled = enabled;
    }
}
