using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightType
{
    Hallway, Bespoke
}

public class LightBlock : MonoBehaviour
{
    public AudioSource _source;
    public LightType _type;
    [Header("Night")]
    public Light _spotLightUV;
    public Light _areaLightUV;
    [Header("Day")]
    public Light _spotLight;
    public Light _areaLight;
    public ReflectionProbe _reflectionProbe;
    public MeshRenderer _renderer;

    public void DaySetActive(AudioClip clip, bool active, Material material)
    {
        if (_source != null)
        { 
            _source.clip = clip;
            _source.Play();
        }
        if (_spotLight != null)
            _spotLight.enabled = active;
        if (_areaLight != null)
            _areaLight.enabled = active;
        if (_renderer != null)
            _renderer.material = material;
        if(_reflectionProbe != null)
            _reflectionProbe.enabled = active;
    }

    public void NightSetActive(AudioClip clip, bool active, Material material)
    {
        if (_source != null)
        {
            _source.clip = clip;
            _source.Play();
        }
        if (_spotLightUV != null)
            _spotLightUV.enabled = active;
        if (_areaLightUV != null)
            _areaLightUV.enabled = active;
        if (_renderer != null)
            _renderer.material = material;
    }
}

