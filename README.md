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

0630 

프리팹, 프리팹 생성하는 리소스 매니저 만들기

action, func, 리스너 패턴, 제네릭, 싱글톤, 프로퍼티 복습 필요!

프리팹 

에셋 폴더에 만든 게임 오브젝트를 넣어서 만듦
프리팹에 오브젝트는 프리펩만 수정하면 다 일괄적으로 적용됨
프리펩 수정 - 프리팹 두번 클릭해서 수정 or 오브젝트 우측 화살표로 수정
프리팹에서 수정하면 일괄 수정이 되지만
객체에세 값을 수정하면 수정된 부분이 진해지면서 값이오버라이드 됨

nestde prefab - 프리팹 안에 다른 프리팹을 포함하는 프리팹, 프리펩을 중첩해서 사용, 프리팹 여러개를 묶고 묶은 덩어리로 프리팹을 만들 수 있음

prefab varient - 프리팹 상속 
만들어진 프리팹을 다시 에셋에 넣으면 새 프리펩 or 상속으로 나오고 상속한 프리팹으로 기존 프리팹 받아서 수정 가능 

Resource Manager

instantiate 함수 - 코드상으로 scene에 오브젝트 생성
asset - resources - 코드 제외 리소스 넣는 폴더 형태로 resources 안에 폴더 만들어주자


prefab = Resources.Load<GameObject>("Prefabs/Tank"); - resoures/Prefabs 폴더 안에 있는 Tank를 꺼내와서 로드함, 바로 생성은 안됨
tank = Instantiate(prefab); - 로드한 프리팹을 생성!
Destroy(tank, 3.0f); - 만든 탱크 삭제

0705

Collider

rigidbody - 물리 적용
mass - 질량
iskinematic - 물리에 영향을 받지 않게 해줌

충돌 처리할때 쓰는 함수 - oncollisionenter
rigidbody가  있고 kinematic은 꺼진 상태고
collider가 있고  istrigger가 꺼진 상태고
부딫칠 오브젝트도 collider가 있고  istrigger가 꺼진 상태서
충돌시 oncollisionenter 함수 실행!

Raycast

레이캐스팅 - 물체에서 레이저를 쏴서 레이저에 닿은 물체 정보를 불러오는 느낌
Physics.Raycast(transform.position,Vector3.forward);   - 플레이어 위치로부터 앞으로 레이캐스트, 불값 리턴
Debug.DrawRay(transform.position, Vector3.forward, Color.red); - 디버그로 레이를 눈에 보에게 해줌

위에걸 그대로 하면 레이저가 한칸만 나가니 positon에 
vector3.up으로 플레이어 중간에서 레이저가 나가게 맞춰줌

디버그에서 레이캐스트에 닿은 물체를 보고 싶으면
raycasthit hit; 형태로 hit을 선언하고 raycast 함수에 out hit으로 인자를 넣은 후
디버그 부분에 hit,colider.gameobject.name 형태로 이름 넣어주기

vector3.foward로 하면 월드 기준으로 앞으로만 나가니까
Vector3 look = transform.TransformDirection(Vector3.forward);
transformdirection으로 플레이어가 보는 방향에 맞게 레이캐스트 조정 가능!

 
그냥 physics.raycast는 오브젝트 여러개에 닿아도 처음 것 하나만 인식하지만 phtsics.raycastall을 사용하면 여러개를 인식 가능
RaycastHit[] hits; - 배열로 선언하고
hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10); - 닿는 모든 오브젝트를 리턴

레이캐스트 활용
레이캐스팅을 이용해서 플레이어가 벽에 가려질때 카메라 시점 문제를 해결 가능
플레이어에서 카메라 위치로 레이캐스트를 하고
중간에 걸리는 물체가 있으면 카메라를 그 물체 앞쪽으로 이동시켜서 해결, 이런식으로 다양하게 응용 가능하다


좌표계

로컬 - 특정 물체 기준
월드 - 하나의 세계 기준 좌표
local - world - vieport - screen 좌표계가 있음


screen 좌표계 

 Input.mousePosition - 마우스 위치를 좌표로 나타냄
Camera.main.ScreenToViewportPoint(Input.mousePosition - 스크린 좌표계와 유사하지만 0~1 사이 비율로 나타냄

 
camera.main - 메인 카메라 가져오기
input.mouspopsition - 마우스가 있는 위치 가져오기
if (Input.GetMouseButtonDown(0)) - 마우스 눌렷을때 작동

ray를 선언하고 raycasthit hit으로 ray에 닿을 물체를 만들고 raycast로 ray에 닿은 물체에 관해서 작동하게 만들 수 있음
강의서 특정 오브젝트나 위치를 마우스로 클릭시 반응하게 만들어주는 코드 작성

LayerMask

레이캐스팅 성능, 최적화, 레이어를 이용해서 필터? 해서 연산하기
레이어 - 32비트 연산으로 이용, 메쉬 콜라이더가 켜진 상태에서 이용 가능
레이어를 이용해서 해당 레이어에 해당하는 오브젝트만 레이캐스팅 가능
int mask = (1<<8)로 8번째 비트 켜주고
raycast 함수에 인자로 mask를 넣어주면 8번 레이어만 켜지게 할 수 있음
int mask = (1 << 8) | ( 1 << 9); 
형태로 마스크를 설정하면 8번,9번에 해당하는 마스크만 켜지게 ( 쉬프트 연산으로 8, 9에 해당하는 걸로 이동하고 or 연산으로 둘다 더해줌) 할수 있음
그냥 int mask = 768로 할 수도 있지만 가독성의 문제 때문에 위에 버전을 사용하는걸로 하자
레이캐스팅이 부하를 많이 주는 방법이기에 최적화 하는게 중요
위에 비트연산을 이용한 방법 말고도 
Layermask mask = LayerMask.GetMask("monster"); 형태로 레이어 이름을 받아서 쓰는 방법도 있음

태그 - 오브젝트 구분할때 사용
camera.main 형태로 오브젝틀를 가져올때 main도 태그의 일종
gameobject findobjectwithtag에 이용, tag도 add 가능


카메라

탑뷰, 쿼터뷰 등 카메라 위치나 카메라 액션을 해보자
카메라 - 타겟텍스쳐 옵션-  cctv같은 느낌으로 사용 가능한 기능
카메라가 플레이어를 따라가게 만들어주려면?
플레이어 회전값에 상관 없이 월드 기준으로 따라가게 하거나 방향벡터를 받아서 이동시켜주기 등등 방법으로 구현해보자

플레이어의 벡터를 카메라 스크립트에 넘겨준후 처음 카메라에 위치에 플레이어의 벡터를 더해주는 방법,
transform.lookat(플레이어 포지션)으로 로테이션을 강제로 넣어주는 방법이 있음
위에 방법으로 카메라가 플레이어를 따라오는 코드에서 업데이트에 카메라 컨트롤러 좌표 부분을 넣으면 덜덜거리는 현상이 발생 
이유는 플레이어컨트롤러도 업데이트에 있고 카메라 컨트롤러도 업데이트에 있어서 둘이 순서가 충돌? 해서 그럼
해결을 위해 카메라를 플레이어컨트롤러보다 늦게 작동하게 만들기 위해서 카메라 컨트롤러 update를 lateupdate로 바꿔줌