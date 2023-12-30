using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionGravity : MonoBehaviour
{
    public void OnCollisionEnter(Collision col)
    {
        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }
    }
}
