using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightType
{
    Hallway, Bespoke
}

public class LightBlock : MonoBehaviour
{
    public AudioSource source;
    public LightType type;
    [Header("Night")]
    public Light spotLightUV;
    public Light areaLightUV;
    [Header("Day")]
    public Light spotLight;
    public Light areaLight;
    public ReflectionProbe reflectionProbe;
    public MeshRenderer renderer;

    public void DaySetActive(AudioClip clip, bool active, Material material)
    {
        if (source != null)
        { 
            source.clip = clip;
            source.Play();
        }
        if (spotLight != null)
            spotLight.enabled = active;
        if (areaLight != null)
            areaLight.enabled = active;
        if (renderer != null)
            renderer.material = material;
        if(reflectionProbe != null)
            reflectionProbe.enabled = active;
    }

    public void NightSetActive(AudioClip clip, bool active, Material material)
    {
        if (source != null)
        {
            source.clip = clip;
            source.Play();
        }
        if (spotLightUV != null)
            spotLightUV.enabled = active;
        if (areaLightUV != null)
            areaLightUV.enabled = active;
        if (renderer != null)
            renderer.material = material;
    }
}

