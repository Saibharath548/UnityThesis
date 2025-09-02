using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addCombo : MonoBehaviour
{
    public AttackSO anyAttack;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerCombat>();
        if(player != null )
        {
            player.combo.Add(anyAttack);
            Destroy(gameObject);
        }
    }
}
