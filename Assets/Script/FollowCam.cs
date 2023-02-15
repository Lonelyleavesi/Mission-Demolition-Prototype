using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S;
    public float easing = 0.05f;
    public Vector2 minXY;
    public bool ________________________________________;

    public GameObject poi; // ��Ȥ��
    public float camZ;

    private void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 destionation;
        if (poi == null) {
            destionation = Vector3.zero;
        } else  {
            destionation = poi.transform.position;
            if (poi.tag == "Projectile") {
                // ���������sleeping״̬����δ�ƶ���
                Projectile projectile = poi.GetComponent<Projectile>();
                if (projectile.IsStopNearly())
                {
                    // ����Ĭ����ͼ
                    poi = null;
                    return;
                }
            }
        }

        // �޶�x��y����Сֵ
        destionation.x = Mathf.Max(destionation.x, minXY.x);
        destionation.y = Mathf.Max(destionation.y, minXY.y);
        destionation = Vector3.Lerp(transform.position, destionation, easing);
        destionation.z = camZ;
        transform.position = destionation;
        this.GetComponent<Camera>().orthographicSize = destionation.y + 15;
    }

}
