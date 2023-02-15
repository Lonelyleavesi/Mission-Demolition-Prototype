using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float ��С�ٶ� = 1f;
    public bool isEnable = true;

    private int frameCount;
    private int frameCountMax;
    private bool isNearlyStop;

    public bool IsStopNearly()
    {


        GameObject �Ǳ� = MissionDemolition.S.castle;
        if (transform.position.x > (�Ǳ�.transform.position.x + 20))
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

        // ֹͣ�����ٶ�С����ֵ�����·���
        if (GetComponent<Rigidbody>().IsSleeping() || GetComponent<Rigidbody>().velocity.sqrMagnitude < (��С�ٶ� * ��С�ٶ�))
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
