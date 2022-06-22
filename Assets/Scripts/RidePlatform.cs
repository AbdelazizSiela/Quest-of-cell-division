using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidePlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            transform.parent.SetParent(collision.transform);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            transform.parent.SetParent(null);
        }
    }
}
