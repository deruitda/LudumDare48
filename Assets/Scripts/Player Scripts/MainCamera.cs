using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float moveSpeed = 4f;
    private float zoomSize = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int startingPoint = 3;
        int increment = 10;
        float defaultZoom = 4.600302f;
        var a = GetComponent<Camera>().orthographicSize;
        float zoom = transform.position.y + defaultZoom / increment;

        if (zoom < startingPoint)
        {
            zoom = defaultZoom;
        }

        if (Input.GetKey(KeyCode.W)) 
        { 
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            if (transform.position.y > 2.5 && transform.position.y % 10 == 0)
            {
                zoomSize++;
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {
            if(transform.position.y > 2.5 && transform.position.y % 10 == 0)
            {
                zoomSize--;
            }
            //Check if the last place root is not above us by more than 5 tiles 
            if(moveSpeed > 0)
            transform.position += -Vector3.up* moveSpeed * Time.deltaTime;
            
        }
        else { }
        GetComponent<Camera>().orthographicSize = zoom;

    }
}
