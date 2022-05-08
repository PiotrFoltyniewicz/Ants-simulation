using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AntState: MonoBehaviour
{
    public abstract void Move();
    public abstract void Turn(float turnAngle);

}
