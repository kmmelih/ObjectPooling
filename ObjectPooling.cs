using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPooling : MonoBehaviour
{
    public GameObject objectPrefab;
    private Stack<GameObject> objectsPool = new Stack<GameObject>();

    private void Start()
    {
        for(var i = 0; i < 500; i++)
        {
            StartCoroutine(CreateAndDestroyObject());
        }
    }

    private IEnumerator CreateAndDestroyObject()
    {
        while (true)
        {
            var obj = FindObjectFromPool();
            var pos = new Vector3(Random.Range(-50, 50), 1, Random.Range(-50,50));
            obj.transform.position = pos;
            yield return new WaitForSeconds(1f);
            AddObjectsToPool(obj);
            yield return new WaitForSeconds(1f);
        }
    }

    private GameObject FindObjectFromPool()
    {
        if (objectsPool.Count > 0)
        {
            var rGameObject = objectsPool.Pop();
            rGameObject.SetActive(true);
            return rGameObject;
        }
        return Instantiate(objectPrefab);
    }

    private void AddObjectsToPool(GameObject g)
    {
        g.SetActive(false);
        objectsPool.Push(g);
    }
}
