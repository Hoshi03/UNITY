using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public void SetCanvas(GameObject go, bool sort = true)
    {
        //캔버스 추출
        Canvas canvas = Util.GetorAddComponet<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        //팝업 ui의 경우 만들어둔 order 순서를 이용해서 관리
        if (sort)
        {
            canvas.sortingOrder = (_order);
            _order++;
        }

        //일반 ui는 그냥 0번으로 밀어주기
        else
        {
            canvas.sortingOrder = 0;
        }

    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        //널 체크하고 없으면 T 이름을 넣어줌
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        //리소스 매니저에 작성한 instanciate로 해당 이름 게임오브젝트 만들고
        GameObject go = Managers.resource.Instanciate($"UI/Scene/{name}");
        //게임 오브젝트 가져오기
        T sceneUI = Util.GetorAddComponet<T>(go);

        _sceneUI = sceneUI;

        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
        {
            //게임 오브젝트 만들어주고 이름 붙이기
            root = new GameObject { name = "@UI_Root" };
        }

        //만들어진 게임오브젝트 go의 부모를 root로 지정해서 root안에 go로 만든 게임오브젝트가 들어가게 해주기
        go.transform.SetParent(root.transform);


        return sceneUI;
    }


    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        //널 체크하고 없으면 T 이름을 넣어줌
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        //리소스 매니저에 작성한 instanciate로 해당 이름 게임오브젝트 만들고
        GameObject go = Managers.resource.Instanciate($"UI/Popup/{name}");
        //게임 오브젝트 가져오기
        T popup = Util.GetorAddComponet<T>(go);
        //스택에 푸시
        _popupStack.Push(popup);

        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
        {
            //게임 오브젝트 만들어주고 이름 붙이기
            root = new GameObject { name = "@UI_Root" };
        }

        //만들어진 게임오브젝트 go의 부모를 root로 지정해서 root안에 go로 만든 게임오브젝트가 들어가게 해주기
        go.transform.SetParent(root.transform);


        return popup;
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;
        UI_Popup popup = _popupStack.Pop();
        //팝 한 게임오브젝트 삭제
        Managers.resource.Destroy(popup.gameObject);
        //혹시 모르니 널로 만들기
        popup = null;
        _order--;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;
        if(_popupStack.Peek() != popup)
        {
            Debug.Log("close popup failed");
            return;
        }
        ClosePopupUI();
    }


    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            CloseAllPopupUI();
    }
}
