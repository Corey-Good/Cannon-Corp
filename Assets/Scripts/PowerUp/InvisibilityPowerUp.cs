using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPowerUp : PowerUp
{
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        playerBrain.SetInvisibilityOn();
    }

    protected override void PowerUpHasExpired()
    {
        base.PowerUpHasExpired();

        playerBrain.SetInvisibilityOff();
    }
}
