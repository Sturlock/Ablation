using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Audio : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] SphereCollider sphere;
    public bool play = false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        sphere = gameObject.AddComponent<SphereCollider>();

        sphere.enabled = false;
        sphere.radius = source.maxDistance;
        sphere.isTrigger = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            StartCoroutine(PlaySound());
        }

        if (!play)
        {
            sphere.enabled = false;
        }
    }

    private IEnumerator PlaySound()
    {
        source.Play();
        sphere.enabled = true;
        yield return new WaitForSeconds(.1f);
        play = false;
    }
}
