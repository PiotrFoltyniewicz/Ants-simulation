using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    float movementSpeed = 1.5f; //maksymalna prêdkoœæ
    float maxTurnAngle = 15f; // maksymalny k¹t skrêtu
    float stepTime = 0.5f; // czas pomiêdzy krokami


    void Start()
    {
        //powiêkszenie na cele testów
        transform.localScale *= 8f;
    }

    void Update()
    {
        transform.position += (Vector3.up * movementSpeed * Time.deltaTime);
    }
}
