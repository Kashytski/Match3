using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down);
            if (hit.collider)
            {
                startPosition = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;
            
            switch (Math.Abs(startPosition.x-endPosition.x) - Math.Abs(startPosition.y-endPosition.y))
            {
                case > 0:
                    switch (startPosition.x - endPosition.x)
                    {
                        case > 0:
                            SwipeLeft();
                            break;
                        
                        case < 0:
                            SwipeRight();
                            break;
                    }
                    break;
                
                case < 0:
                    switch (startPosition.y-endPosition.y)
                    {
                        case > 0:
                            SwipeUp();
                            break;
                        
                        case < 0:
                            SwipeDown();
                            break;
                    }
                    break;
            }
        }
    }

    private void SwipeLeft()
    {
        print("SwipeLeft");
    }

    private void SwipeRight()
    {
        print("SwipeRight");
    }
    
    private void SwipeUp()
    {
        print("SwipeDown");
    }
    
    private void SwipeDown()
    {
        print("SwipeUp");
    }
}
