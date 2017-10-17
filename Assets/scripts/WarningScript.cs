using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour {

    private GameObject cube;
	// Use this for initialization
	void Start () {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = this.transform.position;
        cube.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        cube.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.enabled)
        {
            cube.GetComponent<Renderer>().enabled = true;
        } else
        {
            cube.GetComponent<Renderer>().enabled = false;
        }
	}
}
