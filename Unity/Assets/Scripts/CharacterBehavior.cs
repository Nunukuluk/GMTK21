using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{

    private Vector3 position;
    private Quaternion orientation;

    [HideInInspector] new public Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 1;
    }

    public void ResetPlayer()
    {
        this.gameObject.transform.localRotation = Quaternion.identity;
        rigidbody.gravityScale = 1;
    }

    public void RemoveGravity()
    {
        rigidbody.gravityScale = 0;
    }
}
