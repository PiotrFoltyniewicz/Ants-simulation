using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AntState : Ant
{
    List<AntState> states = new List<AntState>(3);
    public Ant antScript;

    public int currentState = 0;
    
    void Awake()
    {
        states[0] = GetComponent<AntStateSearch>();
        states[1] = GetComponent<AntStateFollow>();
        states[2] = GetComponent<AntStateReturn>();
    }
    void Start()
    {
        ChangeState(currentState);
    }

    public void ChangeState(int stateNum)
    {
        switch(stateNum)
        {
            case 0:
            states[0].enabled = true;
                break;
            case 1:
            states[1].enabled = true;
                break;
            case 2:
            states[2].enabled = true;
                break;
        }
        currentState = stateNum;
    }

    public abstract void Move();
}
