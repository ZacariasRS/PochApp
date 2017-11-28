using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

    public bool moving;
    public Vector3 pTransform;
    public Vector2 speed;
    public Vector2 direction;
    public float py;

	// Use this for initialization
	void Start () {
        speed = new Vector2(1, 1);
        direction = new Vector2(0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (moving)
        {
            if (transform.position.y < 0)
            {
                transform.position += new Vector3(0f, 0.1f, 0f);
            } else
            {
                transform.position = pTransform;
            }
        }
	}

    public void SetHand(GameObject p)
    {
        //GameObject auxHand = Instantiate(hand, new Vector3(p.transform.position.x, p.transform.position.y, p.transform.position.z), Quaternion.identity);
        pTransform = new Vector3(p.transform.position.x - 2, p.transform.position.y, p.transform.position.z);
        transform.position = pTransform;
        py = p.transform.position.y;
        moving = true;
    }

    public void DestroyHand()
    {
        Destroy(this);
    }
}
