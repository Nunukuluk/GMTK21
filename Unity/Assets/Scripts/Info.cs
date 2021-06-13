using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public GameObject infoBoard;
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        infoBoard.SetActive(true);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        infoBoard.SetActive(false);
    }
}
