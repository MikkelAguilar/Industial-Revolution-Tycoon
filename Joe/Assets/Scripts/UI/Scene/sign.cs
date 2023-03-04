using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign : MonoBehaviour
{
    public Texture2D cursorArrow;
    private Vector2 cursorHotspot;
    public Camera m_camera;
    public GameObject brush;
    
    LineRenderer currentLineRenderer;
    
    bool inSignArea = false;
    Vector2 lastPos;

    private void Update()
    {
        Drawing();
        
    }

    void Drawing()
    {
        
        if (inSignArea)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CreateBrush();
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                PointToMousePos();
            }
            else
            {
                currentLineRenderer = null;
            }
        }
        
    }



    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

    void OnMouseEnter()
    {

        inSignArea = true;
        cursorHotspot = new Vector2(0, cursorArrow.height);
        Cursor.SetCursor(cursorArrow, cursorHotspot, CursorMode.ForceSoftware);

    }

    void OnMouseExit()
    {
        inSignArea = false;
        Cursor.SetCursor(null, Vector2.zero , CursorMode.ForceSoftware);
    }


}
