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

    public Vector3 movePos = Vector3.zero;
    public float damping = 2.0f;

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

        if (Input.GetMouseButtonDown(0)
            && Physics.Raycast(ray, out hit, 50.0f, 1<<floorLayer))
        {
            movePos = hit.point;
        }

        Vector3 dir = movePos - tr.position;  //벡터의 뺄셈 연산
        //벡터가 이루고 있는 쿼터니언 각도를 계산
        Quaternion rot = Quaternion.LookRotation(dir);
        //각도를 Slerp 적용
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

    }
}
