using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("All of the ship visuals")]
    [Tooltip("This is the left laser on the player ship")][SerializeField] ParticleSystem leftLazer;
    [Tooltip("This is the right laser on the player ship")][SerializeField] ParticleSystem rightLazer;
    [Tooltip("This is the explosion vfx for the player ship")][SerializeField] ParticleSystem explosion;
    [Tooltip("This is the main thruster on the player ship")][SerializeField] public ParticleSystem thruster;
    [Tooltip("This is the spark vfx  that goes off when someone damages the player ship")][SerializeField] ParticleSystem hitSpark;
    [Tooltip("This is the win vfx that plays when the player ship defeats all enemies")][SerializeField] ParticleSystem winBoostFlame;
    [Tooltip("This is the left thruster on the player ship")][SerializeField] ParticleSystem leftThruster;
    [Tooltip("This is the right thruster on the player ship")][SerializeField] ParticleSystem rightThruster;


    [Header("All of the gameObjects the ship needs")]
    [Tooltip("This is the first heart for the ship")][SerializeField] GameObject Heart1;
    [Tooltip("This is the second heart for the ship")][SerializeField] GameObject Heart2;
    [Tooltip("This is the third heart for the ship")] [SerializeField] GameObject Heart3;
    [Tooltip("This is the fourth heart for the ship")] [SerializeField] GameObject Heart4;
    [Tooltip("This is the first damaged heart for the ship")] [SerializeField] GameObject DeadHeart1;
    [Tooltip("This is the second damaged heart for the ship")] [SerializeField] GameObject DeadHeart2;
    [Tooltip("This is the third damaged heart for the ship")] [SerializeField] GameObject DeadHeart3;
    [Tooltip("This is the fourth damaged heart for the ship")] [SerializeField] GameObject DeadHeart4;
    [Tooltip("This is the first enemy")] [SerializeField] public GameObject enemyShip1;
    [Tooltip("This is the second enemy")] [SerializeField] GameObject enemyShip2;
    [Tooltip("This is the third enemy")] [SerializeField] GameObject enemyShip3;
    [Tooltip("This is the fourth enemy")] [SerializeField] GameObject enemyShip4;
    [Tooltip("This is the fifth enemy")] [SerializeField] GameObject enemyShip5;
    [Tooltip("This is the sixth enemy")] [SerializeField] GameObject enemyShip6;
    [Tooltip("This is the seventh enemy")] [SerializeField] GameObject enemyShip7;
    [Tooltip("This is the boundaries needed to keep the player ship in place")] [SerializeField] GameObject boundary;


    [Header("All of the tunables for the ship")]
    [Tooltip("This is the amount of damage the ship can take before exploding")] [SerializeField] int healthPoints = 8;
    [Tooltip("This is the amount of turning speed the ship has")] [SerializeField] public float horizontalControlSpeed = 75;
    [Tooltip("This is the amount of thrust the ship has")] [SerializeField] public float verticalControlSpeed = 50;
    [Tooltip("This is the amount of speed the ship has after winning")] [SerializeField] float speedOfShipWhenWon = 5;
    [Tooltip("This is the amount of time the ship has to wait for until the next level loads in")] [SerializeField] int timeToWaitUntilNextLevel = 8;
    [Tooltip("This is the amount of thrust the ship has after winning")] [SerializeField] public float amountToIncreaseThrusterWhenWon = 3;
    [Tooltip("This is the amount of rotation the ship has when you move the player left or right")] [SerializeField] int amountToRotateOnPlayerMovement = 50;
    [Tooltip("This is the amount of rotation the ship has when you manually rotate the ship by arrow keys")] [SerializeField] int amountToRotateOnPhysicalInput = 65;
    [Tooltip("This is the amount of laser the ship will emit")] int amountToEmitLazer = 1;


    [Header("All the materials for the ship")]
    [Tooltip("This is the red material the ship needs to apply when you click a certain button")] [SerializeField] Material Red;
    [Tooltip("This is the blue material the ship needs to apply when you click a certain button")] [SerializeField] Material Blue;
    [Tooltip("This is the green material the ship needs to apply when you click a certain button")] [SerializeField] Material Green;
    [Tooltip("This is the gray material the ship needs to apply when you click a certain button")] [SerializeField] Material Gray;
    [Tooltip("This is the purple material the ship needs to apply when you click a certain button")] [SerializeField] Material Purple;
    [Tooltip("This is the white material the ship needs to apply when you click a certain button")] [SerializeField] Material White;
    [Tooltip("This is the cyan material the ship needs to apply when you click a certain button")] [SerializeField] Material Cyan;
    [Tooltip("This is the black material the ship needs to apply when you click a certain button")] [SerializeField] Material Black;


    [Header("References")]
    [Tooltip("This is the reference to the audiosource component")] AudioSource audioSource;
    [Tooltip("This is the reference to the rigidbody component")] Rigidbody rigidBody;


    [Header("All the audioclips that the ship needs to play")]
    [Tooltip("This is the clip of shooting lasers")] [SerializeField] AudioClip lazer;
    [Tooltip("This is the clip of taking damage")] [SerializeField] AudioClip damage;
    [Tooltip("This is the clip of winning")] [SerializeField] AudioClip win;
    [Tooltip("This is the clip of loosing")] [SerializeField] AudioClip destruct;

    //At Start(), we want to cache our references and make sure that Time = 0, so our player can read the instructions
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 0;
    }

    //At Update(), we want to make sure that these three methods are running and working
    void Update()
    {
        ProcessShip();
        WinOnEnemyDestruction();
        MakeSureCollisionDoesntAffectPlayerPosition();
    }

    //In this method, we want to make sure that collision doesn't affect the player's velocity
    void MakeSureCollisionDoesntAffectPlayerPosition()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    //This method is for turning the player red which will be accessed through another script
    public void TurnRed()
    {
        GetComponent<MeshRenderer>().material = Red;
    }

    //This method is for turning the player blue which will be accessed through another script
    public void TurnBlue()
    {
        GetComponent<MeshRenderer>().material = Blue;
    }

    //This method is for turning the player green which will be accessed through another script
    public void TurnGreen()
    {
        GetComponent<MeshRenderer>().material = Green;
    }

    //This method is for turning the player gray which will be accessed through another script
    public void TurnGray()
    {
        GetComponent<MeshRenderer>().material = Gray;
    }

    //This method is for turning the player purple which will be accessed through another script
    public void TurnPurple()
    {
        GetComponent<MeshRenderer>().material = Purple;
    }

    //This method is for turning the player white which will be accessed through another script
    public void TurnWhite()
    {
        GetComponent<MeshRenderer>().material = White;
    }

    //This method is for turning the player cyan which will be accessed through another script
    public void TurnCyan()
    {
        GetComponent<MeshRenderer>().material = Cyan;
    }

    //This method is for turning the player black which will be accessed through another script
    public void TurnBlack()
    {
        GetComponent<MeshRenderer>().material = Black;
    }
    
    //This method will transition to the next level if all enemy ships are destroyed, or in other words, null
    void WinOnEnemyDestruction()
    {
        if (enemyShip1 == null &&
            enemyShip2 == null &&
            enemyShip3 == null &&
            enemyShip4 == null &&
            enemyShip5 == null &&
            enemyShip6 == null &&
            enemyShip7 == null)
        {
            transform.Translate(0, 0, speedOfShipWhenWon * Time.deltaTime);
            Invoke(nameof(LoadNextScene), timeToWaitUntilNextLevel);
            if (!winBoostFlame.isPlaying)
            {
                winBoostFlame.Play();
            }
            thruster.GetComponent<ParticleSystem>().startSize = amountToIncreaseThrusterWhenWon;
            if (boundary != null)
            {
                Destroy(boundary);
            }                       
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(win);
            }
        }
    }
    
    //This method is for making the ship move, shoot lazers, rotate, and turn left and right
    void ProcessShip()
    {   
        //If you click these buttons down, emit a lazer and play the sfx
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            leftLazer.Emit(amountToEmitLazer);
            rightLazer.Emit(amountToEmitLazer);
            leftLazer.Play();
            rightLazer.Play();
            audioSource.PlayOneShot(lazer);
        }

        //If you let go of these buttons, stop the lazers
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            leftLazer.Stop();
            rightLazer.Stop();
        }

        //If you click these buttons down, translate the parent to the left
        if (Input.GetKey(KeyCode.A))
        {
            transform.parent.Translate(-.2f * Time.deltaTime * horizontalControlSpeed, 0, 0);
        }

        //If you click these buttons down, rotate the ship and play the right thruster vfx
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -0.5f * Time.deltaTime * amountToRotateOnPlayerMovement, 0);
            rightThruster.Play();
        }

        //If you let go of these buttons, stop the thruster
        if (Input.GetKeyUp(KeyCode.A))
        {
            rightThruster.Stop();
        }

        //If you click these buttons down, translate the parent to the right
        if (Input.GetKey(KeyCode.D))
        {
            transform.parent.Translate(.2f * Time.deltaTime * horizontalControlSpeed, 0, 0);
        }

        //If you click these buttons down, rotate the ship and play the left thruster vfx
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0.5f * Time.deltaTime * amountToRotateOnPlayerMovement, 0);
            leftThruster.Play();
        }

        //If you let go of these buttons, stop the thruster
        if (Input.GetKeyUp(KeyCode.D))
        {
            leftThruster.Stop();
        }

        //If you click the button, rotate and play the right thruster vfx
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(0, -0.8f * Time.deltaTime * amountToRotateOnPhysicalInput, 0);
            rightThruster.Play();
        }

        //If you let go of this button, stop playing the thruster
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rightThruster.Stop();
        }

        //If you click the button, rotate and play the left thruster vfx
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0.8f * Time.deltaTime * amountToRotateOnPhysicalInput, 0);
            leftThruster.Play();
        }

        //If you let go of this button, stop playing the thruster
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            leftThruster.Stop();
        }

        //If you click these buttons, move forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, 0.2f * Time.deltaTime * verticalControlSpeed);
        }

        //If you click these buttons, move back
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -0.2f * Time.deltaTime * verticalControlSpeed);
        }
    }

    //What happens on collision
    void OnCollisionEnter(Collision other)
    {
        //If the collider is the enemy
        //then play the sfx, play the hitspark vfx, and decrease health points by 1
        if (other.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(damage);
            hitSpark.Play();
            healthPoints = healthPoints - 1;            
            if (healthPoints == 7)
            {
                Heart4.GetComponent<Animator>().enabled = false;
                Heart4.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 6)
            {
                DeadHeart4.GetComponent<SpriteRenderer>().enabled = false;
                Heart4.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 5)
            {
                Heart3.GetComponent<Animator>().enabled = false;
                Heart3.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 4)
            {
                DeadHeart3.GetComponent<SpriteRenderer>().enabled = false;
                Heart3.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 3)
            {
                Heart2.GetComponent<Animator>().enabled = false;
                Heart2.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 2)
            {
                DeadHeart2.GetComponent<SpriteRenderer>().enabled = false;
                Heart2.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 1)
            {
                Heart1.GetComponent<Animator>().enabled = false;
                Heart1.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 0)
            {
                CollisionCrashSequence();
                DeadHeart1.GetComponent<SpriteRenderer>().enabled = false;
                Heart1.GetComponent<SpriteRenderer>().enabled = false;
                audioSource.PlayOneShot(destruct);
            }
        }

        //If the collider is the mine
        //then play the sfx, play the hitspark vfx, and decrease health points by 3
        if (other.gameObject.tag == "Mine")
        {
            audioSource.PlayOneShot(damage);
            hitSpark.Play();
            healthPoints = healthPoints - 3;
            if (healthPoints == 7)
            {
                Heart4.GetComponent<Animator>().enabled = false;
                Heart4.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 6)
            {
                DeadHeart4.GetComponent<SpriteRenderer>().enabled = false;
                Heart4.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 5)
            {
                Heart3.GetComponent<Animator>().enabled = false;
                Heart3.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 4)
            {
                DeadHeart3.GetComponent<SpriteRenderer>().enabled = false;
                Heart3.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 3)
            {
                Heart2.GetComponent<Animator>().enabled = false;
                Heart2.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 2)
            {
                DeadHeart2.GetComponent<SpriteRenderer>().enabled = false;
                Heart2.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 1)
            {
                Heart1.GetComponent<Animator>().enabled = false;
                Heart1.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 0)
            {
                CollisionCrashSequence();
                DeadHeart1.GetComponent<SpriteRenderer>().enabled = false;
                Heart1.GetComponent<SpriteRenderer>().enabled = false;
                audioSource.PlayOneShot(destruct);
            }
        }

        //If the collider is the nuke
        //then play the sfx, play the hitspark vfx, and decrease health points by 5
        if (other.gameObject.tag == "Nuke")
        {
            audioSource.PlayOneShot(damage);
            hitSpark.Play();
            healthPoints = healthPoints - 5;
            if (healthPoints == 7)
            {
                Heart4.GetComponent<Animator>().enabled = false;
                Heart4.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 6)
            {
                DeadHeart4.GetComponent<SpriteRenderer>().enabled = false;
                Heart4.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 5)
            {
                Heart3.GetComponent<Animator>().enabled = false;
                Heart3.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 4)
            {
                DeadHeart3.GetComponent<SpriteRenderer>().enabled = false;
                Heart3.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 3)
            {
                Heart2.GetComponent<Animator>().enabled = false;
                Heart2.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 2)
            {
                DeadHeart2.GetComponent<SpriteRenderer>().enabled = false;
                Heart2.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 1)
            {
                Heart1.GetComponent<Animator>().enabled = false;
                Heart1.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 0)
            {
                CollisionCrashSequence();
                DeadHeart1.GetComponent<SpriteRenderer>().enabled = false;
                Heart1.GetComponent<SpriteRenderer>().enabled = false;
                audioSource.PlayOneShot(destruct);
            }
        }
    }

    //All that happens when you crash
    public void CollisionCrashSequence()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Player>().enabled = false;
        Invoke(nameof(LoadCurrentScene), 2f);
        thruster.Stop();
        if (!explosion.isPlaying)
        {
            explosion.Play();
        }
        leftLazer.Stop();
        rightLazer.Stop();
        leftThruster.Stop();
        rightThruster.Stop();
    }

    //What happens on particle collision
    void OnParticleCollision(GameObject other)
    {
        //If the particle is the enemy
        //then play the sfx, play the hitspark vfx, and decrease health points by 1
        if (other.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(damage);
            hitSpark.Play();
            healthPoints = healthPoints - 1;
            if (healthPoints == 7)
            {
                Heart4.GetComponent<Animator>().enabled = false;
                Heart4.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 6)
            {
                //Destroy(DeadHeart4);
                //Destroy(Heart4);
                DeadHeart4.GetComponent<SpriteRenderer>().enabled = false;
                Heart4.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 5)
            {
                Heart3.GetComponent<Animator>().enabled = false;
                Heart3.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 4)
            {
                //Destroy(DeadHeart3);
                //Destroy(Heart3);
                DeadHeart3.GetComponent<SpriteRenderer>().enabled = false;
                Heart3.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 3)
            {
                Heart2.GetComponent<Animator>().enabled = false;
                Heart2.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 2)
            {
                //Destroy(DeadHeart2);
                //Destroy(Heart2);
                DeadHeart2.GetComponent<SpriteRenderer>().enabled = false;
                Heart2.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 1)
            {
                Heart1.GetComponent<Animator>().enabled = false;
                Heart1.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (healthPoints == 0)
            {
                CollisionCrashSequence();
                //Destroy(DeadHeart1);
                //Destroy(Heart1);
                DeadHeart1.GetComponent<SpriteRenderer>().enabled = false;
                Heart1.GetComponent<SpriteRenderer>().enabled = false;
                audioSource.PlayOneShot(destruct);
            }
        }
    }

    //All that happens on particle crash
    void ParticleCrashSequence()
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
        leftThruster.Stop();
        rightThruster.Stop();
    }

    //Load the current scene
    public void LoadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    //Load the next scene
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    //If the player reaches your home planet, do all this
    public void DestroyWhenEnemyFinishes()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Player>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        leftLazer.Stop();
        rightLazer.Stop();
        leftThruster.Stop();
        rightThruster.Stop();
        Destroy(Heart1);
        Destroy(Heart2);
        Destroy(Heart3);
        Destroy(Heart4);
        Destroy(DeadHeart1);
        Destroy(DeadHeart2);
        Destroy(DeadHeart3);
        Destroy(DeadHeart4);
        thruster.Stop();
        audioSource.PlayOneShot(destruct);
    }
}