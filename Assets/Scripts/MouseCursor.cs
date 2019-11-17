using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{

    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 1.0f;
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = cursorPos;
    }
}