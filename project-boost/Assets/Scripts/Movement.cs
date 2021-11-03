using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem booster;
    // Start is called before the first frame update.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!booster.isEmitting)
        {
            booster.Play();
        }
    }
    
        private void StopThrusting()
    {
        audioSource.Stop();
        booster.Stop();
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RightThrusting();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            LeftThrusting();
        }
        else
        {
            StopSideThrusting();
        }
    }

    private void LeftThrusting()
    {
        AplyRotation(-rotationThrust);
        if (!leftThruster.isEmitting)
        {
            leftThruster.Play();
        }
    }

    private void RightThrusting()
    {
        AplyRotation(rotationThrust);
        if (!rightThruster.isEmitting)
        {
            rightThruster.Play();
        }
    }

    private void StopSideThrusting()
    {
        rightThruster.Stop();
        leftThruster.Stop();
    }

    void AplyRotation(float rotationForce)
    {
        rb.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze rotation so physics can take over
    }
}
