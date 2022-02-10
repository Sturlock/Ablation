using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : MonoBehaviour
{
    public AudioSource source;
    public Light spotLightUV;
    public Light glassLightRight;
    public Light glassLightLeft;

    public Light spotLight;
    public Light areaLight;
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
        if (glassLightRight != null)
            glassLightRight.enabled = active;
        if (glassLightLeft != null)
            glassLightLeft.enabled = active;
        if (renderer != null)
            renderer.material = material;
    }
}

