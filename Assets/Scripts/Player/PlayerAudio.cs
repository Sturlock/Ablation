 using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioS;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxInSnapshot;
    public AudioMixerSnapshot ambIdleSnapshot;
    public AudioMixerSnapshot ambInSnapshot;

    public LayerMask monsterMask;
    private bool monsterNear;

    public AudioClip[] metalSteps;
    public AudioClip[] woodSteps;
    public AudioClip[] stairsSteps;

    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5f, transform.forward, 0f, monsterMask);
        if (hits.Length > 0)
        {
            auxInSnapshot.TransitionTo(0.5f);
            if (!monsterNear)
            {
                auxInSnapshot.TransitionTo(0.5f);
                monsterNear = true;
            }
            else
            {
                if (monsterNear)
                {
                    idleSnapshot.TransitionTo(0.5f);
                    monsterNear = false;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            auxInSnapshot.TransitionTo(0.5f);
        }
        if (other.CompareTag("Ambience"))
        {
            ambInSnapshot.TransitionTo(0.5f);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            idleSnapshot.TransitionTo(0.5f);
        }
        if (other.CompareTag("Ambience"))
        {
            ambIdleSnapshot.TransitionTo(0.5f);
        }
    }

    public void FootSteps()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        int r = Random.Range(0, 3);
        if (Physics.Raycast(ray, out hit, 1f))
        {
            switch (hit.transform.tag)
            {
                case "WoodFloor":
                    audioS.PlayOneShot(woodSteps[r]);
                    break;

                case "MetalFloor":
                    audioS.PlayOneShot(metalSteps[r]);
                    break;

                case "Stairs":
                    audioS.PlayOneShot(stairsSteps[r]);
                    break;

                default:
                    audioS.PlayOneShot(metalSteps[r]);
                    break;
            }
        }
    }
}