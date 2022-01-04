using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Goal");
            Scene sceneLoaded = SceneManager.GetActiveScene();
            if(sceneLoaded.buildIndex == 4)
            {
                SceneManager.LoadScene(0);
            }
            if (sceneLoaded.buildIndex == 0)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
            }
            
        }
    }
}
