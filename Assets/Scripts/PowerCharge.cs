using System.Collections;
using UnityEngine;

public class PowerCharge : MonoBehaviour
{
    public bool StartingPower;
    public GameObject[] lightObjects;

    [Header("Audio")]
    [SerializeField] private AudioClip dayOn;

    [SerializeField] private AudioClip dayOff;
    [SerializeField] private AudioClip nightOn;
    [SerializeField] private AudioClip nightOff;

    [Header("Power On Lights")]
    public GameObject[] spotLights;

    public GameObject[] areaLights;

    [Header("Power Off Lights")]
    public GameObject[] spotLightsUV;

    public GameObject[] glassLightsRight;
    public GameObject[] glassLightsLeft;

    [Header("Light Materials")]
    public Material lightsOut;

    public Material lightsUV;
    public Material lightsOn;

    [Header("Generator Time")]
    [Range(1f, 5400f)]
    public float powerTimer = 60;

    private WaitForSeconds timer;

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
            StartCoroutine(PoweringLight());
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
        StartCoroutine(UnpoweringLights());
        yield break;
    }

    private IEnumerator UnpoweringLights()
    {
        for (int i = spotLights.Length - 1; i >= 0; i--)
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
        yield break;
    }

    private IEnumerator PoweringLight()
    {
        for (int i = 0; i < spotLights.Length; i++)
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