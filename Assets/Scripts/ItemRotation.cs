using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    [SerializeField] private float rotSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0,0, rotSpeed * Time.deltaTime);
    }
}
