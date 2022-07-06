using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    //카메라 모드를 설정해줄 enum인 define을 만들고 기본적으로는 쿼터뷰를 들고 있게 해줌
    Define.CameraMode mode = Define.CameraMode.QuarterView;
    
    //플레이어와의 거리를 나타내줄 vector3 변수
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);
    [SerializeField]
    GameObject _player = null;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if(mode == Define.CameraMode.QuarterView)
        {
            RaycastHit hit;
            //레이캐스트 결과가 true면 중간에 벽이 하나 있는 것
            if(Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall"))){
                //벽에 닿은 부분에서 플레이어의 좌표를 빼서 방향벡터 구해주고 0.8배로 앞으로 조금 당겨주기
                float distance = (hit.point - _player.transform.position).magnitude * 0.8f;
                //카메라의 위치를 _delta의 방향에 distance로 당겨온 만큼 이동시켜주기!
                transform.position = _player.transform.position + _delta.normalized * distance;
            }
            else
            {
                //카메라의 포지션을 플레이어의 포지션 + 처음 카메라가 위치했던 방향인 _delta를 해줘서 카메라가 항상 플레이어를 따라다니게 만들어줌
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
    }
}
