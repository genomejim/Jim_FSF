using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 2);
	
	}
}
