using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum rocketType{
    Basic = 0,
    Speed,
    Power,
    Count
}
public class RocketController : MonoBehaviour
{
    public Transform[] rocketPrefab;
    [SerializeField] private rocketType currentRocketType = rocketType.Basic;
    [SerializeField] private bool launch;
    [SerializeField] private float launchCD;
    // Start is called before the first frame update
    void Start()
    {
        launch = false;
        launchCD = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {        
        //*** launch rocket ***//
        if (launchCD > 0.0f)
        {
            launchCD -= Time.deltaTime;
        }
        else
        {
            launch = false;
        }
         if (!launch && Input.GetMouseButton(0))
        {
            launch = true;
            launchCD = 1.0f;
            launchRocket();
        }
        
        if (Input.GetAxis("Mouse ScrollWheel")>0f)
        {
            Debug.Log("ScrollWheel Up");
            if((int)currentRocketType >= (int)rocketType.Count-1)
            {
                currentRocketType = rocketType.Basic;
            }
            else
            {
                currentRocketType++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel")<0f)
        {
            Debug.Log("ScrollWheel Down");
            if((int)currentRocketType <= 0 )
            {
                currentRocketType = rocketType.Power;
            }
            else
            {
                currentRocketType--;
            }
        }

    }
    private void launchRocket()
    {
        Transform rocketTransform = Instantiate(rocketPrefab[(int)currentRocketType]);
        RocketMovement rocketMovement = rocketTransform.GetComponent<RocketMovement>();
        rocketMovement.playerTransform = this.transform;
    }
}
