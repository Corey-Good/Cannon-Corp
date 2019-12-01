/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using UnityEngine;

public class RandomFallThrough : MonoBehaviour
{
    private float time = 0.0f;

    public void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 3.0f)
        {
            if (Random.Range(0, 4) == 0)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                GetComponent<BoxCollider>().enabled = true;
            }
            time = 0.0f;
        }
    }
}