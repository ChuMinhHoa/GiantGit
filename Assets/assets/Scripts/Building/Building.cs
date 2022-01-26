using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{

    public float width;
    public float yForce;

    bool kicked = false;

    Rigidbody mybody;

    float waitTime = 2f;

    float x;
    public float rotageSpeed;

    private void Awake()
    {
        
        mybody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if ( kicked )
        {
            x += Time.deltaTime * rotageSpeed;
            transform.eulerAngles = new Vector3(x, x, x);

            Destroy(gameObject, waitTime);
        }    
    }

    public void Kicked(Transform whatKick, float kickForce) {
        gameObject.layer = 0;
        kicked = true;
        Vector3 direction = transform.position - whatKick.position;
        direction.y = yForce;
        mybody.AddForce(direction * (kickForce),
                ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, width);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Building build = collision.gameObject.GetComponent<Building>();
            build.Kicked(collision.gameObject.transform, 5);
        }
    }
}
