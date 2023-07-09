using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class GameManager : MonoBehaviour
{

    private UpperHandle upperHandle;
    private LowerHandle lowerHandle;
    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private GameObject pacMan;



    private GameObject[] ghosts; 

    private GameObject closestGhost;


    private int switchTimer;
    private int switchTimerMax = 1000;

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

        
    }

    // Update is called once per frame
    void Update()
    {

        switchTimer++;

        if (switchTimer > switchTimerMax) {
            switchTimer = 0;
            StartCoroutine( SwitchToClosestGhost() );
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
}
