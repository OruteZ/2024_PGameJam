using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMine : Mine
{
    [Header("Explode Setting")]
    [SerializeField] float explodeRange = 3f;
    [SerializeField] float explodeForce = 10f;
    [SerializeField] float explodeDamage = 10f;

    

    protected override bool ExcuteMine(Player player)
    {
        if (!base.ExcuteMine(player)) return false;

        ContactFilter2D filter = new ContactFilter2D();
        List<Collider2D> colls = new List<Collider2D>();

        Physics2D.OverlapCircle(transform.position, explodeRange, filter, colls);

        foreach(Collider2D coll in colls)
        {
            if (coll.CompareTag("Player"))
            {
                coll.gameObject.GetComponent<Player>().TakeDamage(explodeDamage, (coll.transform.position - transform.position).normalized, explodeForce);
                continue;
            }

            Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.AddForce(explodeForce * (coll.transform.position - transform.position).normalized, ForceMode2D.Impulse);
            }
        }

        DestroyGameObj();

        return true;
    }
}
