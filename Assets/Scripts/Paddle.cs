using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField] [Range(1.0f, 100.0f)] float m_paddleSpeed;
    	
	void Update () {

        Vector3 velocity = Vector3.zero;

        velocity.x = Input.GetAxis("Horizontal");

        velocity *= m_paddleSpeed * Time.deltaTime;

        transform.position += velocity;
	}
}
