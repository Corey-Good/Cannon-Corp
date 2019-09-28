using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PaintballLauncher : MonoBehaviour
{
    public  GameObject bullet;
    private GameObject bulletCopy;
    private Rigidbody  bulletRB;
    public  GameObject bulletSpawnLocation;
    
    private float timeElapsed = 0f;
    private float reloadSpeed = 2.75f;
    private bool  bulletActive = false;


    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && bulletActive == false)
        {
            bulletCopy = Instantiate(bullet, bulletSpawnLocation.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            bulletRB = bulletCopy.GetComponent<Rigidbody>();
            bulletRB.AddRelativeForce((transform.forward * 55.0f + transform.up * 2.7f), ForceMode.Impulse);
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
