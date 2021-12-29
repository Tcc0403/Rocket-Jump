using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentStage;
    public void Save()
    {
        SaveSystem.SaveData(this);
        Debug.Log("Save data");
    }
    public void Load()
    {
        Debug.Log("Load data");
        PlayerData playerData = SaveSystem.LoadData();
        currentStage = playerData.currentStage;

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
