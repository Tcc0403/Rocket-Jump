using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int currentStage;
    public void Save()
    {
        currentStage = SceneManager.GetActiveScene().buildIndex;
        SaveSystem.SaveData(this);
        Debug.Log("Save data");
    }
    public void Load()
    {
        Debug.Log("Load data");
        PlayerData playerData = SaveSystem.LoadData();
        currentStage = playerData.currentStage;
        if(currentStage == SceneManager.GetActiveScene().buildIndex)
            LoadPosition(playerData);
        
    }
    void LoadPosition(PlayerData playerData)
    {
        Vector3 position;
        position.x = playerData.position[0]; 
        position.y = playerData.position[1]; 
        position.z = playerData.position[2]; 

        transform.position = position;
    }
    void Start()
    {
        Load();
    }
}
