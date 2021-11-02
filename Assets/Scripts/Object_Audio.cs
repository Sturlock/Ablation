using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Audio : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] SphereCollider sphere;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        sphere = gameObject.AddComponent<SphereCollider>();

        sphere.enabled = false;
        sphere.isTrigger = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
