using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtThrowObj : MonoBehaviour
{ 
    
    [SerializeField] private float moveTime;
    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;

    private float _facing;
    
    // Start : Start Move Coroutine
    public void Start()
    {
        StartCoroutine(MoveCoroutine());
    }
    
    // MoveCoroutine : Move 5m forward
    private IEnumerator MoveCoroutine()
    {
        var startPosition = transform.position;
        var targetPosition = startPosition + new Vector3(5, 0, 0) * _facing;
        var currentTime = 0f;
        
        while (currentTime < moveTime)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, currentTime / moveTime);
            yield return null;
        }
        
        Destroy(gameObject);
    }
    
    public void SetOwner(Player player, int facingRight)
    {
        _usingPlayer = player;
        _facing = facingRight;
    }
    
    private Player _usingPlayer;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != _usingPlayer)
            {
                player.TakeDamage(damage, new Vector2(_facing, 1), knockbackPower);
                Destroy(gameObject);
            }
        }
    }
}
