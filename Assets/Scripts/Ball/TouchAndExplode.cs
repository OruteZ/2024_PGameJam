using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAndExplode : MonoBehaviour
{
    [SerializeField] float explodeRange = 2f;

    [SerializeField, Space(5f)] float explodeDamage = 10f;

    [SerializeField, Space(5f)] float explodePower = 10f;

    bool isDestroyed = false;

    ContactFilter2D filter;

    // Start is called before the first frame update
    void Start()
    {
        isDestroyed = false;

        filter = new ContactFilter2D();
        //filter.SetLayerMask()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;//충돌 가능 오브젝트들과 만 충돌하기

        if (isDestroyed) return;
        isDestroyed = true;

        Explode();

        DestroyEffectCreator effect = GetComponent<DestroyEffectCreator>();
        if (effect) effect.CreateDestroyEffect();
        
        Destroy(this.gameObject);

    }

    void Explode()
    {
        List<Collider2D> colls = new List<Collider2D>();

        Physics2D.OverlapCircle(transform.position, explodeRange, filter, colls);

        //Debug.Log(colls.Length);

        foreach(Collider2D coll in colls)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.GetComponent<Player>().TakeDamage(explodeDamage, (coll.transform.position - transform.position).normalized, explodePower);
                continue;
            }

            Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.AddForce(explodePower * (coll.transform.position - transform.position).normalized, ForceMode2D.Impulse);
            }

        }
    }
}
