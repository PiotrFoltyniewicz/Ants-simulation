using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    float movementSpeed = 1.5f; //maksymalna pr�dko��
    float maxTurnAngle = 15f; // maksymalny k�t skr�tu
    float stepTime = 0.5f; // czas pomi�dzy krokami


    void Start()
    {
        //powi�kszenie na cele test�w
        transform.localScale *= 8f;
    }

    void Update()
    {
        transform.position += (Vector3.up * movementSpeed * Time.deltaTime);
    }
}
