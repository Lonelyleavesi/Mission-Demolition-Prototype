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

    public GameObject[] castles;  // �������гǱ���������
    public Text textLevel;
    public Text textScore;
    public Vector3 castlePos; // ���óǱ���λ��

    public bool _______________________________________________;

    public int level;  // ��ǰ����
    public int levelMax;  // ��󼶱�
    public int shotsTaken;
    public GameObject castle; // ��ǰ�Ǳ�
    public GameMode currMode = GameMode.idle;
    public string showing = "Slingshot"; // �������ģʽ

    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        // ����Ǳ��Ѿ����� ������ԭ�гǱ�
        if (castle != null)
        {
            Destroy(castle);
        }

        // ����ԭ�еĵ���
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // ʵ����
        castle = Instantiate(castles[0]) as GameObject;
        castle.transform.position = castlePos;
        shotsTaken = 0;

        // ���������λ��
        SwitchView("Both");
        ProjectileLine.S.Clear();
        // ����Ŀ��״̬
        Goal.goalMet = false;
        ShowText();
        currMode = GameMode.playing;
    }

    void ShowText()
    {
        // ���ý�������
        textLevel.text = "Level:" + (level + 1) + " of " + levelMax;
        textScore.text = "Shots Taken: " + shotsTaken;
    }
    // Update is called once per frame
    void Update()
    {
        ShowText();
        // ����Ƿ��Ѿ���ɸü���
        if (currMode == GameMode.playing && Goal.goalMet)
        {
            currMode = GameMode.levelEnd;
            // ��С�������
            SwitchView("Both");
            // �������ʼһ����
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
                    if (GUI.Button(buttonRect, "�鿴�Ǳ�"))
                    {
                        SwitchView("Castle");
                    }
                }
                break;
            case "Castle":
                {
                    if (GUI.Button(buttonRect, "�鿴ȫ��"))
                    {
                        SwitchView("Both");
                    }
                }
                break;
            case "Both":
                {
                    if (GUI.Button(buttonRect, "�鿴����"))
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

    // �����ڴ�������λ�����ӷ�������Ĵ���
    public static void shotFired()
    {
        S.shotsTaken++;
    }
}


