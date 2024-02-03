using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpriteAnimator : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    
    private Image _image;
    private int _currentIndex;
    private float _timeSinceLastFrame;
    
    public float frameRate = 0.1f;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    private void Update()
    {
        _timeSinceLastFrame += Time.deltaTime;
        if (_timeSinceLastFrame >= frameRate)
        {
            _timeSinceLastFrame = 0;
            _currentIndex = (_currentIndex + 1) % sprites.Count;
            _image.sprite = sprites[_currentIndex];
        }
    }
}
