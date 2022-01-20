using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelEnemy : MonoBehaviour
{
    [SerializeField] int hitPoints = 4;
    
    [Header("All the visuals for the enemy ship")]
    [Tooltip("This is the explosion")] [SerializeField] ParticleSystem explosion;
    [Tooltip("This is the spark that goes off when it takes damage")] [SerializeField] ParticleSystem hitSpark;
    [Tooltip("This is the enemy thruster")] [SerializeField] ParticleSystem thruster;
    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject LoseScreen;


    [Header("All of the references")]
    [Tooltip("This is the player reference")] Player player;
    [Tooltip("This is the audiosource reference")] AudioSource aS;
    ScoreScript sS;
    BonusTimer bT;

    //Make sure to cache our references
    void Start()
    {
        player = FindObjectOfType<Player>();
        aS = GetComponent<AudioSource>();
        sS = FindObjectOfType<ScoreScript>();
        bT = FindObjectOfType<BonusTimer>();
    }

    void Update()
    {
        transform.Translate(0, 0, 0.1f * Time.deltaTime * 30);
        if (Time.timeSinceLevelLoad >= 30)
        {
            if (player.rightLazer.isStopped != true)
            {
                player.rightLazer.Stop();
            }
            if (player.leftLazer.isStopped != true)
            {
                player.leftLazer.Stop();
            }
            bT.ChangeTextToDone();
            Time.timeScale = 0.5f;
            Invoke(nameof(FinishBonus), 0.5f);
        }
    }

    void FinishBonus()
    {
        if (sS.scoreAmount >= 24000f)
        {
            WinScreen.SetActive(true);
            player.winBoostFlame.Play();
        }
        if (sS.scoreAmount < 24000f)
        {
            LoseScreen.SetActive(true);
        }
    }

    //On particle collision
    void OnParticleCollision(GameObject other)
    {
        //If the particle was a player particle
        //reduce hit points by 1
        //play a vfx
        if (other.gameObject.tag == "Player")
        {
            hitPoints = hitPoints - 1;
            hitSpark.Play();
            sS.IncreaseScore(38);
            if (hitPoints == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BonusLevelEnemy>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                if (!explosion.isPlaying)
                {
                    explosion.Play();
                }
                Invoke(nameof(DestroyWhenDestroyed), 2f);
                if (thruster != null)
                {
                    thruster.Stop();
                }
            }
        }
    }

    //What happens on collision
    void OnCollisionEnter(Collision other)
    {
        //If the collider is the enemy finish pad
        //play explosionPlayer vfx
        //Call DestroyWhenEnemyFinishes from the player script
        //Finally, load the current scene in 2 seconds
        if (other.gameObject.tag == "Player")
        {
            player.CollisionCrashSequence();
        }
    }
    
    //Load Current Scene
    void EnemyLoadCurrentScene()
    {
        player.LoadCurrentScene();
    }
    
    //Destroy the gameObject
    void DestroyWhenDestroyed()
    {
        Destroy(gameObject);
    }
}
