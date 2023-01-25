using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public Transform startPoint;
	public Transform endPoint;
	public float travelTime;
	private Rigidbody rb;
	private Vector3 currentPos;

	private Rigidbody playerRb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate()
	{
		currentPos = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -.5f + .5f);
		rb.MovePosition(currentPos);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			playerRb = other.GetComponent<Rigidbody>();
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			//characterController.Move(rb.velocity * Time.deltaTime);
			//other.GetComponent<CharacterController>().Move(rb.velocity * Time.deltaTime);
			Debug.Log("Player est monté sur une plateforme");
			//playerRb.MovePosition(rb.velocity * Time.deltaTime);
		}
	}
}
