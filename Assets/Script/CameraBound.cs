using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    public static CameraBound SharedInstance;

    public float Width;

    private void Awake()
    {
        SharedInstance = this;
        var camera = GetComponent<Camera>();
        
        Width = Mathf.Floor(camera.orthographicSize * camera.aspect);
    }
}
