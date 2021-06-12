using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour
{

    public int active = 0;
    public float rotation = 0;
    public int radius = 1;

    private const float _minimumHoldDuration = 0.25f;
    private float _pressedTime;
    private bool _hold;
    private bool rotatingMagnet;
    
    // Use this for initialization  
    void Start ()
    {
        _hold = false;
        rotatingMagnet = false;
    }

    // Update is called once per frame  
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressedTime = Time.timeSinceLevelLoad;
            _hold = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!_hold)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if ((Physics.Raycast(ray, out hit)) && (hit.transform.gameObject.tag == "magnet"))
                {
                    Debug.Log(string.Format("hit magnet at {0}", (hit.point)));
                    // this.gameObject.Toggle();
                }
            }
            _hold = false;
            rotatingMagnet = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.timeSinceLevelLoad - _pressedTime > _minimumHoldDuration)
            {
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit1;

                if ((Physics.Raycast(ray1, out hit1)) && (hit1.transform.gameObject.tag == "magnet"))
                {
                    rotatingMagnet = true;
                }

                if (rotatingMagnet)
                {
                    Debug.Log("draging magnet");
                    // this.gameObject.Dragging(hit1.point);
                }
            }
        }
    }    
}
