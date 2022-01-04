using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    public GameObject gameObject;
    void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Goal");
            gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Next()
    {
        Scene sceneLoaded = SceneManager.GetActiveScene();
        if(sceneLoaded.buildIndex == 4)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
        }
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
