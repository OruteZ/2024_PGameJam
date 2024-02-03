using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private Player playerReference;
    [SerializeField] private Image[] lifeImages;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private Slider ultimateGauge;

    // Update is called once per frame
    void Update()
    {
        // Update the UI elements with the player's current status
        UpdateLifeImages();
        UpdateDamageText();
        UpdateUltimateGauge();
    }

    private void UpdateLifeImages()
    {
        // Assuming player's life is an integer between 0 and 3
        for (int i = 0; i < lifeImages.Length; i++)
        {
            // lifeImages[i].enabled = i < playerReference.Life;
        }
    }

    private void UpdateDamageText()
    {
        // Assuming player's damage is a float
        damageText.text = playerReference.GetStackedDamage().ToString();
    }

    private void UpdateUltimateGauge()
    {
        // Assuming player's ultimate gauge is a float between 0 and 1
        ultimateGauge.value = playerReference.UltimateGauge;
    }
}
