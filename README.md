# 🔥 EXIT CODE : RED

**학교 건물을 1:1로 옮겨 온 VR 화재 대피 시뮬레이션**

동양미래대학교 3호관을 Blender로 똑같이 모델링한 디지털 트윈 공간에서, Meta Quest 3를 쓰고
불이 난 학교를 직접 빠져나오는 체험형 안전 교육 콘텐츠예요.

> 2025 시스템분석설계(종합설계) · 팀 **HOT ESC** (4조) · 2025.12

![Unity](https://img.shields.io/badge/Unity_6-222222?style=flat-square&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Blender](https://img.shields.io/badge/Blender_4.4-F5792A?style=flat-square&logo=blender&logoColor=white)
![Meta Quest 3](https://img.shields.io/badge/Meta_Quest_3-0467DF?style=flat-square&logo=meta&logoColor=white)
![XR Interaction Toolkit](https://img.shields.io/badge/XR_Interaction_Toolkit-000000?style=flat-square&logo=unity&logoColor=white)

## 💡 왜 만들었나요?

학교 화재 안전 교육은 대부분 이론 강의나 영상 시청으로 끝나요. 그래서 실제로 불이 났을 때 필요한
**즉각적인 판단력**과 **공간 감각**을 기르기 어려워요.
EXIT CODE : RED는 매일 다니는 학교 건물이 재난 현장으로 바뀌는 상황을 안전한 가상 공간에서 체험하고,
몸으로 대피 경로를 익히도록 만들었어요.

## 🎮 두 가지 모드

### 📘 교육 모드 (Training Mode)
- **3D 바닥 유도선**으로 가장 가까운 비상구까지 가는 길을 안내해요.
- **문 열기 전 손잡이 온도 확인**으로 백드래프트 위험을 미리 알아채는 습관을 길러요.
- **소화기 사용 3단계를 직접 해 봐요.** 안전핀 뽑기(그립) → 노즐 조준 → 손잡이 누르기(트리거)
- 유도등은 실제 건물과 같은 위치에 있어요.

### 🔥 실전 모드 (Challenge Mode)
- 유도선, 조작 팁 같은 **안내가 모두 꺼져요.** 기억과 유도등만 보고 탈출해야 해요.
- **제한 시간**(골든타임)과 시야를 가리는 **짙은 연기**가 있어요.
- **HP 시스템:** 연기와 화염에 닿으면 체력이 줄고, 불에 직접 닿으면 크게 줄어요.
- **백드래프트:** 온도를 확인하지 않고 문을 열면 화염이 터져 나와요. 달궈진 손잡이는 무작위로 생겨요.
- **꺼지지 않는 큰 불:** 통로를 막은 큰 불은 소화기로 잠깐 줄어들었다가 다시 커져요.
  "초기 진압이 안 되는 불은 무리하지 말고 우회한다"는 수칙을 체험하게 돼요.
- 탈출 후에는 **남은 HP**로 대응 숙련도를 평가해요.

## 🧱 시스템 구조

| 계층 | 구성 |
|---|---|
| 사용자 입력 | Input Action Manager (트리거·그립·조이스틱), 텔레포트·스냅 턴 이동 |
| 코어 로직 | Interaction Manager (문·소화기), Game Manager (모드 전환·제한 시간·성공/실패 판정) |
| 환경 반응 | Fire & Smoke Controller (불·연기 확산과 진압), Status System (HP 계산) |
| 렌더링 | 최적화한 메시·텍스처, 라이트맵, World Space UI |

## 🛠 개발 환경

| 구분 | 내용 |
|---|---|
| 엔진 | Unity 6000.2.6f1 (URP) |
| 3D 모델링 | Blender 4.4 (학교 도면 기반 1:1 스케일) |
| 스크립트 | C#, Visual Studio 2022 |
| VR | Meta Quest 3 (Android Standalone), Unity XR Interaction Toolkit |
| 이펙트·사운드 | Unity Particle System, Meta XR Audio (공간 음향) |
| 협업 | GitHub, Notion |

## 👥 팀 HOT ESC

| 역할 | 이름 | 담당 |
|---|---|---|
| 팀장 | 이유림 ([@urimll](https://github.com/urimll)) | 게임 전체 로직(교육/실전 모드 제어, HP 시스템, 백드래프트 제어, 랜덤 달궈진 손잡이), XR Grab·Ray 인터랙션, 내비게이션 영역 제한, UI/UX(비상구 방향·체력 게이지·안내 메시지), 2층 복도·출입문·조명 모델링, 라이트맵, 일정 관리와 문서화 |
| 팀원 | 김찬영 | VR 컨트롤러 입력 처리, 문 개폐 상호작용, 백드래프트 구현, 중앙홀·계단 모델링 |
| 팀원 | 김민석 | 1층 복도·소화기·비상등 모델링, 화염·연기 파티클, 연기 노출에 따른 시야 흐림·체력 감소, 파티클 최적화 |

## 🚀 앞으로 해 보고 싶은 것
- 기류와 가연물 배치에 따라 화염과 연기가 퍼지는 AI 기반 화재 시뮬레이션
- 시선·이동 경로·반응 속도를 기록해서 개인별 취약점을 알려주는 평가 시스템
- 옥상·특수 교실, 야간 화재 같은 시나리오 확장
- 휠체어 모드 같은 배리어프리 훈련

## 📂 저장소 구성

| 폴더 | 내용 |
|---|---|
| `Assets/Scripts` | `Player`(모드·HP·소화기·손잡이), `QuestFire`, `DoorInteraction`, `StartManager`, `CamCtrl` |
| `Assets/Scenes` | `StartScene`(모드 선택), `GameScene`(본 게임) |
| `Assets/Models`, `Assets/school2.fbx` | 학교 건물·문 3D 모델 |
| `Assets/Textures`, `Assets/Materials` | 건물 텍스처와 머티리얼 |

## ⚙️ 실행 방법

1. **Unity 6000.2.6f1**로 이 폴더를 열어요. 패키지(`Packages/manifest.json`)는 자동으로 설치돼요.
2. 아래 빠진 에셋을 프로젝트에 다시 넣어요.
3. `Assets/Scenes/StartScene`을 열고, Meta Quest 3를 연결해서 실행하거나 Android로 빌드해요.

### 저장소에 넣지 않은 것

| 항목 | 이유 | 다시 넣는 방법 |
|---|---|---|
| `Assets/Models/school2.fbx` (304MB), `school2.fbm/` | GitHub 파일 크기 한도(100MB) 초과, 텍스처 중복 | 팀 공유 드라이브에서 받기 |
| AllSkyFree, Ian's Fire Pack Universal, CurvedUI, VR_Bire_Bottle, JsonDotNet | 에셋스토어 에셋(재배포 불가) | Unity Asset Store에서 임포트 |
| Pretendard 폰트 | 용량 | [Pretendard](https://github.com/orioncactus/pretendard)에서 받아 `Assets/Font`에 넣기 |
| TextMesh Pro, XR 샘플, VR Template 에셋 | Unity 패키지로 다시 받을 수 있음 | Package Manager에서 임포트 |
