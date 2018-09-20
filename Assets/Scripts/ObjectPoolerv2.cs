using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerv2 : MonoBehaviour
{

    /*
    private Dictionary<int, List<GameObject>> pool = new Dictionary<int, List<GameObject>>();


    public static ObjectPoolerv2 singleton = null;
    // Use this for initialization
    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    /// <summary> Adds new prefab instance to pool. </summary>
    private GameObject AddInstanceToPool(GameObject argPrefab)
    {
        int instanceID = argPrefab.GetInstanceID();
        GameObject instance = Instantiate(argPrefab);
        instance.name = argPrefab.name;
        instance.transform.parent = transform;
        if (pool.ContainsKey(instanceID) == false)
        {
            pool.Add(instanceID, new List<GameObject>());
        }
        pool[instanceID].Add(instance);
        return instance;
    }

    /// <summary> Gets pooled instance. </summary>
    private GameObject GetPooledInstance(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation)
    {
        int instanceID = argPrefab.GetInstanceID();
        GameObject instance = null;
        if (pool.ContainsKey(instanceID) && pool[instanceID].Count != 0)
        {
            for (int i = 0; i < pool[instanceID].Count; i++)
            {
                if (!pool[instanceID][i].activeInHierarchy)
                {
                    instance = pool[instanceID][i];
                    instance.transform.position = argPosition;
                    instance.transform.rotation = argRotation;
                    instance.SetActive(true);
                    return instance;
                }
            }
        }
        instance = AddInstanceToPool(argPrefab);
        instance.transform.position = argPosition;
        instance.transform.rotation = argRotation;
        instance.SetActive(true);

        //Debug.Log(instance.GetInstanceID());

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
    */

    public GameObject pooledObject;
    public int pooledAmount;
    public bool willGrow;
    public List<GameObject> pooledObjectList;
    public static ObjectPoolerv2 singleton = null;
    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }
    void Start () {
        pooledObjectList = new List< GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjectList.Add(obj);
        }
	}

    public GameObject GetPooledObject()
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
    
}
