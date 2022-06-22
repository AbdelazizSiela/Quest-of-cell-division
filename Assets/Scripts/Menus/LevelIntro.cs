using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
    [SerializeField] private StarterAssets.ThirdPersonController thirdPersonScript;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.5f);

        thirdPersonScript.enabled = true;

        yield return new WaitForSeconds(.5f);

        gameObject.SetActive(false);
    }
}
