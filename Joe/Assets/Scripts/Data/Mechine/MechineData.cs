using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechine : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    public int cost; 

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }



    private void Awake()
    {
      
    }

    public void LoadData(GameData data)
    {
       
    }

    public void SaveData(GameData data)
    {

    }



}

