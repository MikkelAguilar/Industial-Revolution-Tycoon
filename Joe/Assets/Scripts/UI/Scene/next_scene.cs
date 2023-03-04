using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class next_scene : MonoBehaviour
{
    public void NextScene()
    {
        
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    
}
