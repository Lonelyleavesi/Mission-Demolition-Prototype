using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    public int numClouds = 40;          // Ҫ�����Ƶ�����
    public GameObject[] cloudPrefabs;   // �ƶ�Ԥ�������
    public Vector3 cloudPosMin;         // �ƶ�λ�õ�����
    public Vector3 cloudPosMax;         // �ƶ�λ�õ�����
    public float cloudScaleMin = 1;     // �ƶ����С���ű���
    public float cloudScaleMax = 5;     // �ƶ��������ű���
    public float cloudSpeedMult = 0.5f; // �����ƶ��ٶ�
    public bool _______________________________________;

    public GameObject[] cloudInstances;


    private void Awake()
    {
        // ���������ƶ�ʵ��
        cloudInstances = new GameObject[numClouds];
        // ����CloudAnchor������
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            // ��0�� cloudPrefabs.Length - 1 ֮��ѡ��һ������
            // Random.Range ����ֵ�в�������Χ����
            int prefabNum = Random.Range(0, cloudPrefabs.Length);
            // ����һ��ʵ��
            cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;
            // �����ƶ�λ��
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y); 
            // �����ƶ����ű���
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            // ��С���ƶ䣨��scaleUֵ��С�������Ͻ�
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            cPos.z = 100 - 90 * scaleU; // ��С���ƾ���Զ

            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one* scaleVal;

            cloud.transform.parent = anchor.transform;
            cloudInstances[i] = cloud;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.localPosition;
            // �ƶ�Խ�� �ٶ�Խ��
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            // ����˶��Ѿ�λ�ڻ�������Զλ��
            if (cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cPos;
        }
    }
}
