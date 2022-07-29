using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
   private int healthAmount = 100;

   [SerializeField] private Image healthBar;
   [SerializeField] private TextMeshProUGUI healthText;

    public void TakeDamage(int damage)
   {
        healthAmount -= damage;

        healthBar.fillAmount = healthAmount / 100f;
        healthText.text = healthAmount.ToString("");

        if (healthAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
   }
}
