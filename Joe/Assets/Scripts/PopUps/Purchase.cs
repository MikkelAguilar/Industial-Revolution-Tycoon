using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    [SerializeField]
    public GameObject _mechine;
    public Texture2D cursorArrow;
    [SerializeField] GameObject mechines;

    
    
    
        
      //CursorToMechine();

        

    

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0)&& purchase == true)
        {
            
             Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             Instantiate(mechine, new Vector3(cursorPos.x, cursorPos.y, 0), Quaternion.identity);
            purchase = false;   
        }*/
        if (Input.GetKeyDown("p"))
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject newMechine = Instantiate(_mechine, new Vector3(cursorPos.x, cursorPos.y, 0), Quaternion.identity);
            newMechine.transform.SetParent(mechines.transform);

        }
    }

    void makepurchase()
    {
        //purchase = true;
    }


    /*void OnMouseEnter()
    {
        col = true;
    }

    void OnMouseExit()
    {
        col = false;*/
    //}
    /* void CursorToMechine()
     {

         Vector2 hotSpot = new Vector2(3, 5);
         Cursor.SetCursor(cursorArrow, hotSpot, CursorMode.ForceSoftware);
     }

     void Cancel()
     {
         Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
     }*/
}
