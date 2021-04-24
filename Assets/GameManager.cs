using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static float _maxOxygen = 100;

    private static float _oxygen = 50;

    public static float MaxOxygen => _maxOxygen;
    public static float Oxygen => _oxygen;

    public static void ReduceOxygen()
    {
        _oxygen--;
    }
}
