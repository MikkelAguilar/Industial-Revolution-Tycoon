using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    [SerializeField] public int startingCash = 10000;
    [SerializeField] public int costToHire = 100;
    [SerializeField] public int happinessLost = 100;
    [SerializeField] public int emplyoeesDied = 0;
    public GameOver gameOver;
    public int currentCash;
    public void GameOver() {
        gameOver.setUp();
    }
    void Start()
    {
        currentCash = startingCash;
    }
    void Update() {
        if (Input.GetKeyDown("1")) {
            currentCash += 1000;
        }
        if (Input.GetKeyDown("2")) {
            currentCash -= 1000;
        }
        if (emplyoeesDied >= 5) {
            GameOver();
        }
    }
}
