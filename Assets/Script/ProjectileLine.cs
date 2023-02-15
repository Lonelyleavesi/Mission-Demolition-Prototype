using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; // ����
    public float minDist = 0.1f;

    public bool ________________________________________;

    // �ڴ����ж�̬���õ��ֶ�
    public LineRenderer line;
    private GameObject _poi;
    public List<Vector3> points;

    private void Awake()
    {
        S = this;
        // ��ȡ������Ⱦ�� (LineRenderer)������
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        // ��ʼ����ά�������List
        points= new List<Vector3>();
    }

    public GameObject poi
    {
        get { return _poi; }    
        set { 
            _poi = value; 
            if (_poi != null)
            {
                // ����POI���ó��¶���ʱ
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        // ��ֱ�������һ����
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).sqrMagnitude < minDist * minDist)
        {
            // ����õ�����һ����ľ��벻��Զ�� �򷵻�
            return;
        }

        if (points.Count == 0)
        {
           // �����ǰ���Ƿ����
            Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
            Vector3 launchPosDiff = pt - launchPos;
            // ... �����һ������ ����֮����׼
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        } else {
            // ������ӵ�
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(line.positionCount - 1, lastPoint);
            line.enabled = true;
        }

    }

    // ���������ӵ��λ��
    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }

    private void FixedUpdate()
    {
        if (poi == null) {
            // �����Ȥ�㲻�� ���ҳ���һ��
            if (FollowCam.S.poi != null)  {
                // �Ӿ�ͷ׷��������
                if (FollowCam.S.poi.tag == "Projectile")  {
                    poi = FollowCam.S.poi;
                } else {
                    return;
                }
            } else {
                return;
            }
        }

        // �ҵ��Ժ�ʼ�ӵ�
        AddPoint();
        Projectile projectile = poi.GetComponent<Projectile>();
        if (projectile.IsStopNearly())
        {
            poi = null;
        }
    }
}
