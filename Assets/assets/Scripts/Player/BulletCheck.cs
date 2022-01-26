using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCheck : MonoBehaviour
{

    AimAndShoot aimAndShoot;

    private void Awake()
    {
        aimAndShoot = GetComponentInParent<AimAndShoot>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = aimAndShoot.checkPoint;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            aimAndShoot.headNow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            aimAndShoot.headNow = false;            
        }
    }
}
