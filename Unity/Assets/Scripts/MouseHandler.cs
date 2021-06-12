﻿using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour
{
    private const float _minimumHoldDuration = 0.20f;
    private float _pressedTime;
    private bool _hold;
    private bool rotatingMagnet;

    private GameObject FoundObject;

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
                Debug.Log("!hold");
                if ((Physics.Raycast(ray, out hit)) && (hit.transform.tag == "Magnet"))
                {
                    Debug.Log(string.Format("hit magnet at {0}", (hit.point)));
                    hit.transform.gameObject.GetComponent<MagnetBehavior>().Toggle();
                }
            }
            _hold = false;
            rotatingMagnet = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 v3 = Input.mousePosition;
            v3.z = 10;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(v3);

            if (Time.timeSinceLevelLoad - _pressedTime > _minimumHoldDuration)
            {
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit1;

                if ((Physics.Raycast(ray1, out hit1)) && (hit1.transform.tag == "Magnet"))
                {
                    FoundObject = GameObject.Find(hit1.collider.gameObject.name;);
                    rotatingMagnet = true;
                }

                if (rotatingMagnet)
                {
                    FoundObject.GetComponent<MagnetBehavior>()?.Dragging(worldPoint);
                    Debug.Log("draging magnet");
                }
            }
        }


    }
}
