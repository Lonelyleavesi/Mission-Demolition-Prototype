using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode { 
    idle,
    playing,
    levelEnd
}


public class MissionDemolition : MonoBehaviour
{
    static public MissionDemolition S;

    public GameObject[] castles;  // 储存所有城堡对象数组
    public Text textLevel;
    public Text textScore;
    public Vector3 castlePos; // 放置城堡的位置

    public bool _______________________________________________;

    public int level;  // 当前级别
    public int levelMax;  // 最大级别
    public int shotsTaken;
    public GameObject castle; // 当前城堡
    public GameMode currMode = GameMode.idle;
    public string showing = "Slingshot"; // 摄像机的模式

    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        // 如果城堡已经存在 则清零原有城堡
        if (castle != null)
        {
            Destroy(castle);
        }

        // 清理原有的弹丸
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // 实例化
        castle = Instantiate(castles[0]) as GameObject;
        castle.transform.position = castlePos;
        shotsTaken = 0;

        // 重置摄像机位置
        SwitchView("Both");
        ProjectileLine.S.Clear();
        // 重置目标状态
        Goal.goalMet = false;
        ShowText();
        currMode = GameMode.playing;
    }

    void ShowText()
    {
        // 设置界面文字
        textLevel.text = "Level:" + (level + 1) + " of " + levelMax;
        textScore.text = "Shots Taken: " + shotsTaken;
    }
    // Update is called once per frame
    void Update()
    {
        ShowText();
        // 检查是否已经完成该级别
        if (currMode == GameMode.playing && Goal.goalMet)
        {
            currMode = GameMode.levelEnd;
            // 缩小画面比例
            SwitchView("Both");
            // 在两秒后开始一级别
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    private void OnGUI()
    {
        Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);
        switch (showing)
        {
            case "Slingshot":
                {
                    if (GUI.Button(buttonRect, "查看城堡"))
                    {
                        SwitchView("Castle");
                    }
                }
                break;
            case "Castle":
                {
                    if (GUI.Button(buttonRect, "查看全部"))
                    {
                        SwitchView("Both");
                    }
                }
                break;
            case "Both":
                {
                    if (GUI.Button(buttonRect, "查看弹弓"))
                    {
                        SwitchView("Slingshot");
                    }
                }
                break;
            default:
                break;
        }
    }

    static public void SwitchView(string eView)
    {
        S.showing = eView;
        switch (eView) {
            case "Slingshot":
                FollowCam.S.poi = null;
                break;
            case "Castle":
                FollowCam.S.poi = S.castle;
                break;
            case "Both":
                FollowCam.S.poi = GameObject.Find("ViewBoth");
                break;

        }
    }

    // 允许在代码任意位置增加发射次数的代码
    public static void shotFired()
    {
        S.shotsTaken++;
    }
}


