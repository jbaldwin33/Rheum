using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshScroll : MonoBehaviour {

    float scrollSpeed = 0.3f;
    Mesh mesh;
    public enum Quarter {first, second, third, fourth};
    public Quarter q;
    void Start()
    {
        mesh = transform.GetComponent<MeshFilter>().mesh;
    }

    void Update()
    {
        SwapUVs();
    }

    void SwapUVs()
    {
        Vector2[] uvSwap = mesh.uv;
        for (int b = 0; b < uvSwap.Length; b++)
        {
            if (q == Quarter.first)
            {
                uvSwap[b] += new Vector2(scrollSpeed * Time.deltaTime / 2, -scrollSpeed * Time.deltaTime);
            }
            if (q == Quarter.second)
            {
                uvSwap[b] += new Vector2(-scrollSpeed * Time.deltaTime / 2, -scrollSpeed * Time.deltaTime);
            }
            if (q == Quarter.third)
            {
                uvSwap[b] += new Vector2(-scrollSpeed * Time.deltaTime / 2, scrollSpeed * Time.deltaTime);
            }
            if (q == Quarter.fourth)
            {
                uvSwap[b] += new Vector2(scrollSpeed * Time.deltaTime / 2, scrollSpeed * Time.deltaTime);
            }
        }

        mesh.uv = uvSwap;
    }
}
