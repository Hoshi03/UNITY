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

    }

    float _yAngle = 0.0f;
    void Update()   
    {
        _yAngle += Time.deltaTime * _speed;
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward),0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left),0.1f);
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
    }
}
