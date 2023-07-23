using UnityEngine;
using DualPantoFramework;
public class ObstacleManager : MonoBehaviour
{
    PantoCollider[] pantoColliders;

    Collider[] colliders;

    [SerializeField]
    private GameObject spawnPoint;

    PantoHandle upperHandle;
    PantoHandle lowerHandle;

    async void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();

        lowerHandle.Freeze();
        upperHandle.Freeze();

        colliders = GameObject.FindObjectsOfType<BoxCollider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        
        await upperHandle.MoveToPosition(spawnPoint.transform.position);

   
        await lowerHandle.MoveToPosition(spawnPoint.transform.position);

        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }

        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }

        lowerHandle.Free();
        upperHandle.Free();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (PantoCollider collider in pantoColliders)
            {
                collider.Enable();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (PantoCollider collider in pantoColliders)
            {
                collider.Disable();
            }
        }
    }
}
