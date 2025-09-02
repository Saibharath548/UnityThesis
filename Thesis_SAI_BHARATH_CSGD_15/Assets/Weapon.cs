using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage;
    public bool canDamage;

    BoxCollider TriggerBox;

    private void Start()
    {
        TriggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Enemy>();
        if (canDamage)
        {
            if (player != null)
            {
                player.Health -= Damage;
                if (player.Health <= 0)
                {
                    Destroy(player.gameObject);
                }
            }
        }
    }

    public void EnableTriggerBox()
    {
        TriggerBox.enabled = true;
    }
    public void DisableTriggerBox()
    {
        TriggerBox.enabled = false;
    }
}
