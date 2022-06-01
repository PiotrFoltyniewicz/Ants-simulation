using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Variables
{
    public static float antMoveSpeed;
    public static float antMaxTurnStrength;
    public static float antStepTime;
    public static float antLeavePointTime;
    public static int antMaxNumberOfPoints;
    public static float antSensorRadius;
    public static int amountOfAnts;
    public static float timeSpeed;
    public static int foodHealth;
    public static int amountOfFoodInPoint;
    public static float foodSpawnRadiusInPoint;
    public static int amountOfFoodPoints;

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
