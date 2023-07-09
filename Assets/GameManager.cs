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
    private int switchTimerMax;
    private float minDist;



    // Start is called before the first frame update
    IEnumerator Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        yield return upperHandle.MoveToPosition(spawnPoint.transform.position);

        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        yield return lowerHandle.MoveToPosition(spawnPoint.transform.position);
        //lowerHandle.Freeze();


        ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        closestGhost = GameObject.Find("Ghost");

        switchTimer = switchTimerMax;

        yield return speechOut.Speak("hello test 123");
}

    // Update is called once per frame
    void Update()
    {

        //handle certain checks, but not every frame.
        switchTimer++;
        if (switchTimer > switchTimerMax) {
            switchTimer = 0;

            //switch it-handle to closest ghost
            StartCoroutine( SwitchToClosestGhost() );


            //check if wincons have been met
            if (GameObject.FindGameObjectsWithTag("Coin").Length == 0)
            {
                StartCoroutine ( LoadNextScene() );
            }
        }



    }

    IEnumerator SwitchToClosestGhost()
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

        yield return lowerHandle.SwitchTo(closestGhost);

    }

    IEnumerator LoadNextScene()
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
