using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {   
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (rb == null) return;

        Rigidbody otherRb = collision.rigidbody;
        if (otherRb == null) return;

        float impactForce = collision.relativeVelocity.magnitude * otherRb.mass;
        
        if (impactForce > 2f && collision.gameObject.CompareTag("Projectile")){
            rb.isKinematic = false;

            rb.AddForce(collision.relativeVelocity * 0.3f, ForceMode.Impulse);

            if (TryGetComponent<Target>(out Target target))
            {
                StartCoroutine(target.PopAndDestroy());
                UIManager.Instance.CompleteTarget(gameObject.name);
            }
            return;
        }
        

        if (rb.isKinematic == false && otherRb.isKinematic == true)
        {
            otherRb.isKinematic = false;
        }


    }
}