#region CopyRight Region

/*
 * Copyright (c) 2017 Razeware LLC
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish,
 * distribute, sublicense, create a derivative work, and/or sell copies of the
 * Software in any work that is designed, intended, or marketed for pedagogical or
 * instructional purposes related to programming, coding, application development,
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works,
 * or sale is expressly withheld.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

#endregion CopyRight Region

using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    public float speed = 2.2f;

    public int damageFromEnemyContact = 11;

    public GameObject tankbase;
    public GameObject tankhead;

    private int playerHitPoints; 

    private Renderer tankbaseRenderer;
    private Renderer tankheadRenderer;

    private float reloadBoostTimer = 0.0f;
    private float totalDistanceTravelled = 0.0f;
    private float oldMovementMultiplier;
    private float oldRotateMultiplier;
    private static float reloadOriginal;

    private Vector3 movementBoostStartLocation;

    private bool isInvisible = false;
    private bool reloadBoostActive = false;
    private bool movementBoostActive = false;
    private bool isPlayerInvulnerable;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        tankbaseRenderer = tankbase.GetComponent<Renderer>();
        tankheadRenderer = tankhead.GetComponent<Renderer>();
    }

    private void Start()
    {
        playerHitPoints = 100;
    }

    private void FixedUpdate()
    {
        // Removes boost after set amount of time
        if (reloadBoostActive)
        {
            reloadBoostTimer += 0.1f;
        }
        if (reloadBoostTimer > 55.0f)
        {
            SetFasterReloadOff();
        }
        Debug.Log(reloadBoostTimer);

        // Removes invisibility after firing
        if (isInvisible)
        {
            if (Input.GetMouseButtonDown(KeyBindings.clickIndex))
            {
                SetInvisibilityOff();
            }
        }

        // Removes movement boost after set distance is travelled
        if (movementBoostActive)
        {
            if (totalDistanceTravelled < 50.0f)
            {
                totalDistanceTravelled += Vector3.Distance(movementBoostStartLocation, this.transform.position);
                movementBoostStartLocation = this.transform.position;
            }
            else if (totalDistanceTravelled > 50.0f)
            {
                SetFasterMovementOff();
            }
        }
        //Debug.Log(totalDistanceTravelled);
    }

    public void SetHealthAdjustment(int adjustmentAmount)
    {
        playerHitPoints += adjustmentAmount;
        if (LoadUI.currentHealth + adjustmentAmount > LoadUI.totalHealth)
        {
            LoadUI.currentHealth = LoadUI.totalHealth;
        }
        else
        {
            LoadUI.currentHealth += adjustmentAmount;
        }
        if (playerHitPoints > 100)
        {
            playerHitPoints = 100;
        }
    }

    public void SetInvisibilityOn()
    {
        tankbaseRenderer.enabled = false;
        tankheadRenderer.enabled = false;
        isInvisible = true;
    }

    public void SetInvisibilityOff()
    {
        tankbaseRenderer.enabled = true;
        tankheadRenderer.enabled = true;
        isInvisible = false;
    }

    public void SetFasterReloadOn(float newSpeedMultiplier)
    {
        if (reloadBoostActive)
        {
            SetFasterReloadOff();
        }
        reloadBoostActive = true;
        reloadBoostTimer = 0.0f;
        reloadOriginal = PlayerMovement.reloadMultiplier;
        PlayerMovement.reloadMultiplier = newSpeedMultiplier;
    }

    public void SetFasterReloadOff()
    {
        PlayerMovement.reloadMultiplier = reloadOriginal;
        reloadBoostActive = false;
    }

    public void SetFasterMovementOn(float newMovementMultiplier, float newRotateMultiplier)
    {
        if (movementBoostActive)
        {
            SetFasterMovementOff();
        }
        movementBoostActive = true;
        oldMovementMultiplier = PlayerMovement.movementMultiplier;
        oldRotateMultiplier = PlayerMovement.rotateMultiplier;
        PlayerMovement.movementMultiplier = newMovementMultiplier;
        PlayerMovement.rotateMultiplier = newRotateMultiplier;
        movementBoostStartLocation = this.transform.position;
        totalDistanceTravelled = 0.0f;
    }

    public void SetFasterMovementOff()
    {
        PlayerMovement.movementMultiplier = oldMovementMultiplier;
        PlayerMovement.rotateMultiplier = oldRotateMultiplier;
        movementBoostActive = false;
    }

    public void SetInvulnerability(bool isInvulnerabilityOn)
    {
        isPlayerInvulnerable = isInvulnerabilityOn;
    }
}