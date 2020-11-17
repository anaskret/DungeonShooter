using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerModel
{
    public static float Hearts { get; private set; } = 5;
    public static float DamageCooldown { get; private set; } = 0;
    public static int Damage { get; private set; } = 1;
    public static float Speed { get; private set; }
    public static float SpecialAbilityCooldown { get; private set; }

    private static readonly float damageRate = 3;
    private static readonly float specialAbilityRate = 20;

    public static void ChangeHealth(float change)
    {
        if(Hearts<10 && Hearts>0)
            Hearts += change;
    }

    public static void SetDamageCooldown()
    {
        DamageCooldown = Time.time + damageRate;
    }
    
    public static void SetSpecialAbilityCooldown()
    {
        SpecialAbilityCooldown = Time.time + specialAbilityRate;
    }

    public static void BuffDamage()
    {
        Damage++;
    }

    public static void ChangeSpeed(float speed)
    {
        Speed = speed;
    }

    public static void SpecialAbilityUsed()
    {
        Damage *= 2;
    }

    public static void SpecialAbilityFinished()
    {
        Damage /= 2;
    }
    
}
