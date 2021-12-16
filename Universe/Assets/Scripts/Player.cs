using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem leftLazer;
    [SerializeField] ParticleSystem rightLazer;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem thruster;
    [SerializeField] ParticleSystem hitSpark;
    [SerializeField] int healthPoints = 8;
    [SerializeField] GameObject Heart1;
    [SerializeField] GameObject Heart2;
    [SerializeField] GameObject Heart3;
    [SerializeField] GameObject Heart4;
    [SerializeField] float controlSpeed = 75;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessShip();
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKey(KeyCode.C))
        {
            GetComponent<BoxCollider>().enabled = false;
        }
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextScene();
        }
    }
    
    void ProcessShip()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            leftLazer.Play();
            rightLazer.Play();
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            leftLazer.Stop();
            rightLazer.Stop();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-.2f * Time.deltaTime * controlSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(.2f * Time.deltaTime * controlSpeed, 0, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        hitSpark.Play();
        healthPoints = healthPoints - 1;
        if (healthPoints == 6)
        {
            Destroy(Heart4);
        }
        if (healthPoints == 4)
        {
            Destroy(Heart3);
        }
        if (healthPoints == 2)
        {
            Destroy(Heart2);
        }
        if (healthPoints == 0)
        {
            CrashSequence();
            Destroy(Heart1);
        }

    }

    public void CrashSequence()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Player>().enabled = false;
        Invoke(nameof(LoadCurrentScene), 2f);
        thruster.Stop();
        if (!explosion.isPlaying)
        {
            explosion.Play();
        }
    }

    void OnParticleCollision(GameObject other)
    {        
        if (other.gameObject.tag == "Enemy")
        {
            hitSpark.Play();
            healthPoints = healthPoints - 1;
            if (healthPoints == 6)
            {
                Destroy(Heart4);
            }
            if (healthPoints == 4)
            {
                Destroy(Heart3);
            }
            if (healthPoints == 2)
            {
                Destroy(Heart2);
            }
            
            if (healthPoints == 0)
            {
                GetComponent<Player>().enabled = false;
                Invoke(nameof(LoadCurrentScene), 2f);
                GetComponent<MeshRenderer>().enabled = false;
                thruster.Stop();
                if (!explosion.isPlaying)
                {
                    explosion.Play();
                }
                leftLazer.Stop();
                rightLazer.Stop();
                Destroy(Heart1);
            }            
        }
    }

    public void LoadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    public void DestroyWhenEnemyFinishes()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Player>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        leftLazer.Stop();
        rightLazer.Stop();
        Destroy(Heart1);
        Destroy(Heart2);
        Destroy(Heart3);
        Destroy(Heart4);
        thruster.Stop();
    }
}
