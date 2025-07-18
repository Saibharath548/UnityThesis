using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/NormalAttack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animOR;
    public float damage;
}
