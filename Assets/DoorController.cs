using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject entrance;
    private GameObject player;
    [SerializeField] private int numberOfEnemiesInNextRoom;
    public static int NumberOfEnemies { get; private set; } = 2; //Number of enemies in the first room

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (NumberOfEnemies == 0)
        {
            player.transform.position = entrance.transform.position;
            NumberOfEnemies = numberOfEnemiesInNextRoom;
            numberOfEnemiesInNextRoom = 0;
            Debug.Log("Number of enemies: " + NumberOfEnemies);
        }
    }

    public static void EnemyKilled()
    {
        NumberOfEnemies--;
        Debug.Log("Number of enemies: " + NumberOfEnemies);
    }

    public static void NewEnemy()
    {
        NumberOfEnemies++;
        Debug.Log("Number of enemies:" + NumberOfEnemies);
    }
}
