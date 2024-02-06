using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //GameManager.Instance.Dead(other.GetComponent<Player>().playerNumber);
            return;
        }
        
        if (!other.CompareTag("DontDead"))
            Destroy(other.gameObject);
    }
}
