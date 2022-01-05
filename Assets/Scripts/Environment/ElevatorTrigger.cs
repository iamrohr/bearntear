using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    GameObject elevator;

    Animator elevatorAnimator;

    private void Start()
    {
        elevator = GameObject.FindGameObjectWithTag("Elevator");
        elevatorAnimator = elevator.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorAnimator.SetTrigger("Open Doors");
        }
    }
}
