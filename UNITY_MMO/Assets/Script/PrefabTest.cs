using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    GameObject prefab;

    GameObject tank;

    void Start()
    {

        //게임 오브젝트 씬 안에 생성

        // resoures/Prefabs 폴더 안에 있는 Tank를 꺼내와서 로드함, 바로 생성은 안됨
        //prefab = Resources.Load<GameObject>("Prefabs/Tank");
        //로드한 프리팹을 생성!
        //tank = Instantiate(prefab);

        //로드 + instanciate를 한번에 하는 매니저 클래스에서 호출, 두 단계를 한 단계로 줄임
        tank = Managers.resource.Instanciate("Tank");

        Destroy(tank, 3.0f);
    }

}
