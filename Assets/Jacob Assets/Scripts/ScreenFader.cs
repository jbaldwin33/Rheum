using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

    private Image imag;
    public float alpha;
    public float speed = 1f;
    private Color col;

	// Use this for initialization
	void Start () {
        imag = GetComponent<Image>();
        col = imag.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (col.a < alpha)
        {
            col.a += speed * Time.deltaTime;
            if (col.a > alpha)
            {
                col.a = alpha;
            }
        }

        if (col.a > alpha)
        {
            col.a -= speed * Time.deltaTime;
            if (col.a < alpha)
            {
                col.a = alpha;
            }
        }
        imag.color = col;
	}

    public void forceBlack()
    {
        col.a = 1f;
        imag.color = col;
    }

    public void forceClear()
    {
        col.a = 0f;
        imag.color = col;
    }
}
