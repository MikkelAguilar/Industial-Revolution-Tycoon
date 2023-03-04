using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float speed = 1.0f;

    private Vector3 moveLeft;
    private Vector3 moveRight;
    private Vector3 moveUp;
    private Vector3 moveDown;

    GameObject background;
    SpriteRenderer backgroundSprite;

    float camWidth;
    float camHeight;
    void Start()
    {
        moveLeft = new Vector3(-speed, 0, 0);
        moveRight = new Vector3(speed, 0, 0);
        moveUp = new Vector3(0, speed, 0);
        moveDown = new Vector3(0, -speed, 0);
        background = GameObject.Find("Background");
        backgroundSprite = background.GetComponent<SpriteRenderer>();
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }
    void Update()
    {
        PanCamera();
    }

    private void PanCamera() 
    {
        float bgEndX = background.transform.position.x + (backgroundSprite.bounds.size.x / 2);
        float bgStartX = background.transform.position.x - (backgroundSprite.bounds.size.x / 2);
        float bgEndY = background.transform.position.y - (backgroundSprite.bounds.size.y / 2);
        float bgStartY = background.transform.position.y + (backgroundSprite.bounds.size.y / 2);

        float cameraMidHorizontal = camWidth / 2;
        float cameraMidVeritcal = camHeight / 2;
        
        if (Input.GetKey("a") && cam.transform.position.x > bgStartX + cameraMidHorizontal + 1)
        {
            cam.transform.position += moveLeft;
        }
        else if (Input.GetKey("d") && cam.transform.position.x < bgEndX - cameraMidHorizontal - 1)
        {
            cam.transform.position += moveRight;
        }
        else if (Input.GetKey("w") && cam.transform.position.y < bgStartY - cameraMidVeritcal - 1)
        {
            cam.transform.position += moveUp;
        }
        else if (Input.GetKey("s") && cam.transform.position.y > bgEndY + cameraMidVeritcal + 1)
        {
            cam.transform.position += moveDown;
        }
    }
}
