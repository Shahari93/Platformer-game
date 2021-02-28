using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform transformToFollow = null;

    private void LateUpdate()
    {
        transform.position = new Vector3(transformToFollow.position.x, transformToFollow.position.y, transform.position.z);
    }
}
