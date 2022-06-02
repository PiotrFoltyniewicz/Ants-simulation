using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Variables
{
     public static float antMoveSpeed = 1f;
    public static float antMaxTurnStrength = 0.3f;
    public static float antStepTime = 0.2f;
    public static float antLeavePointTime = 0.3f;
    public static int antMaxNumberOfPoints = 40;
    public static float antSensorRadius = 0.2f;
    public static int amountOfAnts = 100;
    public static float timeSpeed = 1.5f;
    public static int foodHealth = 5;
    public static int amountOfFoodInPoint = 25;
    public static float foodSpawnRadiusInPoint = 0.4f;
    public static int amountOfFoodPoints = 3;
    public static void SetToDefault()
    {
        antMoveSpeed = 1f;
        antMaxTurnStrength = 0.3f;
        antStepTime = 0.2f;
        antLeavePointTime = 0.3f;
        antMaxNumberOfPoints = 40;
        antSensorRadius = 0.2f;
        amountOfAnts = 100;
        timeSpeed = 1.5f;
        foodHealth = 5;
        amountOfFoodInPoint = 25;
        foodSpawnRadiusInPoint = 0.4f;
        amountOfFoodPoints = 3;
    }
}
