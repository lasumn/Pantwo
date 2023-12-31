﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using static GameManager;
using UnityEngine.SceneManagement;


public class PacMan : MonoBehaviour
{
    private SoundEffects soundEffects;

    private bool isDead = false;

    void Start()
    {
        soundEffects = GetComponent<SoundEffects>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            soundEffects.coinCollected();
            Destroy(other.gameObject);
            //RefreshClosestCoinTEST();
        }
        
        if (other.CompareTag("Ghost") && !isDead)
        {
            isDead = true;
            soundEffects.gameOver();
            //Time.timeScale = 0;
            Invoke("RestartLevel", 5f);
            //RestartLevel();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
