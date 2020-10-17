using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    private Transform tr;
    
    private Ray ray;
    private RaycastHit hit;
    private int floorLayer;
    private Camera camera;

    void Start()
    {
        tr = GetComponent<Transform>();
        floorLayer = LayerMask.NameToLayer("FLOOR");
        camera = Camera.main;  //"MainCamera" 태그가 설정된 카메라를 리턴
    }

    // Update is called once per frame
    void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.green);
        
    }
}
