using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyAi : EnemyModel
{
    [SerializeField] private float currentDistance;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public float enemySpeed;

    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;

    //waypoints
    private Transform[] waypoints = null;
    private Transform pointA;
    private Transform pointB;
    private int currentTarget;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();
        pointA = p1.transform;
        pointB = p2.transform;
        waypoints = new Transform[2]
        {
            pointA,
            pointB
        };
        currentTarget = 1;
        AddEnemy();

        Health = 8;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsDead();

        currentDistance = player.transform.position.x - transform.position.x;
        myAnimator.SetFloat("horizontalDistanceFromPlayer", currentDistance);

        MoveToCurrentTarget();
    }

    public void SetNextPoint()
    {
        switch (currentTarget)
        {
            case 0:
                currentTarget = 1;
                break;
            case 1:
                currentTarget = 0;
                break;
        }
    }

    public void MoveToCurrentTarget()
    {
        var dif = waypoints[currentTarget].position - transform.position;
        transform.position += dif.normalized * Time.deltaTime * enemySpeed;
        if (dif.magnitude < 0.3)
        {
            SetNextPoint();
        }
    }

    public void ShootLeft()
    {
        if (Time.time > lastFire + fireDelay)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                -1 * bulletSpeed,
                0,
                0);
            lastFire = Time.time;
        }
    }

    public void ShootRight()
    {
        if (Time.time > lastFire + fireDelay)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                1 * bulletSpeed,
                0,
                0);
            lastFire = Time.time;
        }
    }
}
