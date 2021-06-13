using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    [HideInInspector] new public Rigidbody2D rigidbody;

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
