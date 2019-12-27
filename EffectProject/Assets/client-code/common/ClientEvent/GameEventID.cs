using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEventID
{
    InitEvent = 100000,
    LoginEvent = 100001,
    LogoutEvent = 100002,

    TryStartReconnect = 100010,
    StopConnectAndGoLogin = 100011,

    E_Editor_LoadEnd = 1000,
    E_Editor_SetGravitySide = 1001,
    E_Editor_RefushData = 1002,
    E_Editor_ClearBrush = 1003,
    E_Editor_EndWeightSetting = 1004,
    E_Editor_SaveEnd = 1005,
    E_Editor_InitFalling = 1006,
    E_Editor_WarringSave = 1007,
    E_Editor_SaveAndRun = 1008,
    E_Editor_SaveAndUpload = 1009,
    E_EditorUpLoad = 1010,
    E_Editor_DealNewCell = 1011,
    E_Editor_UpdateTarget = 1012,
    E_Editor_RunLevel = 1013,
    E_Editor_RefreshFiles = 1014,
    E_Editor_UpdateTarget_Temp = 1015,
    E_Editor_DelTutorialStep = 1016,
    E_Editor_DelTutorialKiteFly = 1017,
    E_Editor_DelTutorialCell = 1018,
    E_Editor_EditorStateChanged = 1019,
    E_Editor_DelTutorialArrow = 1020,
    E_Editor_SelectFolder = 1021,
    E_Editor_RefreshTutorialStep = 1022,
    E_Editor_ShowTip = 1023,
    E_Editor_ShowUI = 1024,
    E_Editor_LogOut = 1025,
    E_Editor_RefEnemyFormation = 1026,

    E_Game_AddStep_Select = 3000,
    E_Game_ChangeBackGround = 3001,
    E_Game_ShowCamera = 3002,
    E_Game_TypewriterEffect = 3003,
    E_Game_TypewriterEffect_Finish = 3004,
    E_Game_LevelStart = 3010,
    E_Game_LevelFinishCameraPreview = 3011,
    E_Game_LevelUseStep = 3012,
    E_Game_Destroy_Updata = 3013,
    E_Game_InitChessBoard = 3014,
    E_Game_ShowProcess = 3015,
    E_Game_LeaveCityScene = 3016,
    E_Game_UIShowClose = 3017,
    E_Game_BackToLogin = 3018,


    E_Milestone_Add = 4501,
    E_Milestone_Finish = 4502,
    E_Milestone_Reward = 4503,
    E_Milestone_Next = 4504,

    E_Language_Change = 5001,

    Event_LanuageRefresh = 6001,
    Event_ChangeItemNum = 6002,
    Event_RefreshCoin = 6003,
    Event_RefreshStar = 6004,
    Event_RefreshHeart = 6005,
    Event_AnimaFinish = 6006,
    Event_ExitGame = 6007,
    Event_RefreshHummer = 6008,
    Event_LevelNameRefresh = 6009,
    Event_BeginGame = 6010,
    Event_ResetGame = 6011,
    Event_SetUICamera = 6012,

    Event_ShowMaskUI = 7007,//是否显示maskUI,如果为true就显示,否则隐藏
    Event_Refresh_Hero_HP = 7020,//刷新英雄血量
    Event_Refresh_Hero_MP = 7021,//刷新英雄怒气
    Event_ShowDungeonMap = 7022, // 通知小地图情况
    Event_Refresh_SettlementData = 7023, //刷新战斗结算获得奖励
    Event_ShowWorldMap = 7024, // 通知小地图情况


    /// PlotEventID
    //触发剧情
    Event_Plot_LevelBegin_Start = 8001,
    Event_Plot_LevelEnd_Start = 8002,
    Event_Plot_EnemyAppear_Start = 8003,
    Event_Plot_EnemyDeath_Start = 8004,
    Event_Plot_EnemyHP_Start = 8005,
    //触发剧情回调
    Event_Plot_LevelBegin = 8006,
    Event_Plot_LevelEnd = 8007,
    Event_Plot_EnemyAppear = 8008,
    Event_Plot_EnemyDeath = 8009,
    Event_Plot_EnemyHP = 8010,



    Event_Plot_Appear = 8011,
    Event_Plot_DisAppear = 8012,
    Event_Plot_EndPlot = 8013,
    Event_Plot_End_Notice = 8014, //通知剧情结束，为Condition系统加入
    Event_Plot_Begin_Notice = 8015, // 通知剧情开始，为Condition系统加入


    //棋子飞行结束之后
    Event_Plot_LevelRealStart = 8020,
    //战斗
    E_Battle_CastSkill = 8100,
    E_Battle_TeamChange = 8101,
    E_Battle_CastSkillEnd = 8102,
    E_Battle_CallDragon = 8104,
    E_Battle_ChipDestroy = 8105,//单个棋子销毁的事件
    E_Battle_ChipDestroy_Add=8106,//单个棋子销毁增加num的事件

    //棋盘
    E_StartCellMove = 8200,
    E_StartBonus = 8201,
    E_ChessboardStable = 8202,

    //主城 
    E_City_SelectedBuildingChange = 9101,   //通知所选择建筑位置产生了变化
    E_City_BuildingDataChange = 9102,       //收到服务器建筑数据变化
    E_City_BuildingDataNeedSync = 9103,    //本地认为建筑完成，通知数据管理进行验证
    E_City_BuildingBubbleClick = 9104,    //点击气泡资源收集
    E_City_UnlockedAreaChange = 9105,    //建筑区域解锁变化
    E_City_ClickToUnlockArea = 9106,       //点击区域尝试解锁
    E_City_ResCollected = 9107,       //资源已收取
    E_City_CityCameraOnPressed = 9108,       //主城摄像机是否正在移动
    E_City_BuildingBuildComplete = 9109,       //建筑建造完成
    E_City_HideCityScene = 9110,                //隐藏主城场景
    E_City_ShowCityScene = 9111,                //恢复主城场景
    E_City_BuildingUncheckNewFlag = 9112,                //取消建筑“新”的标志
    E_City_ReceivedBuildingDataSync = 9113,       //建筑信息更新之前的通知
    E_City_CameraLookAtOver = 9114,       //看向建筑摄像机移动完成
    E_City_CameraLookAt = 9115,       //主城摄像机是否正在看向并锁定


    //HeroSystem


    //玩家
    E_Player_CurrencyChange = 9301,         //玩家货币变动
    E_Player_DisplayCurrencyChange = 9302,  //玩家显示的货币变动
    E_Player_TitleChange = 9303,            //玩家爵位变动
    E_Player_PrologueFinishe = 9304,        //序章完成
    E_Player_NameChanged = 9305,     //名字变更

      //任务相关
    E_Task_TaskChange = 9401, // 任务改变
    E_Task_ConditionChange = 9402, // 条件改变
    E_Task_GuideBtnClick = 9403, // 引导按键点击
    E_Task_GuideTaskChange = 9404, // 引导任务变更
    E_Task_Reward = 9405, // 任务奖励获得
    E_Task_TaskFinish = 9406, // 任务改变
    E_Task_GuideImageGrayChange = 9407, // 图片变灰
    E_Task_GuideObjActiveChange = 9408, // 控件激活
    E_Task_GuideBackgroudClick = 9409, // 引导按钮点击
    E_Task_BattleTargetSelected = 9410, // 怪物选中

    //资源相关
    E_Res_ResourceCollectAnimFinish = 9501, //资源收取动画完成

    E_Push_PushEntityBegin = 9601, // Push队列开始
    E_Push_PushEntityEnd = 9602, // Push队列结束

    //酒馆抽卡
    Event_ShowSummonDate = 10001, // 通知抽卡主界面
    Event_ShowSummonSpine = 10002, // 抽卡结束Spine展示
    Event_ShowSummonCard = 10003, // 抽卡结束卡牌展示

    //道具相关
    Event_UseItemRefresh = 11001, // 使用道具成功

    //通用弹窗相关
    Event_ReviveHeroRefresh = 12001, //战斗失败复活英雄
    //龙穴
    Event_Refresh_DragonMainUI = 13000,//刷新龙穴UI
    Event_Drag_TourDragon = 13001,//游历任务拖拽龙区域
    Event_Show_Dragon_City = 13002,//主城显示龙模型
}