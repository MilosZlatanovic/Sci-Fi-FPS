using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    private float _lookSpeed = 3;
    void Start()
    {

    }
    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * _lookSpeed;
        transform.localEulerAngles = newRotation;

        /* transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y +
            (_mouseX * _lookSpeed), transform.localEulerAngles.z);*/
    }
}
