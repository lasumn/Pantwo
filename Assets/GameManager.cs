﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{

    private UpperHandle upperHandle;
    private LowerHandle lowerHandle;

    [SerializeField]
    private GameObject pacMan;

    public SpeechOut speechOut = new SpeechOut();

    // For Ghost-it-Handle tracking
    private GameObject[] ghosts; 
    private GameObject closestGhost;
    private int switchTimer;
    [SerializeField]
    private int switchTimerMax = 50;
    private float minDist;

    // For Coin-me-handle tracking

    private GameObject[] coins;
    private GameObject closestCoin;


    //for Ghost-Pathfinding

    private bool isInit = false;

    public Navigator navigator;



    private float yeetSpeed = 100f;




    // Start is called before the first frame update
    void Start()
    {
        pacMan.GetComponent<Collider>().enabled = false;
        pacMan.GetComponent<Rigidbody>().isKinematic = true;

        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        //_ = upperHandle.MoveToPosition(spawnPoint.transform.position);

        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        //_ = lowerHandle.MoveToPosition(spawnPoint.transform.position);
        //lowerHandle.Freeze();

        // disable god object collider
        GameObject mhgo = GameObject.Find("MeHandleGodObject");
        //mhgo.GetComponent<Collider>().enabled = false;
        GameObject ihgo = GameObject.Find("ItHandleGodObject");
        //ihgo.GetComponent<Collider>().enabled = false; //Julian thingz.?

        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        closestGhost = GameObject.Find("Ghost");

        coins = GameObject.FindGameObjectsWithTag("Coin");
        closestCoin = GameObject.Find("Coin");

        switchTimer = switchTimerMax;

        //_ = speechOut.Speak("hello test 123");

        pacMan.GetComponent<Collider>().enabled = true;
        pacMan.GetComponent<Rigidbody>().isKinematic = false;

        isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isInit) return;

        //handle certain checks, but not every frame.
        switchTimer++;
        if (switchTimer > switchTimerMax) {
            switchTimer = 0;

            //switch it-handle to closest ghost
            SwitchToClosestGhost();
            //RefreshClosestCoin();

            //check if wincons have been met
            
        }

       

        RefreshClosestCoin();


        //joinked from @lapesi

        Vector2 vector2 = new Vector2(pacMan.transform.position.x - closestCoin.transform.position.x, closestCoin.transform.position.z - pacMan.transform.position.z);
        float yRotation = Vector2.SignedAngle(Vector2.up, -vector2);
        //set upper handle y rotation to look towards the hole
        upperHandle.Rotate(yRotation);


        GameObject test= GameObject.Find("ItHandleGodObject");
        //Vector2 vector22 = new Vector2(navigator.nodesPos[closestGhost.GetComponent<Ghost>().goingToIndex].x - lowerHandle.transform.position.x, lowerHandle.transform.position.z - navigator.nodesPos[closestGhost.GetComponent<Ghost>().goingToIndex].z);
        //Vector2 vector22 = new Vector2(test.transform.position.x - closestCoin.transform.position.x, closestCoin.transform.position.z - test.transform.position.z);
        if (closestGhost != null) {
            Vector2 vector22 = new Vector2(test.transform.position.x - navigator.nodesPos[closestGhost.GetComponent<Ghost>().goingToIndex].x, navigator.nodesPos[closestGhost.GetComponent<Ghost>().goingToIndex].z - test.transform.position.z);
            float yRotation2 = Vector2.SignedAngle(Vector2.up, vector22);
            lowerHandle.Rotate(yRotation2);
        }
        

        //Debug.Log(upperHandle.gameObject.transform.position.x);

        //Debug.Log(closestCoin.name);
    }

    async void SwitchToClosestGhost()
    {
        if (ghosts.Length == 0) return;

        Debug.Log("Switching to closest ghost");
        minDist = float.MaxValue;

        foreach (GameObject ghost in ghosts)
        {
            if (Vector3.Distance(ghost.transform.position, pacMan.transform.position) < minDist)
            {
                minDist = Vector3.Distance(ghost.transform.position, pacMan.transform.position);
                closestGhost = ghost;
            }

        }
        await lowerHandle.SwitchTo(closestGhost,yeetSpeed);
    }

    public void RefreshClosestCoin()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");

        minDist = float.MaxValue;

        foreach (GameObject coin in coins)
        {
            if (Vector3.Distance(coin.transform.position, pacMan.transform.position) < minDist)
            {
                minDist = Vector3.Distance(coin.transform.position, pacMan.transform.position);
                closestCoin = coin;
            }

        }

    }
    

}
