using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float edgeThick = 5;
    private void Update()
    {
        EdgePan();
    }

    private void EdgePan()
    {
        var mousePosition = Mouse.current.position.ReadValue();

        if (mousePosition.x > Screen.width - edgeThick && mousePosition.x < Screen.width)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (mousePosition.x > 0 && mousePosition.x < edgeThick)
        {
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
        }

        if (mousePosition.y > Screen.height - edgeThick && mousePosition.y < Screen.height)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (mousePosition.y > 0 && mousePosition.y < edgeThick)
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }
}
