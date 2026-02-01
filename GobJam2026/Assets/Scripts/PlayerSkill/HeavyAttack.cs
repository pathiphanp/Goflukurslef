using System.Collections;
using UnityEngine;

public class HeavyAttack : Skill
{
    public override void Start()
    {
        base.Start();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerSoidier.Instance.anim.SetTrigger("HeavyAttack");
        StartCoroutine(CallDodamage(collision.gameObject));
    }

    IEnumerator CallDodamage(GameObject _newTarget)
    {
        yield return new WaitForSeconds(PlayerSoidier.Instance.currentMask.attackTime);
        _newTarget.GetComponent<ITakeDatamage>().TakeDaamge(atk);
        PlayerBase.Instance.cameraSheak.SetUpShakeCamera(true);
        yield return new WaitForSeconds(0.20f);
        PlayerBase.Instance.cameraSheak.SetUpShakeCamera(false);
    }
}
