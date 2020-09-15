using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(0, 0.1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("Space Pressed");
            transform.position += move;
        }
    }
}
