using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour
{
    private const float _minimumHoldDuration = 0.20f;
    private float _pressedTime;
    private bool _hold;
    private bool rotatingMagnet;
    private bool mouseOver;
    private bool mouseOver1;
    private int active = 0;

    private GameObject[] magnets; // create an array

    private GameObject FoundObject;
    private GameObject player;
    private GameObject infoBoard;

    Ray _ray;
    Ray _ray1;
    RaycastHit _hit;

    // Use this for initialization  
    void Start ()
    {
        rotatingMagnet = false;
        magnets = GameObject.FindGameObjectsWithTag("Magnet");
        player = GameObject.FindGameObjectWithTag("Player");
        //infoBoard = GameObject.FindGameObjectWithTag("InfoBoard");
        //infoBoard.SetActive(false);

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
                    FoundObject = hit1.transform.gameObject; // GameObject.Find(hit1.collider.gameObject.name);
                    rotatingMagnet = true;
                }

                if (rotatingMagnet)
                {
                    FoundObject.GetComponent<MagnetBehavior>().Dragging(worldPoint);
                }
            }
        }

        ShowRadius();
        ResetPlayer(magnets);
        //InfoBox();
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

    void InfoBox()
    {
        _ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit1;

        if (Physics.Raycast(_ray1, out _hit1))
        {
            if (_hit.transform.tag == "Info" && !mouseOver1)
            {
                infoBoard.SetActive(true);
                mouseOver1 = true;
            }
        }
        else if (mouseOver1)
        {
            infoBoard.SetActive(false);
            mouseOver1 = false;
        }
    }

    void ResetPlayer(GameObject[] magnets)
    {
        active = 0;

        foreach (GameObject m in magnets)
        {
            if (m.GetComponent<MagnetBehavior>().attraction)
            {
                active += 1;
            }
        }

        if (active == 0)
        {
            player.GetComponent<CharacterBehavior>().ResetPlayer();
        }
    }
}
