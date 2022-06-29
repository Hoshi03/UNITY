# UNITY

20220628

싱글톤 패턴을 이용한 매니저 만들기

transform에 time.deltatime 곱해주기 - 프레임에 상관없이 일정하게 작동
[serialized]를 변수 위에 써서 private 변수를 유니티 내에서 변경 가능


transform.gameObject - 부모 오브젝트의 트렌스폼 바로 연결
input.getkey(keycode.키) - 키 입력 받기


20220629

Position

transform에 time.deltatime을 곱해줘서 속도를 맞춰주기
변수를 퍼블릭으로 선언하면 엔진 상에서 변수값 변경 가능
게임오브젝트도 퍼블릭으로 선언하면 엔진 상에서 생성 가능

new Vector3(0.0f, 0.0f, 1.0f) 처럼 하나씩 입력하지 않고
vector3.foward (back,left) 등도 있으니 그걸로 하자!
로테이션이 적용 된 상태로 트랜스폼을 하면 월드 기준으로 이동하기 때문에 좌표가 이상해짐
키보드 x 키로 플레이어(로컬)기준으로 좌표 이동 가능
코드 상으로는 절대적인 게임 좌표 기준으로 이동
Vector3.forward * Time.deltaTime * _speed - 절대 좌표 기준으로 이동
transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed); - 캐릭터가 돌아가 있어도 월드 기준으로 앞으로 나가게 만들어줌
transform.Translate(Vector3.forward * Time.deltaTime * _speed); - 플레이어가 보고 있는 방향으로 이동함!



vector3

magnitube 로 거리,크기를 나타내는 방향벡터를 구할 수 있음
normalize로 방향에 대한 정보를 가진 단위벡터를 구할 수 있음
Myvector dir = to - from; 로 두점 사이의 거리를 구하고 
dir = dir.nomal;로 단위벡터로 만들어주고
Myvector newpos = from + dir * _speed;로
from에서 원하는 방향으로 _speed만큼 이동

요약 - 벡터는 float3개로 이루어진 구조체이고
위치벡터나 방향벡터로 사용할 수 있다
위치벡터는 좌표간 +-로 구할 수 있고
두 좌표간 뻴셈으로 방향 벡터를 구할수 있음
방향벡터는 거리(magnitude)와
실제 방향(normalized)을 가지고 있고
normalized는 방향은 같지만 크기가 1인 단위벡터를 리턴

Rotation

transform.eularangles  - transform.eularangles - 벡터3로 회전, 절대회전값, 360도를 넘어가면 문제
transform.rotate - 축 기준으로 회전, 오일러보단 로테이트를 쓰자

 
플레이어가 보는 방향으로 이동시키기

quaternion - 인자4개라서 쿼터, vector3만으로는 문제가 생겨서 사용함, 집벌락을 방지.. 자세한 것은 복잡함
quaternion.lookrotation - 특정 방향을 바라보면서 이동하게 만들어줌
transform.rotation = Quaternion.LookRotation(Vector3.right); - 방향을 절대죄표 기준으로 오른쪽을 보게 만들어줌
lookrotaion은 뚝뚝 끊어짐 자연스러운 방법?
loookrotaion을 quarternion.slerp() 안에 넣어줌 - slerp(시작, 도착, 0~1(0은 출발에 가깝고 1은 도착에 가까움)

로테이션 결론
rotation으로 좌표값을 박거나
rotate로 축마다 회전하거나
lookrotaion으로 바라보는 방향으로 이동+slerp로 자연스럽게


translate와 slerp가 들어간 로테이션을 같이 넣으면원하는데로 가지 않고 커브를 그리게 됨
position으로 절대좌표로 이동하게 만들어주면 좀더 자연스럽게 나감

transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
transform.position += (Vector3.back * Time.deltaTime * _speed);