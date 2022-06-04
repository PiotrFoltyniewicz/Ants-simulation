using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Variables
{
    public static Dictionary<string, object> variables = new Dictionary<string, object>()
    {
        {"antMoveSpeed", 1f},
        {"antMaxTurnStrength", 0.3f},
        {"antStepTime", 0.2f},
        {"antLeavePointTime", 0.3f},
        {"antSensorRadius", 0.2f},
        {"timeSpeed", 1.5f},
        {"foodSpawnRadiusInPoint", 0.4f},
        {"antMaxNumberOfPoints", 40},
        {"amountOfAnts", 100},
        {"foodHealth", 5},
        {"amountOfFoodInPoint", 25},
        {"amountOfFoodPoints", 3},
    };

    public static object GetVariable(string key)
    {
        return variables[key];
    }
    public static void SetVariable(string key, object value)
    {
        variables[key] = value;
    }
    public static void SetToDefault()
    {
        variables["antMoveSpeed"] = 1f;
        variables["antMaxTurnStrength"] = 0.3f;
        variables["antStepTime"] = 0.2f;
        variables["antLeavePointTime"] = 0.3f;
        variables["antMaxNumberOfPoints"] = 40;
        variables["antSensorRadius"] = 0.2f;
        variables["amountOfAnts"] = 100;
        variables["timeSpeed"] = 1.5f;
        variables["foodHealth"] = 5;
        variables["amountOfFoodInPoint"] = 25;
        variables["foodSpawnRadiusInPoint"] = 0.4f;
        variables["amountOfFoodPoints"] = 3;
    }
}
