using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BFS;
using UnityEngine.Events;


public class Navigator : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    private float nodeConnectionRange;

    private GameObject[] nodes;

    public Vector3[] nodesPos;

    private GameObject[] ghosts;

    private int[] movingTowardsNodeIndex; // for each Ghost, which Node Index it currently moving towards

    private bool[] isMoving;              // for each Ghost, if its currently trying to move to a node

    public LinkedList<int>[] adjList;

    // list loaded event
    public UnityEvent listLoadedEvent;

    // Start is called before the first frame update
    void Start()
    {

        nodes = GameObject.FindGameObjectsWithTag("Node");

        ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        movingTowardsNodeIndex = new int[ghosts.Length];

        isMoving = new bool[ghosts.Length];

        nodesPos = new Vector3[nodes.Length];

        adjList = new LinkedList<int>[nodes.Length];





        for (int i = 0; i < isMoving.Length; i++)
        {
            isMoving[i] = true;
        }

        for (int i = 0; i < nodesPos.Length; i++)
        {
            nodesPos[i] = nodes[i].transform.position;
        }

        for (int i = 0; i < movingTowardsNodeIndex.Length; i++)
        {
            movingTowardsNodeIndex[i] = FindClosestNodeIndex(ghosts[i]);

            ghosts[i].transform.position = nodes[movingTowardsNodeIndex[i]].transform.position;
        }

        for (int i = 0; i < adjList.Length; i++)
        {
            adjList[i] = findAdj(i);
        }

        listLoadedEvent.Invoke();


        List<int> testList = BFS.FindShortestPath(new BFS.Graph(adjList,adjList.Length), 2, 5);


        Debug.Log(testList);
    }

    // Update is called once per frame
    void Update()
    {
 
    }



    void MoveGhostToNode(GameObject ghost, GameObject node)
    {
        Vector3 temp = new Vector3();

        temp = (node.transform.position - ghost.transform.position);
        temp.Normalize();
        ghost.transform.position += temp / 50;

    }

    int FindClosestNodeIndex(GameObject ghost)
    {
        int ir = -1;

        float minDist = float.MaxValue;

        for (int i = 0; i < nodes.Length; i++)
        {
            if ( (nodes[i].transform.position - ghost.transform.position).magnitude < minDist)
            {
                ir = i;
                minDist = (nodes[i].transform.position - ghost.transform.position).magnitude;
            }
        }
        return ir;
    }

    LinkedList<int> findAdj (int index)
    {
        LinkedList<int> adj = new LinkedList<int> ();

        for (int i = 0; i < nodes.Length    ; i++)
        {
            if (i != index)
            {
                if ((nodesPos[i] - nodesPos[index]).magnitude < nodeConnectionRange)
                {
                    adj.AddLast(i);
                }
            }

        }

        return adj;

    }

}
