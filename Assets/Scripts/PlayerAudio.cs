using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioS;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxInSnapshot;

    public LayerMask monsterMask;
    private bool monsterNear;

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
        if (other.CompareTag("Jawbreaker"))
        {
            auxInSnapshot.TransitionTo(0.5f);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jawbreaker"))
        {
            idleSnapshot.TransitionTo(0.5f);
        }
    }
}