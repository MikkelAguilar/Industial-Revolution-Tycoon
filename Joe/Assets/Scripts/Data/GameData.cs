using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int money;
    //public SerializableDictionary<string, float[]> mechinedata;

    public GameData()
    {
        this.money = 10000;
        //mechinedata = new SerializableDictionary<string, float[]>();

    }
}