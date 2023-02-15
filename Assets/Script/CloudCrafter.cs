using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    public int numClouds = 40;          // 要创建云的数量
    public GameObject[] cloudPrefabs;   // 云朵预设的数组
    public Vector3 cloudPosMin;         // 云朵位置的下限
    public Vector3 cloudPosMax;         // 云朵位置的上限
    public float cloudScaleMin = 1;     // 云朵的最小缩放比例
    public float cloudScaleMax = 5;     // 云朵的最大缩放比例
    public float cloudSpeedMult = 0.5f; // 调整云朵速度
    public bool _______________________________________;

    public GameObject[] cloudInstances;


    private void Awake()
    {
        // 储存所有云朵实例
        cloudInstances = new GameObject[numClouds];
        // 查找CloudAnchor父对象
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            // 在0到 cloudPrefabs.Length - 1 之间选择一个整数
            // Random.Range 返回值中不包括范围上限
            int prefabNum = Random.Range(0, cloudPrefabs.Length);
            // 创造一个实例
            cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;
            // 设置云朵位置
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y); 
            // 设置云朵缩放比例
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            // 较小的云朵（即scaleU值较小）离地面较近
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            cPos.z = 100 - 90 * scaleU; // 较小的云距离远

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
            // 云朵越大 速度越快
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            // 如果运动已经位于画面左侧较远位置
            if (cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cPos;
        }
    }
}
