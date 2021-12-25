using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    public Transform rocketPrefab;
    public float xRotation;

    private Rigidbody playerRigidbody;
    [SerializeField] private bool falling;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool run;
    [SerializeField] private float runSpeed;
    [SerializeField] private bool launch;
    [SerializeField] private float launchCD;

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

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            run = true;
            transform.position += runSpeed * transform.forward * Time.deltaTime;
        }// press 'W' key to move forward
        if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            run = true;
            transform.position -= runSpeed * transform.forward * Time.deltaTime;
        }// press 'S' key to move backward
        //*** move forward or backward ***//

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            run = true;
            transform.position -= runSpeed * transform.right * Time.deltaTime;
        }// press 'A' key to move left
        if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            run = true;
            transform.position += runSpeed * transform.right * Time.deltaTime;
        }// press 'D' key to move right
        //*** move left or right ***//

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

        Debug.Log("PlayerOnCollisionEnter : " + collision.gameObject.name);
    }

    private void launchRocket()
    {
        Transform rocketTransform = Instantiate(rocketPrefab);
        RocketMovement rocketMovement = rocketTransform.GetComponent<RocketMovement>();
        rocketMovement.playerTransform = this.transform;
    }
}
