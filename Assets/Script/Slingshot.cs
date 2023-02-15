using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static public Slingshot S;

    public GameObject prefabProjectile;
    public float velocityMult = 4;
    public bool __________________________________________________;


    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private void Awake()
    {
        S = this;

        //GameObject.Find("ScoreCounter");
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.GameObject();
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    void OnMouseEnter()
    {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        // 实例化一个弹丸
        projectile = Instantiate( prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }


    private void Update()
    {
        if (!aimingMode) { return; }
        // 获取鼠标3d坐标
        Vector3 mousePos2D = Input.mousePosition; ;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // 计算launchPos 和 鼠标两点坐标差
        Vector3 mouseDelta = mousePos3D - launchPos;
        // mouseDela 限制在弹弓球体碰撞器半径范围
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        // 将 弹丸移到新位置
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0)) 
        {
            // 如果已经松开鼠标 
            aimingMode= false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            FollowCam.S.poi = projectile;
            projectile = null;
            MissionDemolition.shotFired();
        }
    }
}
