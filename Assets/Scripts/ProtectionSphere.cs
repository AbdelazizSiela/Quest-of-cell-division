using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionSphere : MonoBehaviour
{
    [SerializeField] private Transform origin;

    private Collider otherCol;

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.GetComponent<CharacterController>().enabled = false;
            otherCol = other;
        }
    }

    private void Update()
    {
        if(otherCol == null || otherCol.transform.GetComponent<CharacterController>().enabled == true)
        {
            return;
        }

        otherCol.transform.position = origin.position;
        otherCol.transform.GetComponent<CharacterController>().enabled = true;
    }

}
