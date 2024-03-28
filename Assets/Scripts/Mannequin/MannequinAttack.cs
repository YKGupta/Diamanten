using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(MannequinChase))]
public class MannequinAttack : MonoBehaviour
{
    [Range(0f, 10f)]
    public float attackRange = 2f;
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
            
        animator.SetBool("isAttacking", true);
        isAttacking = true;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage()
    {
        playerHealth.TakeDamage();
    }

    private bool isAttackable()
    {
        return mannequinChase.isChasing && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= attackRange;
    }
}
