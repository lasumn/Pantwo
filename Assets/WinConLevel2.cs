using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConLevel2 : MonoBehaviour
{
    private SoundEffects soundEffects;
    private bool played = false;
    // Start is called before the first frame update

    private float passedTime = 0f;
    void Start()
    {
        soundEffects = GetComponent<SoundEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;

        if ((CheckWinCondition()))
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
        if (passedTime < 10f)
        {
            return false;
        }

        return true;
    }
}
