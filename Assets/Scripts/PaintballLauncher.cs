﻿using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Photon.Pun;

public class PaintballLauncher : MonoBehaviourPun
{
    public  GameObject bullet;              // Contains the bullet model
    private GameObject bulletCopy;          // Copy of the bullet that gets launched
    public  GameObject bulletSpawnLocation; // Where the bull is instantiated
    private Rigidbody  bulletRB;            // Rigibody of the bullet to be launched
        
    private float timeElapsed = 0f;
    public static bool  bulletActive = false;
    private float reloadSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["reloadSpeed"];
    private float bulletSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletSpeed"];
    private float bulletArch = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletArch"];
    public static float reloadProgress;

    private void Awake()
    {
        // Ensures the bar is full when starting the game
        reloadProgress = 1.0f;
    }
    void FixedUpdate()
    {


            if (Input.GetMouseButtonDown(0) && bulletActive == false && photonView.IsMine)
            {
                // Creates a copy of the bullet, and captures its Rigibody (into bulletRB)
                //bulletCopy = Instantiate(bullet, bulletSpawnLocation.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                bulletCopy = PhotonNetwork.Instantiate("Bullet", bulletSpawnLocation.transform.position, Quaternion.Euler(0, 0, 0));
                bulletRB = bulletCopy.GetComponent<Rigidbody>();

                // Applies the launching force to the bullet and sets its status to active (true)
                bulletRB.AddRelativeForce(((transform.forward * bulletSpeed) + (transform.up * bulletArch)), ForceMode.Impulse);
                bulletActive = true;
            }

            if (bulletActive)
            {
                // Increase time and update the reloadBar progress
                timeElapsed += Time.deltaTime;
                reloadProgress = timeElapsed / reloadSpeed;

                // When a bullet is reloaded, delete the previouis copy and reset timer
                if (timeElapsed >= reloadSpeed)
                {
                    Destroy(bulletCopy);
                    timeElapsed = 0f;
                    bulletActive = false;
                }
            }
        
    }
}
