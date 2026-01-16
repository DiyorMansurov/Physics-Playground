using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 velocity)
    {
        rb.AddForce(velocity, ForceMode.Impulse);
    }

}
