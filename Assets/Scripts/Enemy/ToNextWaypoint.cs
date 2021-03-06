using System.Collections;
using UnityEngine;
[HideInInspector]
public class ToNextWaypoint : MonoBehaviour
{
    private CharacterAI characterAI;

    [Header("Info")]
    public int currentWaypoint;

    private bool goingUp = true;

    // Start is called before the first frame update
    private void OnEnable()
    {
        characterAI = GetComponent<CharacterAI>();
    }

    // Update is called once per frame
    public void MovetoWaypoint()
    {
        if (characterAI.Waypoints.Count == 0) return;

        if (characterAI.UsingWaypoint)
        {
            if (characterAI.Waypoints == null) return;

            if (characterAI.SelectRandomWaypoint)
            {
                currentWaypoint = Random.Range(0, characterAI.Waypoints.Count);
                characterAI.Destination = characterAI.GetWaypointPosition(currentWaypoint);
                characterAI.CurrentWaypoint = currentWaypoint;
                return;
            }

            if (characterAI.LoopWaypoint)
            {
                if (currentWaypoint >= characterAI.Waypoints.Count)
                    currentWaypoint = 0;
            }
            else
            {
                if (currentWaypoint >= characterAI.Waypoints.Count)
                {
                    goingUp = false;
                    currentWaypoint = characterAI.Waypoints.Count - 2;
                }

                if (currentWaypoint <= 0 && !goingUp)
                {
                    goingUp = true;
                    currentWaypoint = 0;
                }
            }

            characterAI.Destination = characterAI.GetWaypointPosition(currentWaypoint);
            characterAI.CurrentWaypoint = currentWaypoint;

            if (characterAI.LoopWaypoint)
            {
                currentWaypoint++;
            }
            else
            {
                if (goingUp) currentWaypoint++;
                else currentWaypoint--;
            }
        }
    }
}