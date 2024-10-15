using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(MannequinChase))]
public class MannequinAttack : MonoBehaviour
{
    public Animator animator;
    public PlayerHealth playerHealth;

    [ReadOnly]
    public bool isAttacking;

    private MannequinChase mannequinChase;

    private void Start()
    {
        mannequinChase = GetComponent<MannequinChase>();
        isAttacking = false;
    }

    private void Update()
    {
        if(!isAttackable())
            return;

        Attack();        
    }

    private void Attack()
    {
        if(isAttacking)
            return;
            
        animator.SetTrigger("isAttacking");
        isAttacking = true;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage()
    {
        if(!isAttackable())
            return;
            
        playerHealth.TakeDamage();
    }

    private bool isAttackable()
    {
        return mannequinChase.isChasing && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= Constants.instance.MANNEQUIN_ATTACK_RANGE;
    }
}
