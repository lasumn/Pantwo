﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);

            if (GameObject.FindGameObjectsWithTag("Coin").Length - 1 == 0)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }

        }
    }
}