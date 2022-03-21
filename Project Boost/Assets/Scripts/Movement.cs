using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float turningThrust = 90f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem leftEngine;
    [SerializeField] ParticleSystem rightEngine;

    private Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    public void StopRocket()
    {
        OnStopRocketThrust();
        OnStopRocketTurn();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            OnRocketThrust();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            OnStopRocketThrust();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            OnRocketTurn(rightEngine, turningThrust);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            OnRocketTurn(leftEngine, -turningThrust);
        }
        else if (
            Input.GetKeyUp(KeyCode.A) || 
            Input.GetKeyUp(KeyCode.LeftArrow)  || 
            Input.GetKeyUp(KeyCode.RightArrow) || 
            Input.GetKeyUp(KeyCode.D))
        {
            OnStopRocketTurn();
        }
    }


    private void OnRocketThrust()
    {
        rb.AddRelativeForce(Time.deltaTime * mainThrust * Vector3.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine, 0.145f);
        }
        if (!engineParticles.isPlaying)
        {
            engineParticles.Play();
        }
    }

    private void OnRocketTurn(ParticleSystem engineParticle, float turningThrust)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine, 0.07f);
        }
        if (!engineParticle.isPlaying)
        {
            engineParticle.Play();
        }
        RotateObject(turningThrust);
    }

    private void OnStopRocketThrust()
    {
        audioSource.Stop();
        engineParticles.Stop();
    }

    private void OnStopRocketTurn()
    {
        audioSource.Stop();
        rightEngine.Stop();
        leftEngine.Stop();
    }

    private void RotateObject(float rotationPerSecond)
    {
        rb.freezeRotation = true;
        transform.Rotate(Time.deltaTime * rotationPerSecond * Vector3.forward);
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
