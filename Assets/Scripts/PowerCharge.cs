using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCharge : MonoBehaviour, IInteractable
{
    bool power = false;
    public bool StartingPower;
    public GameObject[] hallways;
    [SerializeField] List<GameObject> lightHouse = new List<GameObject>();

    public List<GameObject> lightObjects = new List<GameObject>();

    [Header("Audio")]
    [SerializeField] private AudioClip dayOn;

    [SerializeField] private AudioClip dayOff;
    [SerializeField] private AudioClip nightOn;
    [SerializeField] private AudioClip nightOff;

    [Header("Power On Lights")]
    public List<GameObject> spotLights = new List<GameObject>();

    public List<GameObject> areaLights = new List<GameObject>();

    [Header("Power Off Lights")]
    public List<GameObject> spotLightsUV = new List<GameObject>();

    public List<GameObject> glassLightsRight = new List<GameObject>();
    public List<GameObject> glassLightsLeft = new List<GameObject>();

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
        for (int i = 0; i < hallways.Length; i++)
        {
            //Gets Light prefab that exists on all corridors
            lightHouse.Add(hallways[i].gameObject.transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < lightHouse.Count; i++)
        {
            lightObjects.Add(lightHouse[i].gameObject.transform.Find("Light Material").gameObject);
            //Finds Day Light
            spotLights.Add(lightHouse[i].gameObject.transform.Find("SpotLightDay").gameObject);
            areaLights.Add(lightHouse[i].gameObject.transform.Find("AreaLightDay").gameObject);
            //Finds Night Light
            spotLightsUV.Add(lightHouse[i].gameObject.transform.Find("SpotLightNight").gameObject);
            glassLightsRight.Add(lightHouse[i].gameObject.transform.Find("AreaLightRNight").gameObject);
            glassLightsLeft.Add(lightHouse[i].gameObject.transform.Find("AreaLightLNight").gameObject);
        }
    }

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
        if (StartingPower && !power)
        {
            StartCoroutine(PoweringLight());
            StartCoroutine(StartPowerDraw());
            StartingPower = false;
            power = true;
        }
    }
    public void Interact(PlayerInteract script)
    {
        StartCoroutine(PoweringLight());
        StartCoroutine(StartPowerDraw());
    }

    public void Action(PlayerInteract script)
    {
        throw new System.NotImplementedException();
    }
    private IEnumerator StartPowerDraw()
    {
        var tim = timer.ToString();
        Debug.Log("Time: " + tim);
        yield return timer;
        Debug.Log("Time up");
        StartCoroutine(UnpoweringLights());
        
        yield break;
    }

    private IEnumerator UnpoweringLights()
    {
        for (int i = hallways.Length - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.2f);

            #region Lights Powering OFF

            lightObjects[i].GetComponent<AudioSource>().clip = dayOff;
            lightObjects[i].GetComponent<AudioSource>().Play();
            spotLights[i].GetComponent<Light>().enabled = false;
            areaLights[i].GetComponent<Light>().enabled = false;
            lightObjects[i].GetComponent<MeshRenderer>().material = lightsOut;

            #endregion Lights Powering OFF

            yield return new WaitForSeconds(0.4f);

            #region UV Powering ON

            lightObjects[i].GetComponent<AudioSource>().clip = nightOn;
            lightObjects[i].GetComponent<AudioSource>().Play();
            spotLightsUV[i].GetComponent<Light>().enabled = true;
            lightObjects[i].GetComponent<MeshRenderer>().material = lightsUV;
            glassLightsRight[i].GetComponent<Light>().enabled = true;
            glassLightsLeft[i].GetComponent<Light>().enabled = true;

            #endregion UV Powering ON

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

            #region UV Powering OFF

            lightObjects[i].GetComponent<AudioSource>().clip = nightOff;
            lightObjects[i].GetComponent<AudioSource>().Play();
            spotLightsUV[i].GetComponent<Light>().enabled = false;
            lightObjects[i].GetComponent<MeshRenderer>().material = lightsOut;
            glassLightsRight[i].GetComponent<Light>().enabled = false;
            glassLightsLeft[i].GetComponent<Light>().enabled = false;

            #endregion UV Powering OFF

            //Waits a second
            yield return new WaitForSeconds(0.2f);
            //Turns on Day Lights and changes light Material

            #region Lights Powering ON

            lightObjects[i].GetComponent<AudioSource>().clip = dayOn;
            lightObjects[i].GetComponent<AudioSource>().Play();
            spotLights[i].GetComponent<Light>().enabled = true;
            areaLights[i].GetComponent<Light>().enabled = true;
            lightObjects[i].GetComponent<MeshRenderer>().material = lightsOn;

            #endregion Lights Powering ON

            yield return new WaitForSeconds(.3f);
        }
        yield break;
    }

    
}