using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    private void OnTriggerEnter(Collider other)
    {
        // ����������ײ��������ʱ
        // ����Ƿ�Ϊ����
        if (other.gameObject.tag == "Projectile")
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile.enabled)
            {
                // ����ǵ��衢����goalMetΪtrue
                Goal.goalMet = true;
                // ͬʱ����ɫ�Ĳ�͸�������õĸ���
                Color c = GetComponent<Renderer>().material.color;
                c.a = 1;
                GetComponent<Renderer>().material.color = c;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
