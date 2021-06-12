using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMatHandler : MonoBehaviour
{

    public Material enabledMaterial;
    public Material disabledMaterial;

    public void Enable(bool enabled)
    {
        if(enabled)
            this.GetComponent<Renderer>().material = enabledMaterial;
        else
            this.GetComponent<Renderer>().material = disabledMaterial;
    }
}
