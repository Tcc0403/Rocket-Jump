using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerClickHandler
{
    public int SceneIndexDestination = 1;
    public void OnPointerClick(PointerEventData e)
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("current scene name = " + scene.name + "and scene index = " + scene.buildIndex);

        SceneManager.LoadScene(SceneIndexDestination);
    }
}
