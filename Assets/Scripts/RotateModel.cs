/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float rotateSpeed = 18f;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}