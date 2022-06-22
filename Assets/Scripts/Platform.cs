using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Speed")]

    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private float zSpeed;

    [Space(1)]

    [Header("Positions")]

    [SerializeField] private float xNewPos;
    [SerializeField] private float yNewPos;
    [SerializeField] private float zNewPos;

    private Vector3 originalPos;
    private bool invertPosX, invertPosY, invertPosZ;

    private void Awake()
    {
        originalPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.right * xSpeed * Time.deltaTime);
        transform.Translate(transform.up * ySpeed * Time.deltaTime);
        transform.Translate(transform.forward * zSpeed * Time.deltaTime);

        if(transform.position.x >= originalPos.x + xNewPos && !invertPosX)
        {
            xSpeed = -xSpeed;
            invertPosX = true;
        }
        if (transform.position.x <= originalPos.x - xNewPos && invertPosX)
        {
            xSpeed = -xSpeed;
            invertPosX = false;
        }

        if (transform.position.y >= originalPos.y + yNewPos && !invertPosY)
        {
            ySpeed = -ySpeed;
            invertPosY = true;
        }
        if (transform.position.y <= originalPos.y - yNewPos && invertPosY)
        {
            ySpeed = -ySpeed;
            invertPosY = false;
        }

        if (transform.position.z >= originalPos.z + zNewPos && !invertPosZ)
        {
            zSpeed = -zSpeed;
            invertPosZ = true;
        }
        if (transform.position.z <= originalPos.z - zNewPos && invertPosZ)
        {
            zSpeed = -zSpeed;
            invertPosZ = false;
        }
    }
}
