using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour 
{
	public float speed = 10;
	public float gravity = 10;

	private Vector3 moveDirection;
	private CharacterController controller;

	// Use this for initialization
	void Start () 
	{
		moveDirection = Vector3.zero;
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveDirection = new Vector3(moveDirection.x, moveDirection.y - gravity, moveDirection.z+speed*Time.deltaTime);
		if (controller != null) 
		{
			controller.Move(moveDirection);
		}
	
	}
}
