using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private UpperHandle upperHandle;
    private LowerHandle lowerHandle;
    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private GameObject pacMan;

    public SpeechOut speechOut = new SpeechOut();

    // For Ghost-it-Handle tracking
    private GameObject[] ghosts; 
    private GameObject closestGhost;
    private int switchTimer;
    [SerializeField]
    private int switchTimerMax = 500;
    private float minDist;

    // For Coin-me-handle tracking

    private GameObject[] coins;
    private GameObject closestCoin;


    //for Ghost-Pathfinding

    



    private float yeetSpeed = 10f;


    // Start is called before the first frame update
    async void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await upperHandle.MoveToPosition(spawnPoint.transform.position);

        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        await lowerHandle.MoveToPosition(spawnPoint.transform.position);
        //lowerHandle.Freeze();


        ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        closestGhost = GameObject.Find("Ghost");

        coins = GameObject.FindGameObjectsWithTag("Coin");

        closestCoin = GameObject.Find("Coin");

        switchTimer = switchTimerMax;

        

    await speechOut.Speak("hello test 123");
}

    // Update is called once per frame
    void Update()
    {
        //handle certain checks, but not every frame.
        switchTimer++;
        if (switchTimer > switchTimerMax) {
            switchTimer = 0;

            //switch it-handle to closest ghost
            SwitchToClosestGhost();

            RefreshClosestCoin();

            //check if wincons have been met
            if (GameObject.FindGameObjectsWithTag("Coin").Length == 0)
            {
                LoadNextScene();
            }
        }

        //joinked from @lapesi
        Vector2 vector2 = new Vector2(pacMan.transform.position.x - closestCoin.transform.position.x, closestCoin.transform.position.z - pacMan.transform.position.z);
        float yRotation = Vector2.SignedAngle(Vector2.up, vector2);
        //set upper handle y rotation to look towards the hole
        upperHandle.Rotate(yRotation);

        //Debug.Log(upperHandle.gameObject.transform.position.x);

        //Debug.Log(closestCoin.name);
    }


    async void SwitchToClosestGhost()
    {
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

        foreach (GameObject coin in coins)
        {
            if (Vector3.Distance(coin.transform.position, pacMan.transform.position) < minDist)
            {
                minDist = Vector3.Distance(coin.transform.position, pacMan.transform.position);
                closestCoin = coin;
            }

        }

    }

    void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }


    



}
