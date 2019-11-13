using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ReloadPowerUp : PowerUp
{
    public float ReloadMultiplier = .1f;

    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        playerBrain.SetFasterReloadOn(ReloadMultiplier);
    }

    protected override void PowerUpHasExpired()
    {
        base.PowerUpHasExpired();

        playerBrain.SetFasterReloadOff();
    }

    
}
