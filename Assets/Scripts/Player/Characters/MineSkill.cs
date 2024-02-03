using UnityEngine;

public class MineSkill : MonoBehaviour
{
    private Player _player = null;
    private bool detectTrigger = false;

    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;
    
    public void SetPlayer(Player player)
    {
        _player = player;
        detectTrigger = true;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!detectTrigger) return;
        
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != _player)
            {
                //calculate knockback direction
                bool isRight = transform.position.x < player.transform.position.x;
                
                player.TakeDamage(damage, new Vector2( isRight ? 1 : -1 , 2), knockbackPower);
                Destroy(gameObject);
            }
        }
    }
}
