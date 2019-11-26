


internal class HealthPowerUp : PowerUp
{
    public int healthBonus = 20;

    protected override void PowerUpPayload()  // Checklist item 1
    {
        base.PowerUpPayload();

        // Payload is to give some health bonus
        playerBrain.SetHealthAdjustment(healthBonus);
    }
}