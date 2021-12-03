using System;
using System.Collections;
using UnityEngine;

public class PowerCharge : MonoBehaviour
{
    public bool StartingPower;
    public GameObject[] spotLights;
    public GameObject[] areaLights;

    [Range(1f, 5400f)]
    public float powerTimer = 60;
    WaitForSeconds timer;

    public void Start()
    {
         timer = new WaitForSeconds(powerTimer);
    }
    public bool GeneratorBool
    {
        get => StartingPower;
        set => StartingPower = value;
    }
    // Update is called once per frame
    private void Update()
    {
        if (StartingPower)
        {
            StartCoroutine(IncreaseLight());
            StartCoroutine(StartPowerDraw());
            StartingPower = false;
        }
    }

    private IEnumerator StartPowerDraw()
    {
        var tim = timer.ToString();
        Debug.Log("Time: " + tim);
        yield return timer;
        Debug.Log("Time up");
        StartCoroutine(DecreaseLights());
        yield break;
    }

    private IEnumerator DecreaseLights()
    {
        for (int i = spotLights.Length -1; i > 0; i--)
        {
            spotLights[i].GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(.3f);
        }
        yield break;
    }

    private IEnumerator IncreaseLight()
    {
        for(int i = 0; i < spotLights.Length; i++)
        {
            spotLights[i].GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(.3f);
        }
        yield break;
    }
}