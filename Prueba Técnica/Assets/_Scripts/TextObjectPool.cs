using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextObjectPool : MonoBehaviour
{
    [SerializeField] private Text prefabObject;
    [SerializeField] private int poolDepth;
    [SerializeField] private bool canGrow = true;

    private readonly List<Text> pool = new List<Text>();

    private void Awake()
    {
        for (int i = 0; i < poolDepth; i++)
        {
            Text pooledObject = Instantiate(prefabObject);
            pooledObject.gameObject.SetActive(false);
            pool.Add(pooledObject);
        }
    }

    public Text GetAvailableText()
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
            Text pooledObject = Instantiate(prefabObject);
            pooledObject.gameObject.SetActive(false);
            pool.Add(pooledObject);

            return pooledObject;
        }
        else
        {
            return null;
        }
        
    }
}
