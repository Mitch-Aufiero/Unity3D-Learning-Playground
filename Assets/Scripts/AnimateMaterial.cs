using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMaterial : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public bool test;


    float curX;
    float curY;
    private new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        curX = renderer.material.mainTextureOffset.x;
        curY = renderer.material.mainTextureOffset.y;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curX += speedX * Time.deltaTime;
        curY += speedY * Time.deltaTime;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(curX, curY));
    }
}
