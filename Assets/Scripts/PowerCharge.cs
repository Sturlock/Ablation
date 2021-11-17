using System;
using System.Collections;
using UnityEngine;

public class PowerCharge : MonoBehaviour
{
    public bool StartingPower;
    [SerializeField] private float currentBatPercentage = 100.00f;
    [SerializeField] private float currentHUDPercentage = 0f;
    public float warmthTimer = 3.00f;
    float amountToDrain = 0.01f;

    float amountToIncrease = 0.5f;

    [SerializeField] private bool reduce;
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
            //IncreaseLight();
            StartCoroutine(StartPowerDraw());
        }
        else
        {
            //DecreaseLights();
        }
        currentBatPercentage = Mathf.Clamp(currentBatPercentage, 0f, 100f);
        currentHUDPercentage = Mathf.Clamp(currentHUDPercentage, 0f, 100f);

        //bar.SetWarmth(currentHUDPercentage);
    }

    private IEnumerator StartPowerDraw()
    {
        Debug.Log("Start Time");
        yield return timer;
        Debug.Log("Time up");
    }

    private void DecreaseLights()
    {
        //If we have at least one cycles worth of power left.
        if (currentBatPercentage >= amountToDrain)
        {
            //Then remove a percentage of the total battery every time we 'call' this function
            currentBatPercentage -= amountToDrain;
            currentHUDPercentage += amountToDrain;

        }
        else
        {
            //Then we assume we are out of power. Reset the battery percentage to 0, turn off the light and then cancel the repetition of this function
            currentBatPercentage = 0.00f;
            CancelInvoke();
        }
        //Debug.Log("The remaining Warmth percentage is " + currentWarmthPercentage + "%");
    }

    private void IncreaseLight()
    {
        //If we have at least one cycles worth of power left.
        if (currentBatPercentage >= amountToDrain)
        {
            //Then remove a percentage of the total battery every time we 'call' this function
            if (currentBatPercentage >= amountToDrain)
            {
                currentBatPercentage += amountToIncrease;
                currentHUDPercentage -= amountToIncrease;
            }
            else
            {
                currentBatPercentage = 0.00f;
                CancelInvoke();
            }
            //Debug.Log("The remaining Warmth percentage is " + currentWarmthPercentage + "%");
        }
    }
}