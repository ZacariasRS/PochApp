using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {

    public int rank;
    public char palo;
    public int player;

    private SpriteRenderer mySpriteRenderer;
    private Sprite mySprite;
    private bool alreadyClicked;
    private Vector3 initialPosition;
    private bool inCenter;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start () {
        alreadyClicked = false;
        initialPosition = transform.position;
        inCenter = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (!inCenter)
        {
            if (alreadyClicked)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 0);
                transform.position = initialPosition;

                alreadyClicked = false;
            }
            else
            {
                transform.localScale = new Vector3(2.0f, 2.0f, 0);
                transform.position = new Vector3(transform.position.x, transform.position.y, -3.0f);
                alreadyClicked = true;
            }
        }
    }
    
    private void OnMouseDrag()
    {
        if (!inCenter)
        {
            if (alreadyClicked)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                //Debug.Log(mousePosition);
                mousePosition.z = -3f;
                transform.position = mousePosition;
            }
        }
        
    }
    public void CreateCard(Sprite s, int r, char p)
    {
        mySprite = s;
        //mySpriteRenderer.sprite = s;
        rank = r;
        palo = p;
    }

    public void ActivateCard(bool imPlayer)
    {
        if (mySpriteRenderer != null)
        {
            if (imPlayer)
            {
                mySpriteRenderer.sprite = mySprite;
            }
            else
            {
                Sprite backSprite = Resources.Load<Sprite>("back");
                mySpriteRenderer.sprite = backSprite;
            }
        }
    }

    public void EnterCenter()
    {
        transform.localScale -= new Vector3(1.0f, 1.0f, 0);
        alreadyClicked = false;
        inCenter = true;
    }

    public char GetPalo()
    {
        return palo;
    }

    public void SetPlayer(int p)
    {
        player = p;
    }

    public int GetPlayer()
    {
        return player;
    }

    public void ReturnToInitialPosition()
    {
        transform.position = initialPosition;
    }

    public int GetRank()
    {
        return rank;
    }
    
}
