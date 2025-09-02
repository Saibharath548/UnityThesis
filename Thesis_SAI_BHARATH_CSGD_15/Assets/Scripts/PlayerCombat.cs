using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;

    RuntimeAnimatorController baseController;

    Animator anim;
    public Weapon weapon;
    private void Start()
    {
        anim = GetComponent<Animator>();
        baseController = anim.runtimeAnimatorController; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        ExitAttack();
    }
    void Attack()
    {
        weapon.canDamage = true;
        Debug.Log("attacking");
        if (Time.time - lastComboEnd > 0.5f && comboCounter < combo.Count)
        {
            CancelInvoke("EndCombo");
            if (Time.time - lastClickedTime >= 0.2f)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animOR;
                anim.Play("Attack", 0, 0);
                weapon.Damage = combo[comboCounter].damage;
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }
    void ExitAttack()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        if (state.IsTag("Attack") && state.normalizedTime >= 0.9f && state.normalizedTime < 1f)
        {
            if (!IsInvoking("EndCombo"))
            {
                weapon.canDamage = false;
                Invoke("EndCombo", 0.3f);
            }
        }
    }
    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;

        anim.runtimeAnimatorController = baseController;
    }
}
