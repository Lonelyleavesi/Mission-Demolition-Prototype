using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; // 单例
    public float minDist = 0.1f;

    public bool ________________________________________;

    // 在代码中动态设置的字段
    public LineRenderer line;
    private GameObject _poi;
    public List<Vector3> points;

    private void Awake()
    {
        S = this;
        // 获取对线渲染器 (LineRenderer)的引用
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        // 初始化三维向量点的List
        points= new List<Vector3>();
    }

    public GameObject poi
    {
        get { return _poi; }    
        set { 
            _poi = value; 
            if (_poi != null)
            {
                // 当把POI设置成新对象时
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
        // 在直线上添加一个点
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).sqrMagnitude < minDist * minDist)
        {
            // 如果该点与上一个点的距离不够远， 则返回
            return;
        }

        if (points.Count == 0)
        {
           // 如果当前点是发射点
            Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
            Vector3 launchPosDiff = pt - launchPos;
            // ... 则添加一根线条 帮助之后瞄准
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        } else {
            // 正常添加点
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(line.positionCount - 1, lastPoint);
            line.enabled = true;
        }

    }

    // 返回最近添加点的位置
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
            // 如果兴趣点不在 则找出来一个
            if (FollowCam.S.poi != null)  {
                // 从镜头追踪器里找
                if (FollowCam.S.poi.tag == "Projectile")  {
                    poi = FollowCam.S.poi;
                } else {
                    return;
                }
            } else {
                return;
            }
        }

        // 找到以后开始加点
        AddPoint();
        Projectile projectile = poi.GetComponent<Projectile>();
        if (projectile.IsStopNearly())
        {
            poi = null;
        }
    }
}
