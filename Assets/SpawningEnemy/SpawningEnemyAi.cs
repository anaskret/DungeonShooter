using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemyAi : EnemyModel
{
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject spawnPrefab;
    private float cooldown;

    private float currentDistance = 40;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AddEnemy();
        myAnimator = GetComponent<Animator>();
        Health = 8;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsDead();

        currentDistance = Vector3.Distance(player.transform.position, transform.position);

        myAnimator.SetFloat("distanceFromPlayer", currentDistance);
    }

    public void SpawnEnemies()
    {
        if(cooldown < Time.time)
        {
            Instantiate(spawnPrefab, transform.position, transform.rotation);
            DoorController.NewEnemy();
            cooldown = spawnRate + Time.time;
        }
    }
}
