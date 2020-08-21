using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RowObjectPool : MonoBehaviour
{

    [SerializeField] private GameObject prefabObject;
    [SerializeField] private int poolDepth;
    [SerializeField] private bool canGrow = true;

    private readonly List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolDepth; i++)
        {
            GameObject pooledObject = Instantiate(prefabObject);
            pooledObject.SetActive(false);
            pool.Add(pooledObject);
        }
    }

    public GameObject GetAvailableRow()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }

        }

        if (canGrow)
        {
            GameObject pooledObject = Instantiate(prefabObject);
            pooledObject.SetActive(false);
            pool.Add(pooledObject);

            return pooledObject;
        }
        else
        {
            return null;
        }
        
    }
    
}
