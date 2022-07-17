using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//벡터 만들면서 배워보자
struct Myvector
{
    public float x;
    public float y;
    public float z;

    //방향 벡터 구하기 - 거리(크기) 구하기 공식!
    public float magni { get { return Mathf.Sqrt(x * x + y * y + z * z); } }

    //단위 벡터 구하기
    public Myvector nomal { get { return new Myvector(x / magni, y / magni, z / magni); } }

    public Myvector(float x, float y, float z) 
    {
        this.x = x; this.y = y; this.z = z;
    }

    public static Myvector operator +(Myvector a, Myvector b)
    {
        return new Myvector(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static Myvector operator -(Myvector a, Myvector b)
    {
        return new Myvector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Myvector operator *(Myvector a, float d)
    {
        return new Myvector(a.x * d, a.y * d, a.z * d);
    }



}


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destpos;


    public enum PlayerState
    {
        Die,
        Moving,
        Idle
    }

    PlayerState _state = PlayerState.Idle;
    void Start()
    {
        #region
        Myvector from = new Myvector(5.0f, 0.0f, 0.0f);
        Myvector to = new Myvector(10.0f, 0.0f, 0.0f);
        // 두점 사이의 거리 구하기
        Myvector dir = to - from;
        // 단위벡터로 방향 구하기
        dir = dir.nomal;

        //form에서 원하는 방향으로 스피드 만큼 이동
        Myvector newpos = from + dir * _speed;

        //방향 벡터로  a와b 사이의 거리와 실제 방향을 알 수 있음
        #endregion
        //기존 호출 있으면 빼주기
        //Managers.Input.keyaction -= Onkeyboard;
        //Managers.Input.keyaction += Onkeyboard;
        Managers.Input.MouseAction -= OnmouseClicked;
        Managers.Input.MouseAction += OnmouseClicked;

        //UI 불러오기 resourcemanager 이용
        //Managers.resource.Instanciate("UI/UI_BTN");

        //Managers.UI.ShowPopupUI<UI_Button>("UI_Button");
        //Managers.UI.ClosePopupUI(); 
    }

    void UpdateMoving()
    {
        //방향벡터 구해주기
        Vector3 dir = _destpos - transform.position;
        dir.y = 0.0f;

        //목적지에 도달해서 크기가 거의 미묘해지면 멈춰주기
        if (dir.magnitude < 0.00001f)
            _state = PlayerState.Idle;
        else
        {
            //clamp 함수를 이용해서 0~ dir 최대값 사이에 이동할 거리가 들어가게 만들어줌
            float moveDistance = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            //포지션을 dir 쪽으로 옮겨주기, 방향벡터의 크기도 1로 바꾸고 스피드와 델타타임을 곱해줘서 일정하게 만들어준다!
            transform.position += dir.normalized * moveDistance;

            //그냥 lookat으로 하면 동작이 너무 부자연스러워서 자연스럽게 quternion을 사용해서 자연스럽게 만들어주기
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            //플레이어가 목적지를 바라보면서 이동하게 만들어주기
            //transform.LookAt(_destpos);
        }

        Animator anime = GetComponent<Animator>();
        //animator 속성 가져오기
        //만들어둔 float형 파라미터 Speed에 현재 스피드를 넘겨줌
        anime.SetFloat("Speed", _speed);

    }

    void UpdateIdle()
    {
        Animator anime = GetComponent<Animator>();
        //Speed 파라미터를 0으로 바꿔서 캐릭터를 멈추게 해줌
        anime.SetFloat("Speed", 0);

    }

    void UpdateDie()
    {

    }

    void Update()   
    {
        switch (_state)
        {
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
            
        
    }

    void Onkeyboard()
    {
        //플레이어가 죽어있는 상태면 바로 리턴
        if (_state == PlayerState.Die)
            return;
        //절대 회전값
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // y축 기준으로 돌려주기
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime*100.0f, 0.0f));


        //quarternion을 이용한 이동
        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));


        //transformdirection - local을 world로 변경
        if (Input.GetKey(KeyCode.W))
        {
            //캐릭터가 보고 있는 방향 기준으로 이동
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);

            //translate로 이동하면 커브가 그려져서  transform.position 사용
            transform.position += (Vector3.forward * Time.deltaTime * _speed);

            //자연스럽게 돌아가기(slerp), 보고 있는 방향으로(lookrotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.1f);
            transform.position += (Vector3.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
            transform.position += (Vector3.back * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.1f);
            transform.position += (Vector3.right * Time.deltaTime * _speed);
        }
        _state = PlayerState.Idle;
    }

    void OnmouseClicked(Define.MouseEvent _event)
    {
        //플레이어가 죽어있는 상태면 바로 리턴
        if (_state == PlayerState.Die)
            return;
        //if (_event != Define.MouseEvent.Click)
        //    return;

        //밑에 코드 구현한 것을 한줄로 해주는 ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        //8번 레이어를 사용하기 위해 만들어둠
        //int mask = (1 << 8) | ( 1 << 9);

        //위에 것과 다른 버전의 레이어마스크 이용하기

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destpos = hit.point;
            _state = PlayerState.Moving;
            //Debug.Log($"raycast camera : {hit.collider.gameObject.name}");

        }
    }
     
    void OnRunEvent()
    {
        Debug.Log("뚜벅 뚜벅~");
    }
}
