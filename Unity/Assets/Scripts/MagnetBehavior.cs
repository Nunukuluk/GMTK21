using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehavior : MonoBehaviour
{
    public float strength = 0.2f;
    public float radius = 1f;

    private GameObject player;
    private GameObject positive;
    private GameObject negative;
    private GameObject radiusGO;

    private Vector3 posVec; // Vector from center to positive direction
    private Vector3 negVec; // Vector from center to negative direction
    private Vector3 playerVec; // Vector from center to player

    private float minDot = 0.5f;
    private float minDist = 0.1f;

    private bool magnetEnabled = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        positive = GameObject.Find("Positive");
        negative = GameObject.Find("Negative");
        radiusGO = GameObject.Find("Radius");
        SetRadius();
        UpdateDirectionVectors();
    }

    public void Toggle()
    {
        this.magnetEnabled = !this.magnetEnabled;
    }

    public void ToggleRadius()
    {
        radiusGO.SetActive(!radiusGO.activeSelf);
    }

    void SetRadius()
    {
        radiusGO.transform.localScale = new Vector3(radius * 2f, radius * 2f, radiusGO.transform.localScale.z);
        ToggleRadius();
    }

    void CalculateMagnetism()
    {
        UpdateDirectionVectors();

        float posDot = Vector3.Dot(posVec, playerVec);
        float negDot = Vector3.Dot(negVec, playerVec);

        if(posDot >= minDot)
        {
            Pull(posDot);
            return;
        }

        else if(negDot >= minDot)
            Push(negDot);
    }

    void UpdateDirectionVectors()
    {
        posVec = positive.transform.position - this.transform.position;
        posVec.Normalize();

        negVec = negative.transform.position - this.transform.position;
        negVec.Normalize();

        playerVec = player.transform.position - this.transform.position;
        playerVec.Normalize();
    }

    public void Dragging(Vector3 mousePos)
    {
        if (!magnetEnabled)
            return;

        mousePos.z = this.transform.position.z;
        this.transform.LookAt(mousePos);
        this.transform.Rotate(new Vector3(90f, 0f, 0f));
        CalculateMagnetism();
    }

    void Pull(float amount)
    {
        if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) <= minDist)
            return;

        Vector3 target = - playerVec;
        player.transform.position += target * strength * amount * Time.deltaTime;
    }

    void Push(float amount)
    {
        if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) >= radius)
            return;

        Vector3 target = playerVec;
        player.transform.position += target * strength * amount * Time.deltaTime;
    }
}
