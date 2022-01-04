using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum rocketType{
    Basic = 0,
    Speed,
    Power
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    public Transform[] rocketPrefab;
    public float xRotation;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private Vector3 collisionNormal;
    private Rigidbody playerRigidbody;
    
    [SerializeField] private bool falling;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool run;
    [SerializeField] private float runSpeed;
    [SerializeField] private bool launch;
    [SerializeField] private float launchCD;
    [SerializeField] private rocketType currentRocketType = rocketType.Basic;
    [SerializeField] private float slopeLimit = 60f;
    [SerializeField] private bool isOnSlope = false;

    
    
    
    private void Start()
    {        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerRigidbody = GetComponent<Rigidbody>();

        falling = false;
        jumpForce = 200.0f;
        run = false;
        runSpeed = 30f;
        launch = false;
        launchCD = 0.0f;
    }

    private void Update()
    {
        //Debug.Log("gravity:" + Physics.gravity);
        run = false;
        if (launchCD > 0.0f)
        {
            launchCD -= Time.deltaTime;
        }
        else
        {
            launch = false;
        }
        
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));        
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * runSpeed;
        //Debug.Log("MoveVector Before: "+ MoveVector);
        if(isOnSlope)
        {
            //Debug.Log("Is on slope");
            MoveVector += collisionNormal*MoveVector.magnitude;
        }
        //Debug.Log("MoveVector After: "+MoveVector);
        //Debug.Log("collisionNomral: "+collisionNormal) ;
        
        
        playerRigidbody.velocity = new Vector3(MoveVector.x, playerRigidbody.velocity.y, MoveVector.z);
        
        if(playerRigidbody.velocity.magnitude!=0) run = true;
        
        

        if (!falling && Input.GetKey(KeyCode.Space))
        {
            falling = true;
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }// press "Space" key to jump
        //*** jump ***//

        if (!launch && Input.GetMouseButton(0))
        {
            launch = true;
            launchCD = 1.0f;
            launchRocket();
        }
        //*** launch rocket ***//
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            for(int i=0; i<collision.contactCount; i++) //check every contact
            {
                collisionNormal = collision.GetContact(i).normal;            
                
                if(collisionNormal != Vector3.up) isOnSlope = true; //slope detection         
                else isOnSlope = false;

                //Debug.Log("Angle between player and contact:"+Vector3.Angle(Vector3.up, collisionNormal));
                //Debug.Log("Normal vector:" + collisionNormal);
                if(!(Vector3.Angle(Vector3.up, collisionNormal) <= slopeLimit)) //collision's slope is too steep
                {
                    
                    falling = true;
                }
                else
                {
                    falling = false;
                    break;
                }                    
            }
            
        }
        // finish jump
        /*
        if (collision.gameObject.name.Contains("Again"))
        {
            transform.position = new Vector3(10, -44, -90);
            transform.forward = Vector3.forward;
            xRotation = 0.0f;
        }*/

        if (collision.gameObject.name.Contains("Finish"))
        {
            Debug.Log("Win");
        }


        Debug.Log("PlayerOnCollisionEnter : " + collision.gameObject.tag);
        
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            for(int i=0; i<other.contactCount; i++) //check every contact
            {
                collisionNormal = other.GetContact(i).normal;    

                //Debug.Log("Angle between player and contact:"+Vector3.Angle(Vector3.up, collisionNormal));
                //Debug.Log("Normal vector:" + collisionNormal);
                if(collisionNormal != Vector3.up) isOnSlope = true; //slope detection         
                else isOnSlope = false;

                if((Vector3.Angle(Vector3.up, collisionNormal) > slopeLimit)) //collision's slope is too steep
                {                
                    falling = true;
                }
                else
                {
                    falling = false;
                    break;
                }                    
            }
        }
    }
    private void OnCollisionExit(Collision other) {
        
       falling = true;
    }
    private void launchRocket()
    {
        Transform rocketTransform = Instantiate(rocketPrefab[(int)currentRocketType]);
        RocketMovement rocketMovement = rocketTransform.GetComponent<RocketMovement>();
        rocketMovement.playerTransform = this.transform;
    }
    
}
