using System;
using System.Collections;
using UnityEngine;

public class PowerCharge : MonoBehaviour
{
    public bool StartingPower;
    public GameObject[] lights;
    [SerializeField] private float currentBatPercentage = 100.00f;
    [SerializeField] private float currentHUDPercentage = 0f;
    public float warmthTimer = 3.00f;
    float amountToDrain = 0.01f;

    float amountToIncrease = 0.5f;

    [SerializeField] private bool reduce;
    [Range(1f, 5400f)]
    public float powerTimer = 60;
    WaitForSeconds timer;

    void Start()
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
        for (int i = lights.Length -1; i > 0; i--)
        {
            lights[i].GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(.3f);
        }
        yield break;
    }

    private IEnumerator IncreaseLight()
    {
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(.3f);
        }
        yield break;
    }
}