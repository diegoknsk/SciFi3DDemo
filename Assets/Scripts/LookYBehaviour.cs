using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookYBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mathf.Clamp(5000, 30, 200));
        //Debug.Log(Mathf.Clamp(0, 30, 200));
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x -= mouseY;
        transform.localEulerAngles = newRotation;

    }
}
