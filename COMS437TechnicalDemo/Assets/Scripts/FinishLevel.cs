using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        //finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            //finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 0.5f); //waits 0.5 seconds before calling the method
        }
    }

    public void CompleteLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex < 2) ?
                                SceneManager.GetActiveScene().buildIndex + 1 : 0);
    }
}
