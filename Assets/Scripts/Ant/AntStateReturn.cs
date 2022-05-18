using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStateReturn : AntState
{
    public override void Turn(float turnAngle)
    {

    }

    // pozostawienie punktu
        public override void LeavePoint(Vector2 position)
    {
        throw new System.NotImplementedException();
    }
}
