using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = player.position + new Vector3(0, 0, -10);
        transform.position = pos;
        transform.LookAt(player);
        
    }
}
