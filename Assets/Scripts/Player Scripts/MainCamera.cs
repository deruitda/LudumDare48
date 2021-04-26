using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float moveSpeed = 4f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            //Check if the last place root is not above us by more than 5 tiles 
            if(moveSpeed > 0)
            transform.position += -Vector3.up * moveSpeed * Time.deltaTime;
            else { }
    }
}
