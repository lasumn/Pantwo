using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class SoundEffects : MonoBehaviour
{
    public AudioClip coinSound;
    public AudioClip gameOverSound;
    public AudioClip winSound;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void coinCollected()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void gameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    public void win()
    {
        audioSource.PlayOneShot(winSound);
    }
}
