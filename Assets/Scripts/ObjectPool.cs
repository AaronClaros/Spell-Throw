using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    public int poolSize = 5;
    public GameObject instance;
    public bool prePopulate;
    List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        if (prePopulate)
            PrePolpulate();
    }

    public void PrePolpulate()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var clone = CreateInstance();
            clone.SetActive(false);
        }
    }

    public GameObject NextObject()
    {
        foreach (var instance in pool)
        {
            if (instance.activeSelf != true)
            {
                return instance;
            }
        }
        return CreateInstance();
    }

    private GameObject CreateInstance()
    {
        var clone = Instantiate(instance) as GameObject;
        clone.transform.position = Vector2.zero;
        pool.Add(clone);
        return clone;
    }
}
