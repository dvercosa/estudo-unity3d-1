using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip sucess;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;
    bool isTrasitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTrasitioning) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly" :
                Debug.Log("Friendly Pad");
                break;
            case "Finish" :
                StartSuccessSequence();
                break;
            //case "Fuel" :
                //Debug.Log("You picked up fuel!");
                //break;             
            default:                
                Debug.Log("Sorry, you blew up");
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTrasitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(sucess);
        // Todo add Particle Effetc upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTrasitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        // Todo add Particle Effetc upon sucess
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
