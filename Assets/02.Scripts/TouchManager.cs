using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Ray ray;        //광선을 생성
    private RaycastHit hit; //광선에 충돌한 객체의 정보를 리턴 받을 구조체
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;  //"Main Camera" 테그인 카메라를 리턴
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        ray = camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.green);
        
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 10.0f, 1<<8))
        {
            //Debug.Log($"Hit {hit.collider.name}");
            Destroy(hit.collider.gameObject);
            ExpBox();
        }
        #endif


        #if UNITY_ANDROID
        //손가락 터치 여부
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) //TouchPhase.Began, Stationary, Ended, Moved
            {
                ray = camera.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit, 10.0f, 1<<8))
                {
                    Destroy(hit.collider.gameObject);
                    ExpBox();
                }
            }
        }
        #endif
    }

    void ExpBox()
    {
        Collider[] colls = Physics.OverlapSphere(hit.point, 10.0f, 1<<9);
        foreach(var coll in colls)
        {
            //(횡폭발력, 폭발원점, 반경, 종 폭발력)
            coll.GetComponent<Rigidbody>().AddExplosionForce(1500.0f, hit.point, 10.0f, 1800.0f); 
        }
    }
}
