public enum EmClickState
{
    Ok,
    Cancel,
}

public enum EmCanvasLayer
{
    Layer1,
    Layer2,
    Overlay
}

public enum EmPhoneState
{
    Inventory = 1,                                     // 인벤토리.
    ZombieGuide,                               // 도감.
    Talk,                                           // 일단 이거 대기. 
    None = 0                                         //  현재 상태.
}

public enum EmWarningType
{
    NicKaNemeError,               // 닉네임 없음.
    LackMoney,                  // 돈이 부족함.
    Inventory,                     // 인벤토리 공간 부족.
    SceneChange,                  // 씬이동할 때 사용하는 것.
    PurchaseComplete,            // 구매완료.
    ApplicationQuit,                // 어플리케이션 종료.
    LackMaterial                    // 재료 부족.
}


// 이것은 OSA의 상태를 중복을 판단을 알려주기 위한.
// 상태이다.
// OSA의 버그임 판별하기 위해 상태값을 두어야한다.
public enum EmCellState
{
    Complete,
    NotComplete
}

// 이거 확인 후 지우기.
// 인벤 슬롯칸 타입.
public enum EmInvenType
{
    Medical = 1,                                        // 빨간 물약.
    Weapon,                                              // 무기.
    Bullet,                              // 무기 부속품(총알, 화살, 폭탄).
    Armor = 4,
    Empty = 0                              // 1~15개는 None으로 처리.
}

public enum EmItemType
{
    Medical = 1,
    Weapon,
    Bullet,
    Armor = 4,
    Ingredient,
    Empty = 0
}

// CUIMainPlayer 사용할 곳.
public enum EmSkillNum
{
    First,
    Second
}

// 스킬 타입.
// 시민 -> 적 찾기.
// 의료인 -> 아이템 찾기.
// 군인 -> 방패.
public enum EmSkillType
{
    FindtheEnemy = 1,
    FindItem,
    Shield,
    Run = 0
}

public enum EmActionType
{
    Go = 1,
    Complete,
    Victory,
    GameOver = 4,
    LevelUp,
    Recovery,
    None = 0
}


// 아이템을 클릭했을 떄 나타나는 정보창.
public enum EmInfoType
{
    Inventory = 1,                          // 인벤토리.
    ItemBox,                           // 아이템 박스.
    Shop,                              // 가게.
    Skill                                    // 스킬 정보.
}

// 플레이어가 가지고 있는 스탯.
public enum EmStatsType
{
    Speed,
    Attack,
    Defense,
    Recovery,
    CriticalHit
}

public enum EmProfessional
{
    Orignal = 1,                       // 일반인(스피드 : 3, 공격력 : 3, 크리티컬력 : 3, 방어력 : 3, 회복력 : 3).
    Medical,                      // 의사(스피드 :3 , 공격력 :2, 크리티컬력 : 3, 방어력 : 2, 회복력 : 5).
    Soldier,                       // 군인(스피드 : 4, 공격력 : 5, 크리티컬력:2, 방어력 : 4, 회복력:1).
    None = 0
}


public enum EmNpcType
{
    Store = 1,
    Reward,
    Quest,
    None = 0
}

// 좀비 타입.
public enum EmZombieType
{
    Easy,                         // 기어다니기, 물기.
    Normal,                     // 동작이 보통 (기본동작 : 걷기, 멈추기,물기,  공격1).
    Bad,                         // 동작이 나쁨-> (동작 : 걷기, 뛰기, 멈추기, 물기, 공격1).   
    Worst                       // 동작이 최악 -> (동작 : 걷기, 뛰기, 멈추기, 물기, 공격1, 토함). 
}


// Object를 관리하는 Type(계속 추가할 것).
public enum EmObjectPoolType
{
    ItemBox,
    Obstacle,
    Fog,
    Bullet,
    Arrow,
    Bomb,
    None =0
}

public enum EmCameraType
{
    Openning,                       // 시작화면.
    Bunker,                           // 벙커씬으로 넘어왔을  떄 -> 로보토미 코퍼레이션 처럼 이용하기..
    FirstPerson,                      // 1인칭.
    ThirdPerson                     //  3인칭.
}


public enum EmDialogType
{
    Me,
    Hadi,
    UJin,
    Invitation,
    Dialog,
    Button
}

public enum EmWeapon
{
    Sword = 1,
    Bat,
    Hybrid,
    Rifle = 4,
    Crossbow,
    Shield,
    None = 0
}

public enum EmMouseState
{
    Attack = 1,
    UI,                                 // UI일 시 카메라 움직임 비활성화.
    None = 0,
}

public enum EmDBType
{
    Quest,
    Level
}

// CplayerAnimation에서 사용할 타입.
public enum EmAniState
{
    WeaponChange=1,
    GetOffWeapon,
    Attack,
    Injured,
    Boost,
    GetHit,
    Death,
    Idle = 0
}