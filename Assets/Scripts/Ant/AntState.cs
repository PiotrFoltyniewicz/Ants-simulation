using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AntState: MonoBehaviour
{
    // funkcja ktora ma implementacje w klasach dziedziczacych, odpowiada za ruch do przodu
    public abstract void Move();
    // funkcja ktora ma implementacje w klasach dziedziczacych, odpowiada za obrot mrowki
    public abstract void Turn(float turnAngle);

}
