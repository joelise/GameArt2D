using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float zoomAmount;


    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, zoomAmount);
    }
}
