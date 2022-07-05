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
            //카메라의 포지션을 플레이어의 포지션 + 처음 카메라가 위치했던 방향인 _delta를 해줘서 카메라가 항상 플레이어를 따라다니게 만들어줌
            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform);
        }
    }
}
