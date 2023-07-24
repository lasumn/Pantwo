using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using UnityEngine.SceneManagement;

public class WinConLevel0 : MonoBehaviour
{
    private SoundEffects soundEffects;
    private bool played = false;
    public GameManager gameManager;
    // Start is called before the first frame update
    async void Start()
    {
        soundEffects = GetComponent<SoundEffects>();
        
        await gameManager.speechOut.Speak("Test hello whats");  
    }

    // Update is called once per frame
    void Update()
    {
        if((CheckWinCondition()))
        {
            Debug.Log("Win condition met");
            Invoke("PlayWinSound", 2f);
            Invoke("LoadNextScene", 4f);
        }
    }

    private void LoadNextScene()
    {
        Debug.Log("Loading next scene");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void PlayWinSound()
    {
        if(!played)
        {
            soundEffects.win();
            played = true;
        }
    }

    private bool CheckWinCondition()
    {
        if(GameObject.FindGameObjectsWithTag("Coin").Length > 0)
        {
            return false;
        }

        return true;
    }
}
