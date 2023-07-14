using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Linq;

public class PacMan : MonoBehaviour
{
    private UpperHandle handle;
    private bool moving;

    private GameObject cur_node;
    private GameObject next_node;

    // Start is called before the first frame update
    async void Start()
    {
        handle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        next_node = GameObject.FindGameObjectsWithTag("Node")[0];
        await handle.SwitchTo(next_node, 10f);
        Move();
    }

    async void Move(){
        while(true){
            cur_node = next_node;
            cur_node.GetComponent<Collider>().enabled = false;

            // Get available nodes
            List<GameObject> nodes = new List<GameObject>();
            foreach (GameObject node in GameObject.FindGameObjectsWithTag("Node")){
                if (node == cur_node) {continue;}
                node.GetComponent<Collider>().enabled = false;
                Ray ray = new Ray(
                    cur_node.transform.position, 
                    (node.transform.position - cur_node.transform.position).normalized
                );
                RaycastHit hit;
                int wall = LayerMask.GetMask("Wall");
                bool a = Physics.Raycast(ray, out hit, (cur_node.transform.position - node.transform.position).magnitude, wall);
                node.GetComponent<Collider>().enabled = true;
                if(a){ continue; }

                nodes.Add(node);
            }

            cur_node.GetComponent<Collider>().enabled = true;

            //Vector3 p = GameObject.Find("MeHandle").transform.position;
            Vector3 p = handle.HandlePosition(transform.position);
            float best_direction = float.MaxValue;
            next_node = nodes.Last();

            foreach(GameObject node in nodes){
                //float b = DistancePointLine(p, cur_node.transform.position, node.transform.position);

                Vector3 one_direction = (cur_node.transform.position - node.transform.position).normalized;
                float b = Vector3.Cross(one_direction, p - cur_node.transform.position).magnitude;
                if (b < best_direction){
                    best_direction = b; 
                    next_node = node;
                }
            }

            await handle.SwitchTo(next_node, 1f);

        }
    }

    void FixedUpdate(){
        
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
            gameObject.GetComponent<GameManager>().RefreshClosestCoin();
            Debug.Log("NOMNOMNOM");

        }
    }
}
