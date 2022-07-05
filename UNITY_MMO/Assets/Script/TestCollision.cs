using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //부딫힌 오브젝트 이름 출력
        Debug.Log($"name : {other.gameObject.name}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"name : {collision.gameObject.name}");
    }
    void Start()
    {
        
    }

    void Update()
    {
        //레이캐스트 부분
        #region
        //좌표를 월드 기준으로 바꿔주기
        /*Vector3 look = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, look , out hit, 10))
            Debug.Log($"raycast : {hit.collider.gameObject.name}");*/


        //여러개 raycast하는 raycastall 버전!, 여러개니 배열로 raycasthit 선언

        /* RaycastHit[] hits;

         hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);

         foreach(RaycastHit hitted in hits)
         {
             Debug.Log($"raycast : {hitted.collider.gameObject.name}");
         }*/
        #endregion

        //스크린 좌표계
        //Debug.Log(Input.mousePosition);

        //뷰포트 좌표계
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));

        //마우스로 클릭한 부분을 나타냄
        if (Input.GetMouseButtonDown(0))
        {
            //밑에 코드 구현한 것을 한줄로 해주는 ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //8번 레이어를 사용하기 위해 만들어둠
            //int mask = (1 << 8) | ( 1 << 9);

            //위에 것과 다른 버전의 레이어마스크 이용하기
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"raycast camera : {hit.collider.gameObject.name}");
            }



            /*//클릭한 지점의 월드 좌표계, z자리에 카메라 near 위치 가져와서 넣어주기
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            //방향 벡터 구해주기
            Vector3 dir = mousepos - Camera.main.transform.position;
            //단위벡터로 크기 맞춰주기
            dir = dir.normalized;*/

            //Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

            //RaycastHit hit;
            //if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            //    Debug.Log($"raycast camera : {hit.collider.gameObject.name}");
        }
    }
}
