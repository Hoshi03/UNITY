using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{

    //제네릭으로 경로 받아서 타입이 오브젝트 인것만 가져오기
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    //parent는 게임 내 부모 오브젝트가 있으면 붙여주기 위해서 사용?
    public GameObject Instanciate(string path, Transform parent = null)
    {
        //경로를 받아서 프리팹 로드
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        //널체크
        if(prefab == null)
        {
            Debug.Log($"failed to load prefab, path : {path}");
            return null;
        }
        //널이 아니면 오브젝트 생성
        return Object.Instantiate(prefab, parent);
    }
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        Object.Destroy(go);
    }

}
