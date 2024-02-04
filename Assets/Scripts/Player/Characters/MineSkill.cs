using UnityEngine;
using System.Collections.Generic;

public class MineSkill : MonoBehaviour
{
    private Player _player = null;
    private bool detectTrigger = false;

    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;
    [SerializeField] private GameObject explosionEffect;

    bool isStopped = false;
    
    public void SetPlayer(Player player)
    {
        _player = player;
        detectTrigger = true;

        isStopped = false;
    }
    
    void StopPosition()
    {
        if (isStopped) return;
        isStopped = true;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;

        GetComponent<CircleCollider2D>().radius = 1f;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!detectTrigger) return;

        if (1 << other.gameObject.layer == LayerMask.GetMask("Ground")) StopPosition();

        //Debug.Log(other.gameObject.layer + " " + LayerMask.GetMask("Ground"));

        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player is P1)
            {
                //calculate knockback direction
                bool isRight = transform.position.x < player.transform.position.x;
                
                player.TakeDamage(damage, new Vector2( isRight ? 1 : -1 , 2), knockbackPower);

                ContactFilter2D filter = new ContactFilter2D();
                List<Collider2D> colls = new List<Collider2D>();

                Physics2D.OverlapCircle(transform.position, 5f, filter, colls);

                foreach (Collider2D coll in colls)
                {
                    if (coll.CompareTag("Player"))
                    {
                        continue;
                    }

                    Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
                    if (rb)
                    {
                        rb.AddForce(10f * (coll.transform.position - transform.position).normalized, ForceMode2D.Impulse);
                    }
                }

                Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
