using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LayerFixed))]
public class EnemyController : MonoBehaviour {

    private LayerFixed layerController;

    private GameObject player;

    private PlayerController playerController;

    private bool playerIsSpotted = false;

    private bool playerIsCloseEnough = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerController>();


	}
	
	// Update is called once per frame
	void Update () {
        PlayerInSight(playerController, this);
	}

    public bool PlayerInSight(PlayerController player, EnemyController enemy)
    {
        Vector3 forward = enemy.transform.right;
        Vector3 layerInFront = -enemy.transform.forward;
        Vector3 toPlayer = player.transform.position - enemy.transform.position;

        float distance = toPlayer.magnitude;

        float directionForward = Vector3.Dot(forward, toPlayer.normalized);
        float directionInFront = Vector3.Dot(layerInFront, toPlayer.normalized);

        if(directionForward > 0)
        {

        }

        if(directionForward < 0)
        {

        }

        if(directionForward >= .5f || directionInFront >= .5f)
        {
            playerIsSpotted = true;
        }

        Debug.Log(directionForward);

        if (distance <= 100)
        {
            if(playerIsSpotted)
            {
                RaycastHit hit;
                if(Physics.Raycast(enemy.transform.position, toPlayer, out hit, Mathf.Infinity))
                {
                    

                    return true;
                }
            }
            else
            {

            }
        }

        return false;
    }
}
