using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    [SerializeField] private Image fill;
    [SerializeField] private Sprite rainbow;
    [SerializeField] private Sprite white;
    
    public readonly List<Color> damageColors = new List<Color>
    {
        //white
        new Color(1, 1, 1),
        //FFF537
        new Color(1, 0.96f, 0.22f),
        //E6B500
        new Color(0.9f, 0.71f, 0),
        //E68900
        new Color(0.9f, 0.54f, 0),
        //EA0011
        new Color(0.92f, 0, 0.07f),
        //B7000B
        new Color(0.71f, 0, 0.04f)
    };

    public int playerNumber;

    private void Awake()
    {
        fill = ultimateGauge.fillRect.GetComponent<Image>();
    }
    
    public void Start() {
        //from gameManager, get player reference
        playerReference = null;
    }

    // Update is called once per frame
    void Update()
    {
        // if player reference is null, return
        playerReference = GameManager.Instance.GetPlayer(playerNumber);
        if (playerReference is null) return;
        
        // Update the UI elements with the player's current status
        UpdateLifeImages();
        UpdateDamageText();
        UpdateUltimateGauge();
    }

    private void UpdateLifeImages()
    {
        int life = playerNumber == 1 ? GameManager.Instance.player1Life : GameManager.Instance.player2Life;
        
        // Assuming player's life is an integer between 0 and 3
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = i < life;
        }
    }

    private void UpdateDamageText()
    {
        int damage = playerReference.GetStackedDamage();
        Color color = Color.black;
        
        // 0 ~ 19 : white
        if (damage < 20) color = damageColors[0];
        // 20 ~ 39 : FFF537
        else if (damage < 40) color = damageColors[1];
        // 40 ~ 59 : E6B500
        else if (damage < 60) color = damageColors[2];
        // 60 ~ 79 : E68900
        else if (damage < 80) color = damageColors[3];
        // 80 ~ 99 : EA0011
        else if (damage < 100) color = damageColors[4];
        // 100 : B7000B
        else color = damageColors[5];

        damageText.text = damage + "%";
        damageText.color = color;
    }

    private void UpdateUltimateGauge()
    {
        // Assuming player's ultimate gauge is a float between 0 and 1
        ultimateGauge.value = playerReference.ultimateGauge;
        
        // Change the color of the ultimate gauge to rainbow if the gauge is full
        fill.sprite = playerReference.ultimateGauge >= 1 ? rainbow : white;
    }
}
