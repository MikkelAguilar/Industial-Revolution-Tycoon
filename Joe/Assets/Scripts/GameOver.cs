using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void setUp() {
        gameObject.SetActive(true);
    }
    public void RestartButton() {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitButton() {
        SceneManager.LoadScene("Menu");
    }
}
