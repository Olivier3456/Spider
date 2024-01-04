using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public Spider spider;

    public float maxGroundAnglesDeltaToContinueWalking = 90f;

    

    public void Travel()
    {
        //Vector3 lastPosition = spider.TriggerTransform.position;

        Vector3 movementDirection = spider.VisualTransform.forward;
        Vector3 movement = movementDirection * spider.TravelSpeed * Time.deltaTime;
        spider.TriggerTransform.position += movement;

        //Vector3 newPosition = spider.TriggerTransform.position;

        //spider.ActualTravelSpeed = Vector3.Distance(lastPosition, newPosition) / Time.deltaTime;
        spider.ActualTravelSpeed = Vector3.Distance(spider.VisualTransform.position, spider.LastVisualTransformPosition) / Time.deltaTime;
    }

       
    private IEnumerator turnCoroutine;
    public void Turn(float angle, float length)
    {
        if (turnCoroutine != null)
        {
            Debug.Log("A spider turn command was already running. Stopping it to perform the new command.");
            StopCoroutine(turnCoroutine);
        }

        turnCoroutine = TurnCoroutine(angle, length);
        StartCoroutine(turnCoroutine);
    }


    private IEnumerator TurnCoroutine(float angle, float length)
    {
        float step = 0;

        while (step < 1)
        {
            float turningRotationAngleForThatFrame = Time.deltaTime * angle;
            spider.VisualTransform.rotation *= Quaternion.AngleAxis(turningRotationAngleForThatFrame, Vector3.up);
            yield return null;
            step += Time.deltaTime / length;
        }

        turnCoroutine = null;
    }


    public void StopTurn()
    {
        if (turnCoroutine != null)
        {
            StopCoroutine(turnCoroutine);
            turnCoroutine = null;
        }
    }


    public bool IsTurning()
    {
        return turnCoroutine != null;
    }
}
