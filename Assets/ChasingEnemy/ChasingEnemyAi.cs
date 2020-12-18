using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyAi : EnemyModel
{
    public float speed;
    [SerializeField] private float currentDistance;

    private bool contact = false;
    private float distanceToTarget;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();
        AddEnemy();

        Health = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsDead();

        currentDistance = Vector3.Distance(player.transform.position, transform.position);

        myAnimator.SetFloat("distanceFromPlayer", currentDistance);
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

    public void AttackPlayer()
    {
        if (contact == false)
        {
            var dif = player.transform.position - transform.position;
            transform.position += dif.normalized * Time.deltaTime * speed;
        }
    }
}
