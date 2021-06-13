using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour
{
    private const float _minimumHoldDuration = 0.20f;
    private float _pressedTime;
    private bool _hold;
    private bool rotatingMagnet;
    private bool mouseOver;

    private GameObject FoundObject;

    Ray _ray;
    RaycastHit _hit;

    // Use this for initialization  
    void Start ()
    {
        rotatingMagnet = false;
    }

    // Update is called once per frame  
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressedTime = Time.timeSinceLevelLoad;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.timeSinceLevelLoad - _pressedTime < _minimumHoldDuration)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if ((Physics.Raycast(ray, out hit)) && (hit.transform.tag == "Magnet"))
                {
                    hit.transform.gameObject.GetComponent<MagnetBehavior>().Toggle();
                }
                rotatingMagnet = false;
            }  
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
                    FoundObject = hit1.transform.gameObject;// GameObject.Find(hit1.collider.gameObject.name);
                    Debug.Log(FoundObject.name);
                    rotatingMagnet = true;
                }

                if (rotatingMagnet)
                {
                    FoundObject.GetComponent<MagnetBehavior>().Dragging(worldPoint);
                }
            }
        }

        ShowRadius();
    }

    void ShowRadius()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit))
        {
            if (_hit.transform.tag == "Magnet" && !mouseOver)
            {
                FoundObject = _hit.transform.gameObject; //GameObject.Find(_hit.collider.gameObject.name);
                Debug.Log(FoundObject.name);
                FoundObject.GetComponent<MagnetBehavior>().ToggleRadius();
                mouseOver = true;
            }
        }
        else if (mouseOver)
        {
            FoundObject.GetComponent<MagnetBehavior>().ToggleRadius();
            mouseOver = false;
        }
    }
}
