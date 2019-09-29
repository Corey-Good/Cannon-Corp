using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PaintballLauncher : MonoBehaviour
{
    public  GameObject bullet;
    public  GameObject bulletSpawnLocation;
    private GameObject bulletCopy;
    private Rigidbody  bulletRB;
    
    
    private float timeElapsed = 0f;
    private bool  bulletActive = false;
    private float reloadSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["reloadSpeed"];
    private float bulletSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletSpeed"];
    private float bulletArch = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletArch"];


    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && bulletActive == false)
        {
            bulletCopy = Instantiate(bullet, bulletSpawnLocation.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            bulletRB = bulletCopy.GetComponent<Rigidbody>();
            bulletRB.AddRelativeForce(((transform.forward * bulletSpeed) + (transform.up * bulletArch)), ForceMode.Impulse);
            bulletActive = true;
        }

        if(bulletActive)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= reloadSpeed)
            {
                Destroy(bulletCopy);
                timeElapsed = 0f;
                bulletActive = false;
            }
        }
    }
}
