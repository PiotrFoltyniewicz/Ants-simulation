using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    AntState antState;
    public static float movementSpeed = 4f; //maksymalna predkosc
    public static float maxTurnAngle = 10f; // maksymalna sila skretu
    public static float stepTime = 0.2f; // czas pomiedzy krokami
    float stepTimeLeft = 0f;

    AntState[] states;

    void Awake()
    {
        states = new AntState[3]{ gameObject.AddComponent<AntStateSearch>(), gameObject.AddComponent<AntStateFollow>(), gameObject.AddComponent<AntStateReturn>() };
    }

    void Start()
    {
        //powiekszenie na cele testow
        transform.localScale *= 8f;

        ChangeState(0);
    }

    void Update()
    {
        stepTimeLeft -= Time.deltaTime;
        if(stepTimeLeft <= 0)
        {
            stepTimeLeft = stepTime;
            antState.Turn(maxTurnAngle);
        }

        antState.Move();
    }

    public void ChangeState(int stateNum)
    {
        switch (stateNum)
        {
            case 0:
                antState = states[0];
                break;
            case 1:
                antState = states[1];
                break;
            case 2:
                antState = states[2];
                break;
        }
    }
}
