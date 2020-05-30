using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static event Action OnMouseLeftClickHold = delegate { };

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnMouseLeftClickHold();
        }
    }
}
