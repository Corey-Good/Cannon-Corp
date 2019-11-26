


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