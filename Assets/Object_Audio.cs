using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Audio : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] SphereCollider sphere;
    [SerializeField] BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        sphere = gameObject.AddComponent<SphereCollider>();
        box = GetComponent<BoxCollider>();
        sphere.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
