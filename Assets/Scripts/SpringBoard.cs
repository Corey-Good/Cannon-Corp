using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    private void OnCollisionStay(Collision collisionInfo)
    {
        int randomNumber = Random.Range(0, 115);
        float direction;
        if(Random.Range(0,2) == 0)
        {
            direction = 0.75f;
        }
        else
        {
            direction = -0.5f;
        }
        
        if (randomNumber == 0)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, direction) * 2500, ForceMode.Impulse);
          
        }
    }
}
