using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
	//########## Script qui déplace les plateformes mouvantes ##########
    public Transform startPoint;
	public Transform endPoint;
	public float travelTime;
	private Rigidbody rb;
	private Vector3 currentPos;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate()
	{
		currentPos = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -.5f + .5f);
		rb.MovePosition(currentPos);
	}
}
