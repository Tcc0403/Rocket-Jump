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
    private Rigidbody playerRigidbody;
    [SerializeField] private bool falling;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool run;
    [SerializeField] private float runSpeed;
    [SerializeField] private bool launch;
    [SerializeField] private float launchCD;
    [SerializeField] private rocketType currentRocketType = rocketType.Basic;

    
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerRigidbody = GetComponent<Rigidbody>();

        falling = false;
        jumpForce = 300.0f;
        run = false;
        runSpeed = 7.5f;
        launch = false;
        launchCD = 0.0f;
    }

    private void Update()
    {
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
        if (collision.gameObject.name.Contains("Cube"))
        {
            falling = false;
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


        Debug.Log("PlayerOnCollisionEnter : " + collision.gameObject.name);
    }

    private void launchRocket()
    {
        Transform rocketTransform = Instantiate(rocketPrefab[(int)currentRocketType]);
        RocketMovement rocketMovement = rocketTransform.GetComponent<RocketMovement>();
        rocketMovement.playerTransform = this.transform;
    }
}
