# Unity 심화 프로젝트

<h1>🐟 Find a fish 🐟</h1>

## 🕵️ 프로젝트 소개


고양이에게 생선을 가져다 주세요! 참고로 생선은 무시무시한 던전 안에 있답니다!

![스크린샷 2025-06-18 오후 10 10 29](https://github.com/user-attachments/assets/0aeb5f29-445b-447c-b414-803d81e8f0e3)

📱 슈팅 액션 로그라이크

## 🚩게임 타이틀
![start](https://github.com/user-attachments/assets/fb09407f-36f1-4f98-9eff-c853c6c6bbd4)

플레이어가 마을에서 만난 배고픈 고양이에게 먹을 것을 가져다주기 위해 여정을 떠납니다.



### 🕹조작키
WASD, ↑←↓→ 이동

E 줍기

F 대화

Tab 인벤토리 고정

마우스 좌클릭 공격

## 🎞진행 과정 & 주요 기능

### 🏘마을 - 대화 시스템
플레이어는 평화로운 마을에서 게임을 시작합니다.

NPC(고양이)와의 대화로 이 게임의 목표를 알 수 있습니다.

대화 시스템은 두트윈(DOTween) 애니메이션으로 매끄럽게 진행됩니다.

### 🗺던전 탐험 - 맵 구조
![trap](https://github.com/user-attachments/assets/52f74863-688a-433f-8510-600a4ca0df06)

![Box](https://github.com/user-attachments/assets/94b61e84-e749-436c-a9ee-e0fa9091f105)


던전은 타일맵 기반으로 제작되어, 다양한 지형과 전투구역이 존재합니다.

각 방에는 전투 트리거가 있어, 플레이어가 진입 시 몬스터가 등장하고 일정 조건 달성 시 통로가 열립니다.

### 👛무기 - 인벤토리 시스템
![image](https://github.com/user-attachments/assets/04d9f916-d147-49ef-ab78-001f0afdfd0f)

플레이어 무기는 총으로, 마우스 커서를 따라 360도 자유롭게 회전합니다.

인벤토리 UI는 두트윈(DOTween) 애니메이션으로 자연스럽게 나타나고 사라지며, 슬롯에 아이템이 실시간 반영됩니다.

※단축키(1~6번)**로 아이템 사용이 가능합니다.
아이템은 체력 회복, 이동속도 증가 물약 등이 있습니다.

### ⚔전투 시스템
![Stage](https://github.com/user-attachments/assets/9b197815-600d-4449-8630-f598010e7b04)

플레이어가 마우스 클릭 시 총을 발사, 탄환이 생성되어 몬스터를 타격합니다.

몬스터 피격 시 체력바, 데미지 텍스트 UI가 활성화되어 전투 상황을 명확히 보여줍니다.

*스태미너 : 플레이어가 이동하면 소모되고, 멈추면 회복됩니다. 스태미너가 0이면 이동속도가 느려집니다.

### ☠몬스터 - 보스 구조
![enemy](https://github.com/user-attachments/assets/c688aba1-5297-4ecd-9257-da582583dcd4)

![boss2](https://github.com/user-attachments/assets/980ccbaa-8f2a-4009-a83f-b85315fdc4b4)


여러 종류의 몬스터 : 근거리, 원거리, 방어 특화 등 다양한 타입이 존재합니다.

몬스터는 StateMachine을 통해 기본, 탐지, 추격, 공격 등 다양한 상태로 전환되며, 몬스터별로 공격 패턴이 다르게 동작합니다.

보스 몬스터 : 상태머신 대신 전략패턴(Strategy Pattern)으로 페이즈별 공격 패턴을 구현하여, 더 복잡한 전투를 제공합니다.

### 💬엔딩 스토리
![fish](https://github.com/user-attachments/assets/4ff7b5c1-a9a3-4d8d-bb55-fc817024f9ad)

보스 몬스터 처치 후 생선 아이템을 획득합니다.

마을로 돌아와 고양이에게 생선을 건네면, 엔딩 씬으로 전환되어 게임이 마무리됩니다.

### ⚙기타 UI 및 시스템
![setting](https://github.com/user-attachments/assets/ce0e3963-f025-4674-b6e3-1fde27b410bb)

![Intro](https://github.com/user-attachments/assets/2fa189d6-fd77-4474-bf71-14664acacc6b)

일시정지, 설정 메뉴: 언제든 게임을 멈추거나 타이틀 화면으로 돌아갈 수 있습니다.

Intro 씬: 게임 시작 전 스토리 텍스트와 일러스트, 자연스러운 씬 전환이 구현되어 있습니다.

## 제작 팀
| 팀원|역할|
|-------------|------|
| 조성무 | 맵 및 전체적인 게임 흐름 |
| 김영훈 | 플레이어 |
| 최은송 | 몬스터 |
| 한예림 | UI |
| 정재훈 | 인트로 씬 |
