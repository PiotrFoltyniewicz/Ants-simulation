using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * klasa ObjectPooling za przechowywanie w puli i tworzenie obiektow punktow
 * celem tego jest optymalizacja -> punkty sa wlaczane i wylaczane zamiast tworzone i niszczone
 */
public class ObjectPooling : MonoBehaviour
{
    public static List<GameObject> pooledToNestPoints = new List<GameObject>();
    public static List<GameObject> pooledToFoodPoints = new List<GameObject>();
    public GameObject toNestPoint;
    public GameObject toFoodPoint;

    // metoda odpowiadajaca za wziecie punktu z puli punktow prowadzacych do mrowiska
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
        // jezeli nie ma wylaczanych punktow w puli to tworzy nowy punkt
        point = Instantiate(toNestPoint, transform.position, Quaternion.identity, null);
        pooledToNestPoints.Add(point);
        return point;
    }

    // metoda odpowiadajaca za wziecie punktu z puli punktow prowadzacych do jedzenia
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
        // jezeli nie ma wylaczanych punktow w puli to tworzy nowy punkt
        point = Instantiate(toFoodPoint, transform.position, Quaternion.identity, null);
        pooledToFoodPoints.Add(point);
        return point;
    }
}
