/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Vector3 mousePos;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 1.0f;
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = cursorPos;
    }
}