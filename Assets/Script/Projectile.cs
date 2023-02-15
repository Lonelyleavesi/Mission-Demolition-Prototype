using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float 最小速度 = 1f;
    public bool isEnable = true;

    private int frameCount;
    private int frameCountMax;
    private bool isNearlyStop;

    public bool IsStopNearly()
    {


        GameObject 城堡 = MissionDemolition.S.castle;
        if (transform.position.x > (城堡.transform.position.x + 20))
        {
            return true;
        }

        return this.isNearlyStop;
    }

    private void Awake()
    {
        this.frameCount = 0;
        this.frameCountMax = 100;
        this.isNearlyStop = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // 停止或者速度小于阈值就重新发球
        if (GetComponent<Rigidbody>().IsSleeping() || GetComponent<Rigidbody>().velocity.sqrMagnitude < (最小速度 * 最小速度))
        {
            this.frameCount++;
            if (this.frameCount >= this.frameCountMax)
            {
                isNearlyStop = true;
                isEnable = false;
            }
        }
        else
        {
            this.frameCount = 0;
        }
    }
}
