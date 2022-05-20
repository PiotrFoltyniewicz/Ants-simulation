using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStateReturn : AntState
{
    public override void Turn(float turnAngle = 0, Transform target = null)
    {

    }

    // pozostawienie punktu
    public override void LeavePoint(Vector2 position)
    {
        throw new System.NotImplementedException();
    }
}
