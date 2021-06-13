using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehavior : MonoBehaviour
{
    public float strength = 0.2f;
    public float radius = 1f;
    public bool magnetEnabled = false;

    private GameObject player;
    private GameObject positiveDirGO;
    private GameObject negativeDirGO;
    private GameObject positiveGO;
    private GameObject negativeGO;
    public GameObject radiusGO;

    private Vector3 posVec; // Vector from center to positive direction
    private Vector3 negVec; // Vector from center to negative direction
    private Vector3 playerVec; // Vector from center to player

    private float minDot = 0.5f;
    private float minDist;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        positiveDirGO = this.gameObject.transform.Find("Positive").gameObject; 
        negativeDirGO = this.gameObject.transform.Find("Negative").gameObject; 
        positiveGO = this.gameObject.transform.Find("PositiveCube").gameObject; 
        negativeGO = this.gameObject.transform.Find("NegativeCube").gameObject; 
        minDist = positiveGO.transform.lossyScale.y * 1.5f;
        SetRadius();
        UpdateDirectionVectors();
    }

    private void Update()
    {
        if(magnetEnabled)
            CalculateMagnetism();
    }

    public void Toggle()
    {
        magnetEnabled = !magnetEnabled;
        positiveGO.GetComponent<MagnetMatHandler>()?.Enable(this.magnetEnabled);
        negativeGO.GetComponent<MagnetMatHandler>()?.Enable(this.magnetEnabled);
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
        posVec = positiveDirGO.transform.position - this.transform.position;
        posVec.Normalize();

        negVec = negativeDirGO.transform.position - this.transform.position;
        negVec.Normalize();

        playerVec = player.transform.position - this.transform.position;
        playerVec.Normalize();
    }

    public void Dragging(Vector3 mousePos)
    {
        mousePos.z = this.transform.position.z;
        this.transform.LookAt(mousePos);
        this.transform.Rotate(new Vector3(90f, 0f, 0f));
    }

    void Pull(float amount)
    {
        if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) <= minDist ||
            Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) >= radius)
            return;

        Vector3 target = - playerVec;
        player.transform.position += target * strength * amount * Time.deltaTime;
    }

    void Push(float amount)
    {
        if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) <= minDist ||
            Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) >= radius)
            return;

        Vector3 target = playerVec;
        player.transform.position += target * strength * amount * Time.deltaTime;
    }
}