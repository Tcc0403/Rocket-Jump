using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class RocketMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;

    [SerializeField]private float moveSpeed = 15.0f;
    [SerializeField]private float explosionRange = 4.0f;
    [SerializeField]private float explosionForce=150;
    private void Start()
    {
        transform.forward = cameraTransform.forward;
        // get camera rotation
        transform.rotation = cameraTransform.rotation;
        
        transform.up = transform.forward;
        transform.position = cameraTransform.position + transform.up * 0.5f;
        // start from player's position

        //***
        Collider collider= GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void Update()
    {
        transform.position += moveSpeed * transform.up * Time.deltaTime;
        // move forward

        if (outOfMap())
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.CompareTag("Ground"))
        {// collide with background
            if(explosionNearPlayer())
                BlowUpPlayer();

            Destroy(this.gameObject);
        }
    }

    private bool explosionNearPlayer()
    {
        return (playerTransform.position - transform.position).magnitude < explosionRange;
    }

    private void BlowUpPlayer()
    {
        Rigidbody playerRigidbody = playerTransform.GetComponent<Rigidbody>();
        playerRigidbody.AddForce((explosionRange - (playerTransform.position - transform.position).magnitude) * explosionForce * (playerTransform.position - transform.position));
    }

    private bool outOfMap()
    {
        return transform.position.x < -2000 || transform.position.x > 2000 || transform.position.y < -2000 || transform.position.y > 2000 || transform.position.z < -2000 || transform.position.z > 2000;
    }
}

