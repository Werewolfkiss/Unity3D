using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float timeToWait = 3f;
    bool dropped = false;
    private MeshRenderer meshRenderer;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer.enabled = false;
        }
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dropped && Time.time >= timeToWait)
        {
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            
            if (rigidBody != null)
            {
                rigidBody.useGravity = true;
            }
            dropped = true;
        }
    }
}
