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
using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Responsible for detecting and acting on player collisions
/// Sends messages to any listeners of IPlayerEvents
/// Listens to main game controller and freezes player when game over
/// </summary>
public class PlayerBrain : MonoBehaviour//, IMainGameEvents
{
    public float speed = 2.2f;
    public int damageFromEnemyContact = 11;
    public AudioClip soundEffectEnemyContact;
    public GameObject particleContactPrefab;

    private Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;
    private GameObject particleContactInstance;
    private ParticleSystem particleSystemContactInstance;
    // particle system from above gameobject
    private int playerHitPoints;
    private float speedOriginal;
    private bool isPlayerInvulnerable;
    private float horizSpeed;
    private float vertSpeed;
    private static float reloadOriginal;
    private float oldMovementMultiplier;
    private float oldRotateMultiplier;
    public GameObject tankbase;
    public GameObject tankhead;
    Renderer tankbaseRenderer;
    Renderer tankheadRenderer;
    float reloadBoostTimer = 0.0f;
    Vector3 movementBoostStartLocation;
    float totalDistanceTravelled = 0.0f;

    bool isInvisible = false;
    bool reloadBoostActive = false;
    bool movementBoostActive = false;

    //void IMainGameEvents.OnGameWon ()
    //{
    //    // Remove from physics (no collisions, no movement) if game over
    //    rigidBody.simulated = false;
    //}

    //void IMainGameEvents.OnGameLost ()
    //{
    //    // Remove from physics (no collisions, no movement) if game over
    //    rigidBody.simulated = false;
    //    // We lose our yellow color
    //    spriteRenderer.color = Color.grey;
    //}

    private void Awake ()
    {
        rigidBody = GetComponent<Rigidbody> ();
        tankbaseRenderer = tankbase.GetComponent<Renderer>();
        tankheadRenderer = tankhead.GetComponent<Renderer>();
    }

    // Use this for initialization
    void Start ()
    {
        playerHitPoints = 100;
    }

    void FixedUpdate()
    {
        // Removes boost after set amount of time
        if(reloadBoostActive)
        {
            reloadBoostTimer += 0.1f;
        }
        if(reloadBoostTimer > 55.0f)
        {
            SetFasterReloadOff();
        }
        Debug.Log(reloadBoostTimer);

        // Removes invisibility after firing
        if(isInvisible)
        {
            if(Input.GetMouseButtonDown(KeyBindings.clickIndex))
            {
                SetInvisibilityOff();
            }
        }

        // Removes movement boost after set distance is travelled
        if(movementBoostActive)
        {
            if(totalDistanceTravelled < 50.0f)
            {
                totalDistanceTravelled += Vector3.Distance(movementBoostStartLocation, this.transform.position);
                movementBoostStartLocation = this.transform.position;
            } else if (totalDistanceTravelled > 50.0f)
            {
                SetFasterMovementOff();
            }
        }
        //Debug.Log(totalDistanceTravelled);
    }

    public void SetHealthAdjustment (int adjustmentAmount)
    {
        playerHitPoints += adjustmentAmount;
        if(LoadUI.currentHealth + adjustmentAmount > LoadUI.totalHealth)
        {
            LoadUI.currentHealth = LoadUI.totalHealth;
        } else
        {
            LoadUI.currentHealth += adjustmentAmount;
        }
        if (playerHitPoints > 100)
        {
            playerHitPoints = 100;
        }

        //SendPlayerHurtMessages ();
    }

    /// <summary>
    /// Send message to listenrs that player has lost some health
    /// </summary>
    //private void SendPlayerHurtMessages ()
    //{
    //    // Send message to any listeners
    //    foreach (GameObject go in EventSystemListeners.main.listeners)
    //    {
    //        ExecuteEvents.Execute<IPlayerEvents> (go, null, (x, y) => x.OnPlayerHurt (playerHitPoints));
    //    }
    //}

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

    //public void SetSpeedBoostOn (float speedMultiplier)
    //{
    //    speedOriginal = speed;
    //    speed *= speedMultiplier;
    //}

    //public void SetSpeedBoostOff ()
    //{
    //    speed = speedOriginal;
    //}

    public void SetInvulnerability (bool isInvulnerabilityOn)
    {
        isPlayerInvulnerable = isInvulnerabilityOn;
    }

    //private void OnTriggerEnter2D (Collider2D collision)
    //{
    //    // What did we trigger?
    //    if (collision.gameObject.tag == "Exit")
    //    {
    //        // Send message to any listeners
    //        foreach (GameObject go in EventSystemListeners.main.listeners)
    //        {
    //            ExecuteEvents.Execute<IPlayerEvents> (go, null, (x, y) => x.OnPlayerReachedExit (collision.gameObject));
    //        }
    //    }
    //}

    //private void OnCollisionEnter2D (Collision2D collision)
    //{
    //    // What did we hit?
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        // Lose health only if we're not invulnerable
    //        if (!isPlayerInvulnerable)
    //        {
    //            if (soundEffectEnemyContact != null)
    //            {
    //                MainGameController.main.PlaySound (soundEffectEnemyContact);
    //            }

    //            // Some small collision particles
    //            SpawnCollisionParticles (collision.transform.position, collision.transform.rotation);

    //            SetHealthAdjustment (-damageFromEnemyContact);
    //        }
    //    }
    //}

    //private void SpawnCollisionParticles (Vector3 pos, Quaternion rot)
    //{
    //    // Just one system that we keep re-using (if it is in use we don't spawn any particles)
    //    if (particleContactPrefab != null)
    //    {
    //        if (particleContactInstance == null)
    //        {
    //            // First time usage
    //            particleContactInstance = Instantiate (particleContactPrefab, pos, rot, transform);
    //            particleSystemContactInstance = particleContactInstance.GetComponent<ParticleSystem> ();
    //        } else
    //        {
    //            if (!particleSystemContactInstance.IsAlive ())
    //            {
    //                // Reuse existing particle system
    //                particleContactInstance.transform.SetPositionAndRotation (pos, rot);
    //                particleSystemContactInstance.Play ();
    //            }
    //        }
    //    }
    //}

    // update is called once per frame
//    private void Update ()
//    {
//        // We poll for movement here as opposed to FixedUpdate so we dont miss any frames
//        horizSpeed = Input.GetAxis ("Horizontal") * speed;
//        vertSpeed = Input.GetAxis ("Vertical") * speed;
//    }

//    private void FixedUpdate ()
//    {
//        // Movement
//        rigidBody.velocity = new Vector2 (horizSpeed, vertSpeed);
//    }
}
