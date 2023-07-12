using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BFS
{
    public class Graph
    {
        public LinkedList<int>[] adj;
        public int Size;
    }

    public static List<int> FindShortestPath(Graph G, int startVert, int targetVert)
    {
        bool[] visited = new bool[G.Size];
        int[] previous = new int[G.Size];
        System.Collections.Generic.Queue<int> q = new System.Collections.Generic.Queue<int>();

        visited[startVert] = true;
        previous[startVert] = -1;

        q.Enqueue(startVert);

        while (q.Count > 0)
        {
            int v = q.Dequeue();

            if (v == targetVert)
            {
                // Reconstruct the shortest path
                return ReconstructPath(previous, startVert, targetVert);
            }

            foreach (int adjV in G.adj[v])
            {
                if (!visited[adjV])
                {
                    visited[adjV] = true;
                    previous[adjV] = v;
                    q.Enqueue(adjV);
                }
            }
        }

        // No path found
        return null;
    }

    public static List<int> ReconstructPath(int[] previous, int startVert, int targetVert)
    {
        List<int> path = new List<int>();

        int current = targetVert;
        while (current != -1)
        {
            path.Add(current);
            current = previous[current];
        }

        path.Reverse();

        return path;
    }
}

public class Navigator : MonoBehaviour
{

    private GameObject[] nodes;

    private Vector3[] nodesPos;

    private GameObject[] ghosts;

    private int[] movingTowardsNodeIndex; // for each Ghost, which Node Index it currently moving towards

    private bool[] isMoving;              // for each Ghost, if its currently trying to move to a node

    private LinkedList<int>[] adjList;

    // Start is called before the first frame update
    void Start()
    {

        nodes = GameObject.FindGameObjectsWithTag("Node");

        ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        movingTowardsNodeIndex = new int[ghosts.Length];

        isMoving = new bool[ghosts.Length];

        nodesPos = new Vector3[ghosts.Length];

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


        List<int> testList = BFS.FindShortestPath(new BFS.Graph(adjList,adjList.Count() ), 2, 5);

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
                if ((nodesPos[i] - nodesPos[index]).magnitude < 100)
                {
                    adj.AddLast(i);
                }
            }

        }

        return adj;

    }

}
