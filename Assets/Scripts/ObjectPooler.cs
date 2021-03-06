﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {


    private Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();


    public static ObjectPooler singleton = null;
   /* public GameObject[] pooledObject;
    public string[] codes;
    public int[] pooledAmount;
    public bool[] willGrow;

    Dictionary<string, GameObject> pooledObjectList;
    */
    // Use this for initialization
    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    /// <summary> Adds new prefab instance to pool. </summary>
    private void AddInstanceToPool(GameObject argPrefab)
    {
        int instanceID = argPrefab.GetInstanceID();
        argPrefab.SetActive(false);
        GameObject instance = Instantiate(argPrefab);
        argPrefab.SetActive(true);
        instance.name = argPrefab.name;
        instance.transform.parent = transform;
        if (pool.ContainsKey(instanceID) == false)
        {
            pool.Add(instanceID, new Queue<GameObject>());
        }
        pool[instanceID].Enqueue(instance);
    }

    /// <summary> Gets pooled instance. </summary>
    private GameObject GetPooledInstance(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation)
    {
        int instanceID = argPrefab.GetInstanceID();
        GameObject instance = null;
        if (pool.ContainsKey(instanceID) && pool[instanceID].Count != 0)
        {
            if (pool[instanceID].Peek().activeInHierarchy == false)
            {
               Debug.Log("inactive");
                instance = pool[instanceID].Dequeue();
                pool[instanceID].Enqueue(instance);
            }
            else
            {
               Debug.Log("active");
                AddInstanceToPool(argPrefab);
                instance = pool[instanceID].Dequeue();
                pool[instanceID].Enqueue(instance);
            }
        }
        else
        {
            AddInstanceToPool(argPrefab);
            instance = pool[instanceID].Dequeue();
            pool[instanceID].Enqueue(instance);
        }

        instance.transform.position = argPosition;
        instance.transform.rotation = argRotation;
        instance.SetActive(true);

        Debug.Log(instance.GetInstanceID());

        return instance;
    }

        /// <summary> Instantiates using pooling system  </summary>
    static public GameObject InstantiatePooled(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation)
    {
        return singleton.GetPooledInstance(argPrefab, argPosition, argRotation);
    }
    static public void InstantiatePooled<T>(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation, System.Action<T> argAction)
    {
        GameObject instance = singleton.GetPooledInstance(argPrefab, argPosition, argRotation);

        T[] tComponents = instance.GetComponentsInChildren<T>();
        for (int i = 0; i < tComponents.Length; i++)
        {
            argAction(tComponents[i]);
        }
    }
    static public void InstantiatePooled(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation, Transform argParentTo)
    {
        GameObject instance = singleton.GetPooledInstance(argPrefab, argPosition, argRotation);

#if UNITY_EDITOR || DEBUG
        if (instance.GetComponent<Rigidbody>() != null && argParentTo.GetComponent<Rigidbody>() != null)
        {
            Debug.LogWarning("Avoid parenting rigidbodies to another rigidbody. This will causes problems. Ie.: " + instance.name + ".transform.SetParent(" + argParentTo.name + ".transform)");
            Debug.Break();
            return;
        }
#endif

        instance.transform.SetParent(argParentTo, true);
    }


    /*

    void Start () {
        pooledObjectList = new Dictionary<string, GameObject>();
        for(int i = 0; i < pooledAmount.Length; i++)
        {
            for(int j=0;j<pooledAmount[i];j++)
            {
                GameObject obj = (GameObject)Instantiate(pooledObject[i]);
                obj.SetActive(false);
                pooledObjectList.Add(codes[i],obj);
            }
        }
	}

    public GameObject GetPooledObject(string code)
    {
        for(int i = 0; i < pooledObjectList.Count; i++)
        {
            if (!pooledObjectList[i].activeInHierarchy)
            {
                return pooledObjectList[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjectList.Add(obj);
            return obj;
        }
        else return null;
    }
    */
}
