using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //movement
    [SerializeField] private float speed;
    Rigidbody2D newRigidbody;

    //shooting
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireDelay;
    private float lastFire;

    //talking with an npc
    private GameObject merchant;
    private static bool receivedUpgrade = false;
    [SerializeField] private Text centerText;

    //game over control
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject gameOverButton;

    //UI control
    [SerializeField] private Text health;
    [SerializeField] private GameObject specialAbility;

    //Special ability   
    [SerializeField] private float abilityDuration = 3;
    private float abilityTime;
    private bool isTriggered = false;

    void Start()
    {
        newRigidbody = GetComponent<Rigidbody2D>();

        merchant = GameObject.FindGameObjectWithTag("Merchant");

        PlayerModel.ChangeSpeed(speed);

        
    }


    void FixedUpdate()
    {
        GameOver();

        health.text = PlayerModel.Hearts.ToString();

        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();

        Talk();

        //if (EnemyModel.EnemiesOverall <= 0)

        if (PlayerModel.Speed != speed)
            speed = PlayerModel.Speed;

        if (IsAbilityAvailable())
        {
            specialAbility.SetActive(true);
        }
        else
        {
            specialAbility.SetActive(false);
        }

        UseAbility();
        FinishUsingAbility();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if(CanShoot(shootVertical, shootHorizontal) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }

        newRigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
    }

    private void Talk()
    {
        if (Vector3.Distance(merchant.transform.position, transform.position) < 5)
        {
            Debug.Log("MERCH");
            centerText.text = "Press A to increase damage\nPress X to increase speed";
            AwaitInput();
        }
        else if(Vector3.Distance(merchant.transform.position, transform.position) > 5)
        {
            centerText.text = "";
        }
    }

    void Shoot(float x, float y)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            x * bulletSpeed,
            y * bulletSpeed,
            0);
    }

    private void UseAbility()
    {
        if (PlayerModel.DamageCooldown < Time.time && Input.GetKey("joystick button 1") && isTriggered == false)
        {
            PlayerModel.SpecialAbilityUsed();
            isTriggered = true;
            abilityTime = Time.time + abilityDuration;
        }
    }

    private void FinishUsingAbility()
    {
        if(isTriggered == true && abilityTime < Time.time)
        {
            PlayerModel.SpecialAbilityFinished();
            isTriggered = false;
            PlayerModel.SetSpecialAbilityCooldown();
        }
    }

    private bool IsAbilityAvailable()
    {
        if (PlayerModel.SpecialAbilityCooldown < Time.time)
            return true;
        else
            return false;
    }

    private void AwaitInput()
    {
        if (Input.GetKey("joystick button 0") && !receivedUpgrade)
        {
            Debug.Log("Upgrade 1");
            PlayerModel.BuffDamage();
            receivedUpgrade = true;
        }
        else if (Input.GetKey("joystick button 2") && !receivedUpgrade)
        {
            Debug.Log("Upgrade 2");
            PlayerModel.ChangeSpeed(8);
            receivedUpgrade = true;
        }

    }

    private void GameOver()
    {
        if (DidPlayerWon())
        {
            gameOverText.text = "YOU WON!";
            gameOverButton.SetActive(true);
            
        }
        if (DidPlayerLost())
        {
            gameOverText.text = "YOU LOST!";
            gameOverButton.SetActive(true);
            Destroy(gameObject);
        }
    }

    private bool DidPlayerWon()
    {
        if (EnemyModel.EnemiesOverall <= 0)
            return true;
        else
            return false;
    }

    private bool DidPlayerLost()
    {
        if (PlayerModel.Hearts <= 0)
            return true;
        else
            return false;
    }
 #region test

    private bool CanShoot(float y, float x)
    {
        if (((y < 1 && y > 0) || (y > -1 && y < 0)) && ((x < 1 && x > 0) || (x > -1 && x < 0)))
            return false;

        return true;
    }
#endregion test
}
