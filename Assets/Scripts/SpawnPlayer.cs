using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    public float minX, maxX;
    public float minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));

        GameObject temp = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        if (temp.GetComponent<PhotonView>().IsMine)
        {
            temp.GetComponent<PlayerMovement>().SetJoysticks(Instantiate(cameraPrefab, randomPosition, Quaternion.identity));
        }
    }
}