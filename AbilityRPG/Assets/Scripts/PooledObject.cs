using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
    public string strPoolName;
    public GameObject gPrefab;
    public int nPoolCount = 0;

    //오브젝트를 담을 직렬화된 공간
    [SerializeField]
    private List<GameObject> poolList = new List<GameObject>();

    public Transform tfParent;     //계층 구조의 부모가 될 변수

    public void Initialize(GameObject parent = null)
    {
        if(parent == null)
        {
            parent = new GameObject("ObjectPool");
        }

        tfParent = new GameObject(gPrefab.name + "_Pool").transform;
        tfParent.transform.SetParent(parent.transform);

        for(int i = 0; i < nPoolCount; ++i)
        {
            poolList.Add(CreateObject(tfParent));
        }
    }

    //사용한 오브젝트를 풀에 반환한다.
    public void PushObject(GameObject obj, Transform parent = null)
    {
        if(parent == null)
        {
            parent = tfParent;
        }

        obj.transform.SetParent(parent);
        obj.SetActive(false);
        poolList.Add(obj);
    }

    //사용할 오브젝트를 풀에서 꺼낸다.
    public GameObject PopObject(Transform parent = null)
    {
        //풀이 비어있으면 null 반환한다. -> 생성
        if (poolList.Count == 0)
        {
            poolList.Add(CreateObject(parent));
            //return null;
        }

        GameObject obj = poolList[0];  //반환할, 풀의 첫 번째 오브젝트를 저장한다.
        poolList.RemoveAt(0);          //첫 번째 오브젝트를 지운다.

        return obj;
    }

    GameObject CreateObject(Transform parent = null)
    {
        GameObject obj = Object.Instantiate(gPrefab) as GameObject;

        if (strPoolName == null)
        {
            strPoolName = gPrefab.name;
        }

        obj.name = strPoolName;
        obj.transform.SetParent(parent);
        obj.SetActive(false);

        return obj;
    }

    public int GetPoolSize()
    {
        return poolList.Count;
    }
}
