using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    Rigidbody rb;
    AudioSource audioSource;
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
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
            
        } else {
            audioSource.Stop();
        }

        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            AplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            AplyRotation(-rotationThrust);
        }
    }

    void AplyRotation(float rotationForce)
    {
        rb.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze rotation so physics can take over
    }
}
