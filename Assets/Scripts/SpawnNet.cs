using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class SpawnNet : NetworkBehaviour
{
    [SerializeField] public NetworkObject objectToSpawn;
    public Transform spawnPoint;
    [SerializeField] public Camera camera;


    private void Start()
    {

    }
    private void Update()
    {
       // [ServerRpc(RequireOwnership = false)]
        if (!IsLocalPlayer)
          {
              return;
          }
        
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CmdSpawnMyCrapServerRpc(hit.point);

        }

     /*   if (Input.GetKeyDown(KeyCode.N))

        {
            GetComponent<NetworkObject>().ChangeOwnership(OwnerClientId);
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }*/
    }


    [ServerRpc(RequireOwnership = false)]
    private void CmdSpawnMyCrapServerRpc(Vector3 spawnPos)
    {
        // Spawn the prefab in normally (on the server)
        NetworkObject prop = Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        // Replicate the object to all clients and give
        // ownership to the client that owns this player
        prop.SpawnWithOwnership(OwnerClientId);
        /* if (Input.GetKeyDown(KeyCode.N))
         {


             prop.ChangeOwnership(OwnerClientId);
             Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
         }



     }*/
      //  [ServerRpc(RequireOwnership = false)]

        //private void ChangeOwnerShipServerRpc()
        {
            GetComponent<NetworkObject>().ChangeOwnership(OwnerClientId);
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
          
    }
}

    

    

    




