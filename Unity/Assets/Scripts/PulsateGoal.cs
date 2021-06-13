using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsateGoal : MonoBehaviour
{
    private Renderer renderer;
    public Color color1;
    public Color color2;
    float rate = 0.5f;
    public bool animate = true;
    float i = 0f;

    void Start()
    {
        renderer = this.transform.GetComponent<Renderer>();
    }
    void Update()
    {
        if (animate)
        {
            i += Time.deltaTime * rate;
            renderer.material.color = Color.Lerp(color1, color2, Mathf.PingPong(i*2, 1));
            // Blend towards the current target colour
            /*
            renderer.material.color = Color.Lerp(color1, color2, Mathf.PingPong(i * 2, 1));
            // If we've got to the current target colour, choose a new one
            if (i >= 1)
            {
                i = 0;
                Color tempCol = color1;
                color1 = color2;
                color2 = tempCol;
            }
            */
        }
    }
}
