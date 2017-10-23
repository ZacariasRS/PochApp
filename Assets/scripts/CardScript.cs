using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {

    public int rank; // valor de la carta
    public char palo; // palo al que pertenece
    public int player; // jugador al que pertenece la carta
    public int moving;
    public int speed;

    private SpriteRenderer mySpriteRenderer;
    private Sprite mySprite;
    public bool alreadyClicked;
    private Vector3 initialPosition;
    public bool inCenter;
    //private Shader def;
    //public Shader grayScale;
    public Material def;
    public Material grayScale;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //def = mySpriteRenderer.material;
        moving = -1;
        speed = 4;
    }
    // Use this for initialization
    void Start () {
        alreadyClicked = false;
        inCenter = false;
	}
	
	// Update is called once per frame
	void Update () {
		switch (moving)
        {
            case -1:
                break;
            case 0:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
        }
	}

    private void OnMouseDown()
    {
        if (!inCenter)
        {
            if (alreadyClicked)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                ReturnToInitialPosition();

                alreadyClicked = false;
            }
            else
            {
                transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
                transform.position = new Vector3(transform.position.x, transform.position.y, -3.0f);
                alreadyClicked = true;
            }
        }
    }

    private void OnMouseUp()
    {
        if (!inCenter)
        {
            alreadyClicked = false;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            ReturnToInitialPosition();
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
                mySpriteRenderer.enabled = true;
            }
            else
            {
                Sprite backSprite = Resources.Load<Sprite>("back");
                mySpriteRenderer.sprite = backSprite;
                mySpriteRenderer.enabled = true;
            }
        }
        //SaveInitialPosition();
    }

    public void HideCard()
    {
        if (mySpriteRenderer != null)
        {
            mySpriteRenderer.enabled = false;
            transform.position = new Vector3(10, 10, 10);
            inCenter = false;
        }
        else Debug.Log("Hide Card: mySpriteRenderer == null");
    }

    public void EnterCenter(bool imP)
    {
        if (imP)
        {
            Debug.Log("ImPlayer Scale down");
            transform.localScale -= new Vector3(1.0f, 1.0f, 1.0f);
        } else
        {
            mySpriteRenderer.sprite = mySprite;
        }
        switch(player)
        {
            case 0:
                transform.position = new Vector3(0f, -1.75f, -3f);
                break;
            case 1:
                transform.position = new Vector3(2.75f, 0f, -3f);
                break;
            case 2:
                transform.position = new Vector3(0f, 1.75f, -3f);
                break;
            case 3:
                transform.position = new Vector3(-2.75f, 0f, -3f);
                break;
            default:
                break;
        }
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

    public void SaveInitialPosition()
    {
        initialPosition = transform.position;
    }
    public void ReturnToInitialPosition()
    {
        transform.position = initialPosition;
    }

    public int GetRank()
    {
        return rank;
    }
    
    public void SetGreyMaterial()
    {
        mySpriteRenderer.material  = grayScale;
    }

    public void SetDefMaterial()
    {
        mySpriteRenderer.material = def;
    }
    
    public Sprite GetSprite()
    {
        return mySprite;
    }
}
