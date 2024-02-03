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
    [SerializeField] private Image fill;
    [SerializeField] private Sprite rainbow;
    [SerializeField] private Sprite white;

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
        damageText.text = damage + "%";
    }

    private void UpdateUltimateGauge()
    {
        // Assuming player's ultimate gauge is a float between 0 and 1
        ultimateGauge.value = playerReference.ultimateGauge;
        
        // Change the color of the ultimate gauge to rainbow if the gauge is full
        fill.sprite = playerReference.ultimateGauge >= 1 ? rainbow : white;
    }
}
