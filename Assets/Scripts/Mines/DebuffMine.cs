using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffMine : MonoBehaviour
{
    [SerializeField] float damageAmount = 10f;

    [Header("Debuff Setting")]
    [SerializeField] float frictionAmount = 0.2f;

    [SerializeField, Space(5f)] float debuffTime = 3f;

    PhysicsMaterial2D phMat;

    bool isUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        isUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDebuff(collision.gameObject.GetComponent<Player>());
        }
    }

    void ApplyDebuff(Player player)
    {
        if (isUsed) return;
        isUsed = true;

        player.TakeDamage(damageAmount);

        //이후 디버프 기능 들어가면 디버프 추가
        phMat = player.GetComponent<Rigidbody2D>().sharedMaterial;

        phMat.friction *= frictionAmount;

        Invoke(nameof(UnApplyDebuff), debuffTime);
    }

    void UnApplyDebuff()
    {
        phMat.friction /= frictionAmount;

        Destroy(this.gameObject);
    }
}
