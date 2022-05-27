using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static List<GameObject> pooledToNestPoints = new List<GameObject>();
    public static List<GameObject> pooledToFoodPoints = new List<GameObject>();
    public GameObject toNestPoint;
    public GameObject toFoodPoint;

    public GameObject GetToNestPoint()
    {
        GameObject point = null;
        foreach (var pointObj in pooledToNestPoints)
        {
            if (!pointObj.activeInHierarchy)
            {
                return pointObj;
            }
        }
        point = Instantiate(toNestPoint, transform.position, Quaternion.identity, null);
        pooledToNestPoints.Add(point);
        return point;
    }

    public GameObject GetToFoodPoint()
    {
        GameObject point = null;
        foreach (var pointObj in pooledToFoodPoints)
        {
            if (!pointObj.activeInHierarchy)
            {
                return pointObj;
            }
        }
        point = Instantiate(toFoodPoint, transform.position, Quaternion.identity, null);
        pooledToFoodPoints.Add(point);
        return point;
    }
}
