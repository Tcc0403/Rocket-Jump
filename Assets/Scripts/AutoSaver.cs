using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Save();            
        }
    }
}
