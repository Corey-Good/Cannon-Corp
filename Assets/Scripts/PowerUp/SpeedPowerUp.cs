using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    public float newMovementMultiplier = 2.0f;
    public float newRotateMultiplier = 2.0f;
    // Start is called before the first frame update
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        playerBrain.SetFasterMovementOn(newMovementMultiplier, newRotateMultiplier);
    }
}
