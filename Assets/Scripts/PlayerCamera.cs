using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    public float speed = 10f;
    public Vector3 playerOffset;
	
	private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + playerOffset, Time.deltaTime * speed);
	}
}
