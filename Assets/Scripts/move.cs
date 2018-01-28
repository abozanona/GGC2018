using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
    public GameObject player;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vec = player.transform.position;
         vec.z = transform.position.z;
         //gameObject.transform.position = vec;

        transform.position = Vector3.Lerp(transform.position, vec, speed * Time.deltaTime);
	}
}
