using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<PooledObject> lObjectPool = new List<PooledObject>();

    private new void Awake()
    {
        for(int i = 0; i < lObjectPool.Count; ++i)
        {
            lObjectPool[i].Initialize(this.gameObject);
        }
    }

    //적합한 풀에 오브젝트를 반환한다.
    public bool PushToPool(string poolName, GameObject obj, Transform parent = null)
    {
        PooledObject pool = GetPooledObject(poolName);

        if(pool == null)
        {
            return false;
        }

        //위에서 반환된 풀에 오브젝트를 반환한다.
        pool.PushObject(obj, parent);
        return true;
    }

    //적합한 풀에서 오브젝트를 가져온다.
    public GameObject PopFromPool(string poolName, Transform parent = null)
    {
        PooledObject pool = GetPooledObject(poolName);

        if(pool == null)
        {
            return null;
        }

        return pool.PopObject(parent);
    }

    public PooledObject GetPooledObject(string poolName)
    {
        for(int i = 0; i < lObjectPool.Count; ++i)
        {
            if(lObjectPool[i].strPoolName.Equals(poolName))
            {
                return lObjectPool[i];
            }
        }

        Debug.Log("There is no matched pool in list.");
        return null;
    }
}
