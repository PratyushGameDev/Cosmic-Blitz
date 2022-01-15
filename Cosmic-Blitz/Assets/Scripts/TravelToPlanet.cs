using UnityEngine.SceneManagement;
using UnityEngine;

public class TravelToPlanet : MonoBehaviour
{
    [Tooltip("Amount to translate when traveling")][SerializeField] float verticalControlSpeed = 50;

    //Set Time to 0 && Set player component to false
    void Start()
    {
        Time.timeScale = 0;
        GetComponent<Player>().enabled = false;
    }

    //Translate forward at a frame rate independant rate and after 10 secs, load the next scene, which is scene #7
    void Update()
    {
        transform.Translate(0, 0, .2f * Time.deltaTime * verticalControlSpeed);
        if (Time.timeSinceLevelLoad > 10)
        {
            SceneManager.LoadScene(7);
        }
    }
}