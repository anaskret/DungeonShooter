using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    //[SerializeField] protected DoorController[] doors;
    protected Animator myAnimator;
    protected GameObject player;

    public virtual int Health { get; protected set; }
    public static int EnemiesOverall { get; private set; } = 0;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerModel.DamageCooldown < Time.time)
            {
                PlayerModel.ChangeHealth(-0.5f);
                PlayerModel.SetDamageCooldown();
                Debug.Log(PlayerModel.Hearts);
            }
        }
    }

    public virtual void ChangeEnemyHealth(int change)
    {
        Health += change;
    }

    protected virtual void IsDead()
    {
        if (Health <= 0)
        {
            DoorController.EnemyKilled();
            EnemiesOverall--;
            Debug.Log("Enemies overall:" + EnemiesOverall);
            Destroy(gameObject);
        }
    }

    public static void AddEnemy()
    {
        EnemiesOverall++;
        Debug.Log("Enemies overall:" + EnemiesOverall);
    }
}
