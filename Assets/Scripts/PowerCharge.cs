using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCharge : MonoBehaviour, IInteractable
{
    public bool power;
    public GameObject[] hallways;
    public GameObject[] bespokeRooms;
    List<LightBlock> lightBlocks = new List<LightBlock>();

    [Header("Audio")]
    [SerializeField] private AudioClip dayOn;
    [SerializeField] private AudioClip dayOff;
    [SerializeField] private AudioClip nightOn;
    [SerializeField] private AudioClip nightOff;

    [Header("Light Materials")]
    public Material lightsOut;
    public Material lightsUV;
    public Material lightsOn;

    [Header("Generator Time")]
    [Range(1f, 5400f)]
    public float powerTimer = 60;

    private WaitForSeconds timer;

    void Awake()
    {
        if (bespokeRooms != null)
        {
            for (int i = 0; i < bespokeRooms.Length; i++)
            {
                lightBlocks.Add(hallways[i].GetComponentInChildren<LightBlock>());
            }  
        }
        for (int i = 0; i < hallways.Length; i++)
        {
            //Gets Light prefab that exists on all corridors
            lightBlocks.Add(hallways[i].GetComponentInChildren<LightBlock>());
        }
        
    }

    void Start()
    {
        timer = new WaitForSeconds(powerTimer);
    }

    public bool GeneratorBool
    {
        get => power;
        set => power = value;
    }

    public void Interact(PlayerInteract script)
    {
        StopAllCoroutines();
        StartCoroutine(PoweringLight());
        StartCoroutine(StartPowerDraw());
        power = true;
    }

    public void Action(PlayerInteract script)
    {
        StopAllCoroutines();
        StartCoroutine(UnpoweringLights());
    }
    private IEnumerator StartPowerDraw()
    {
        var tim = timer.ToString();
        Debug.Log("Time: " + tim);
        yield return timer;
        Debug.Log("Time up");
        power = true;
        StartCoroutine(UnpoweringLights());
        
        yield break;
    }

    private IEnumerator UnpoweringLights()
    {
        for (int i = hallways.Length - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.2f);

            lightBlocks[i].DaySetActive(dayOff, false, lightsOut);

            yield return new WaitForSeconds(0.4f);

            lightBlocks[i].NightSetActive(nightOn, true, lightsUV);

            yield return new WaitForSeconds(0.3f);
        }
        power = false;
        yield break;
    }

    private IEnumerator PoweringLight()
    {
        for (int i = 0; i < hallways.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            //Turns off Night Lights and changes light Material
            lightBlocks[i].NightSetActive(nightOff, false, lightsOut);

            //Waits a second
            yield return new WaitForSeconds(0.2f);

            lightBlocks[i].DaySetActive(dayOn, true, lightsOn);

            yield return new WaitForSeconds(.3f);
        }
        yield break;
    }

    
}