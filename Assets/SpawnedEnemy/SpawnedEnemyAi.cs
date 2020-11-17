using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedEnemyAi : EnemyModel
{
    [SerializeField] private float speed;

    private bool contact = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AddEnemy();
        Health = 1;
    }

    void FixedUpdate()
    {
        IsDead();

        if (contact == false)
        {
            var dif = player.transform.position - transform.position;
            transform.position += dif.normalized * Time.deltaTime * speed;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = true;
            if (PlayerModel.DamageCooldown < Time.time)
            {
                PlayerModel.ChangeHealth(-0.5f);
                PlayerModel.SetDamageCooldown();
                Debug.Log(PlayerModel.Hearts);
                Health--;
                IsDead();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = false;
        }
    }

}
