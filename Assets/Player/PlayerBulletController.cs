using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public float lifeTime;
    private int damage=-1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        damage = -PlayerModel.Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ChasingEnemy"))
        {
            collision.GetComponent<ChasingEnemyAi>().ChangeEnemyHealth(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("ShootingEnemy"))
        {
            collision.GetComponent<ShootingEnemyAi>().ChangeEnemyHealth(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("SpawningEnemy"))
        {
            collision.GetComponent<SpawningEnemyAi>().ChangeEnemyHealth(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("SpawnedEnemy"))
        {
            collision.GetComponent<SpawnedEnemyAi>().ChangeEnemyHealth(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
