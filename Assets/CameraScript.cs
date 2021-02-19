using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform followObject;
    private Vector3 moveTemp;
    public float offsetX = 2;

    // Use this for initialization
    void Start()
    {
        moveTemp = followObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveTemp = followObject.transform.position;
        moveTemp.x += offsetX;
        moveTemp.y = transform.position.y;
        moveTemp.z = transform.position.z;
        transform.position = moveTemp;
    }
}
