using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    public GameObject player;
    private Vector3 displacement;
	// Use this for initialization
	void Awake () {
        displacement = player.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position - displacement;
	}
}
