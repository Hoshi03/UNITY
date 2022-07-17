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

0706

플레이어가 마우스 클릭을 따라서 이동하게 만들어보자
레이캐스트를 플레이어 컨트롤러에 넣고 
인풋매니저를 수정해서 키 + 마우스 둘다 받게 만들어주기
레이캐스트로 이동할때 이동할 거리가 현재 속도로 이동하는 것보다 작으면 플레이어가 발작하는 현상이 발생
해결을 위해서는 이동거리가 (목적지-플레이어 위치) 보다 항상 작게 만들어줘야됨
해결법은 이동거리가 (목적지-플레이어 위치)보다 크면 이동거리를 목적지-플레이어 위치로 바꿔버리거나
math.clamp로 이동거리를 dir 최대값을 넘어가지 않게 만들어줌

플레이어가 벽에 가려지지 않게 하는 것을 구현해보자
플레이어 에서 카메라 방향으로 레이를 쏴서
중간에 걸리는게 있으면 카메라를 플레이어 쪽으로 당겨오는 것을 구현
레이캐스트로 중간에 Wall에 hit히면 
벽에 닿은 부분에서 플레이어의 위치를 빼서 방향을 구해주고 방향벡터를 약간 줄여서 카메라 초기 위치와 플레이어 사이의 방향에 곱한 곳에 카메라를 위치하게 해줌

0707

Animation

애니메이션 컨트롤러에서 애니메이션 수정 가능
코드상에선 getcomponent로 animator를 가져오고
가져온 animator를 조건부로 켜서 애니메이션 전환

Animation Blending

애니메이션도 lerp 쓴느 것처럼 자연스럽게 움직이는
블렌딩을 이용해서 자연스럽게 전환, 멈추기 가능
애니메이터에서 블렌드 트리 만들기로 들어가기
파라미터에서 애니메이션 섞는 비율을 만들 수 있음
만들어둔 파라미터는 코드상에서 mathf.lerf로 자연스럽게 이동하는 값을 setfloat 해줘서 넘겨줌

State 패턴

현재 개선할점. 
애니메이션이 2개 ( run / wait) 밖에 없기에 
하드코딩으로도 가능하지만 애니메이션이 많아지면 
하드코딩으론 현실적으로 불가능해짐
bool값으로 점프, 이동, 달리기, 캐스팅 등등 각각 동작을 하나씩 넣어주는식으로 노가다를 하면 관리 불가능
그래서 state(상태) 패턴으로 playerstate를 정의하고
switch로 분기를 만들어줌, enum으로 state를 만들어주고 
플레이어가 조건문으로 플레이어가 달릴때 특정 state로 바뀌주는 식으로 state를 바꿔주고 
그 state에 해당하는 함수를 만들어주고
update 문에서 state를 이용한 분기문+ 그 스테이트에 해당하는 함수 호출을 해주는 방식으로 사용!
지금 상태는 한번에 한가지 동작만 가능.. 추후 여러가지가 가능하게 개선해주자

0713

애니메이터에서 state 바꿔주기를 하는게 관리가 편하고 보기도 좋음
기존엔 블렌드 트리를 이용하고 실수값으로 애니메이션을 관리했지만 이번에는 툴에서 애니메이션을 이어서 관리
애니메이션은 transition 순서대로 가고 hasexittime이 켜져 있을때 이동

엔진 상에서 트렌지션을 변화시키는 방법
애니미이터-스테이트-트렌지션(화살표)에 컨디션에 만들어둔 파라미터를 넣고 파라미터 변수가 크거나 작은 상태에서 조건부로 애니메이션을 변경
이동과 관련된 부분은 스테이트 머신으로 만들고스킬같은 복잡한 부분은 블렌딩처럼 따로 관리하는 경우가 많음

KeyFrame Animation

게임 상의 플레이어 말고 다른 물체에 애니메이션 지정해두기 - 돌겜 카드 나오는 애니메이션 같은것
윈도우 - 애니메이션으로 들어가서 animation 창 들어가기 해당하는 오브젝트 클릭후 위부터 시간, 이벤트, 키 순서대로 한줄씩 있음
좌표 클릭 형식으로 동작을 하나씩 지정해주거나 녹화 버튼을 누르고 물체를 수정해서 애니메이션 만들 수 있음


Animation Event

애니메이션 특정 구간에 이벤트 추가를 하고 애니메이션을 실행할 오브젝트내 함수를 이벤트로 불러올 수 있음
공격 애니메이션에서 딱 닿는 시점에 이벤트를 해서 그때 콜리더가 닿아있으면 데미지가 들어가게 한다든지 다양한 이벤트 구현하는데 사용할 수 있음
미리 만들어둔 애니메이션에도 특정 프레임에 이벤트 추가해서 사용 가능!



UI (유아이)

ui - ui 오브젝트로 할 수 도 있지만 ui 메뉴가 따로 잇음, ui 원근법 적용 x

Rect Transform

ui에서 앵커는 rect transform 상에서 사용 가능하고 사용시 패널과 앵커 핀 거리는 비율로, 앵커 핀과 버튼같은 ui와의 거리는 절대좌표로 계산함,
앵커를 쓰는 이유는 해상도가 다른 화면마다 비슷한 위치에 버튼같은 ui를 위치하게 하기 위해서?
앵커가 중앙에 있으면 아무리 해상도가 변해도 버튼 크기는 고정된 채로 이동함 앵커를 버튼과 매우 가깝게 두면 비율을 그대로 유지함

Button Event

버튼 넣은 캔버스를 프리팹으로 만들어서 사용하는 방법도 ㄱㅊ음
캔버스에 버튼에서 호출할 함수를 넣은 스크립트를 붙이고 캔버스 안의 버튼 온클릭에 캔버스를 넣는 구조로 작성
ui를 클릭중인것을 알아내기 위해 EventSystem.current.IsPointerOverGameObject()로
ui가 클릭됬으면 인풋 매니저에서 바로 리턴을 하게 만들어줌 - 플레이어가 버튼 누를때 이동하는 경우를 막아줌, 상당히 자주 쓰게될듯 외워놓자!
기존에는 ui를 다 만들어서 실행할때 꺼두고
특정 이벤트 발생시 ui를 꺼내주는 방식으로 코딩했는데 리소스 매니저를 사용해서 해주는 방법도 ㄱㅊ은 것 같다, 매니저와 관련 문법을 다시 공부해서 이해하고 잘 써먹어보자!
ui 팝업을 중첩시킬때 캔버스에서 sort order 순으로 나옴
ui를 온클릭으로 하나하나 넣어주면서 관리하기에
프로젝트 규모가 작지 않으면 관리가 불가능할 정도로 힘들어짐, 툴에서 할 수 있는것은 코드상에서 할수 있음, 매니저로 관리해보자



UI 자동화, 솔직히 이해 잘 못함, 문법 공부하고 다시 봐야됨

온클릭 드래그&드롭으로 넣는것과 text 하나씩 툴로 연결 하는 것을 개선하기
하나하나 넣는 것을 이름만 넣으면 알아서 찾아서 연결하는 방법으로 개선하기

enum 으로 text랑 버튼들을 묶어넣고bind 함수를 만들어서 enum안에 있는 것과 이름이 같으면 그걸 조작하는 함수를 만들어보자
enum을 넘겨주는 방법은?
리플렉션을 이용해서 정보를 넘겨주기
리플렉션을 사용할땐 type을 넣고 bind에 typeof로 enum 타입을 넘김
bind에 제네릭으로 타입을 넣어줘서 특정 타입에 해당하는 것만 넘겨줄 수 있게 해줌

bind 함수 작동 순서
1. enum에 있는 것들 이름을 가져오는 str 배열 만들고
2. str 배열 길이만큼 유니티엔진 오브젝트를 넣을 배열을 만들고 
3. 키에 타입, 밸류에 오브젝트를 가지는 딕셔너리에 타입과 2에서 만든 오브젝트 배열을 넣고
4. for문 으로 순차적으로 오브젝트 배열을 돌기 

util - findchild 함수 
게임오브젝트, 이름, 반복여부를 받아서
foreach문으로 해당 타입 게임오브젝트의 자식을 찾아준다

0717

get 함수를 이용해서 텍스트를 코드상에서 바꾸기 편하게 해둠, 매핑 자동화
이름 유의해서 enum에 추가하고 Get<Text>((int)Texts.ScoreText).text = "bind test"; 형식으로 변경 가능
이벤트 핸들러를 넣어줘서 드래그 인식 가능,
이벤트핸들러 스크립트에 Action <PointerEventData> 으로 액션을 만들어주고 UI_Btn 스크립트 스타트 부분에 image 타입 게임 오브젝트를 가지고 와서 
이벤트 핸들러를 추출하고 가져온 게임 오브젝트가 드래그 되면 이벤트 핸들러를 추출한 오브젝트를 드래그 한 곳으로 이동시키는 함수를 람다식으로 만들어서 구독하게 함
UI의 recttransform은 transform을 상속받앗기에 transform으로 recttransform 이동하는게 가능

새로운 문법 - 익스텐션 메서드

 

게임오브젝트에 없는 함수를 익스텐션으로 추가해서 gameobject.익스텐션으로 사용 가능하게 만들어보자
익스텐션을 만들때는 static 클래스로 만들고 모노비헤이어 빼주기 
만들 함수를 넣고 gameobject. 으로 사용하고 싶으면 gameobject 앞에 this를 붙여서 사용한다

UI manager

uimanager에서 할 것 - 팝업 순서 받아서 관리하기
가장 마지막에 띄운 팝업을 가장 먼저 삭제할 것= 스택으로 만들기
스택에 넣을 내용은 자체만으로는 깡통인 게임오브젝트 말고 게임오브젝트가 가지고 있는 컴포넌트(스크립트도 포함)인 ui_btn 스크립트에 상속 된 ui_popup 컴포넌트를 넣자
UImanager - ui팝업 함수
showpopup에 들어가는 string은 프리팹 이름
일단 string.IsNullOrEmpty()로 이름을 안받았는지 체크하고 안받았으면 name = typeof(T).Name;으로
이름에 T 이름을 넣어줌
리소스 매니저에서 경로 넣어서 게임오브젝트로 불러오기
util 스크립트에서 컴포넌트 가져오는 getoraddcomponent로 ui_pop가져오기
UImanager - ui닫는 함수
처음에 스택 카운트 체크하고 암것도 없음 리턴 
있으면 스택을 pop 하고  pop해서 가져온 게임 오브젝트 삭제, 혹시 모르니 popup을 널로 만들기
피크 - 맨 위에 것을 반환하고 삭제는 안함, 체크할 때 사용
-------------- ui관련 정리하는것은 복습 꼭 해야할듯--------------

인벤토리 만들기 

2d 스프라이트를 패키지 매니저에서 넣어주고
스프라이트를 패널에 넣어줌, 스프라이트는 해당
스프라이트를 클릭하면 나오는 스프라이트 에디터에서 수정 가능
패널에 이미지를 넣을때 rect transform에서 
자식 이미지를 클릭하고 alt + shift 한 뒤 우하단 맨 밑 버튼을 눌러주면 부모 사이즈에 맞게 딱 들어가게 할 수 있음
컴포넌트를 추가할때 레이아웃을 설정하면 자동으로 정렬 하는 느낌으로 만들 수 있음 
패널에 자식으로 icon을 복붙으로 넣어두고 레이아웃을 그리드로 하면 정렬된 느낌을 줌 