using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            GetComponent<Rigidbody>().freezeRotation = false;
            Destroy(gameObject, 3f);
        }
    }
}
