using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlat : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    public float waitTime;
    private void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.5f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                effector2D.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            effector2D.rotationalOffset = 0f;
        }
    }
}
