using UnityEngine;

public class MannequinAnimatorEvents : MonoBehaviour
{
    public MannequinAttack mannequinAttack;

    public void TakeDamage()
    {
        mannequinAttack.TakeDamage();
    }

    public void EndAttack()
    {
        mannequinAttack.EndAttack();
    }
}
