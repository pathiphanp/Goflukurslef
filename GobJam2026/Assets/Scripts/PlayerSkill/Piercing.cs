using System.Collections;
using UnityEngine;

public class Piercing : Skill
{
    public override void Start()
    {
        base.Start();
    }
    public override IEnumerator TriggerPulse()
    {
        PlayerSoidier.Instance.anim.SetTrigger("Piercing");
        if (PlayerSoidier.Instance.transform.position.x > PlayerSoidier.Instance.currentTarget.transform.position.x)
        {

            PlayerSoidier.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            PlayerSoidier.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        return base.TriggerPulse();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ITakeDatamage>().TakeDaamge(atk);
    }
    IEnumerator CallDodamage(GameObject _newTarget)
    {
        yield return new WaitForSeconds(PlayerSoidier.Instance.currentMask.attackTime);
    }
}
