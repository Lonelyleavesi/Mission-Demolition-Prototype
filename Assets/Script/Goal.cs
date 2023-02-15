using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    private void OnTriggerEnter(Collider other)
    {
        // 当其他物体撞到触发器时
        // 检查是否为弹丸
        if (other.gameObject.tag == "Projectile")
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile.enabled)
            {
                // 如果是弹丸、设置goalMet为true
                Goal.goalMet = true;
                // 同时将颜色的不透明度设置的更高
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
