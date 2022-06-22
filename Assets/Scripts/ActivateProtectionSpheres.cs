using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateProtectionSpheres : MonoBehaviour
{
    [SerializeField] private GameObject[] Spheres;

    private int currentIndex;

   public void ActivateNext()
    {
        if (Spheres.Length <= currentIndex)
        {
            return;
        }
        if(Spheres[currentIndex] == null)
        {
            currentIndex++;
            return;
        }

        Spheres[currentIndex].SetActive(false);

        currentIndex++;

        if (Spheres.Length <= currentIndex)
        {
            return;
        }

        Spheres[currentIndex].SetActive(true);
    }
}
