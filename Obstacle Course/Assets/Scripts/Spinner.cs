using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float xAngle = 0;
    [SerializeField] private float yAngle = 100f;
    [SerializeField] private float zAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime* xAngle, Time.deltaTime * yAngle, Time.deltaTime * zAngle);
    }
}
