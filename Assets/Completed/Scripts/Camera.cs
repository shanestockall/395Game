using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public Transform player = null;
	public float cameraHeight = 20.0f;
	private Transform cam = null;


	public void Start()
	{
		cam = transform;
	}
	public void Update()
	{
		Vector3 pos = player.position;
		pos.y = cameraHeight;
		cam.position = pos;
	}
}
