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
        if (isDestroyed) return;
        isDestroyed = true;

        Explode();

        Destroy(this.gameObject);

    }

    void Explode()
    {
        Collider2D[] colls = new Collider2D[10];

        Physics2D.OverlapCircle(transform.position, explodeRange, filter, colls);

        foreach(Collider2D coll in colls)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.GetComponent<Player>().TakeDamage(explodeDamage, (coll.transform.position - transform.position).normalized, explodePower);
            }
        }
    }
}
