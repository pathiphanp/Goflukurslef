using System.Collections;
using UnityEngine;

public class Speedster : Skill
{
    [SerializeField] float dashPown;
    [SerializeField] float durationDash;
    public override void Start()
    {
        base.Start();
    }

    void OnDisable()
    {
        PlayerSoidier.Instance.rb.linearVelocity = Vector2.zero;
        PlayerSoidier.Instance.rb.Sleep();
        PlayerSoidier.Instance.EndAttack();
    }
    public override IEnumerator TriggerPulse()
    {
        PlayerSoidier.Instance.anim.SetTrigger("Speedster");
        col.enabled = true;
        PlayerSoidier.Instance.checkClose.FindCloseTarget();
        yield return new WaitForFixedUpdate();
        Vector2 _knockbackDi = -(PlayerSoidier.Instance.transform.position -
        PlayerSoidier.Instance.currentTarget.transform.position).normalized;
        PlayerSoidier.Instance.rb.AddForce(_knockbackDi * dashPown, ForceMode2D.Impulse);
        yield return new WaitForSeconds(durationDash);
        PlayerSoidier.Instance.rb.linearVelocity = Vector2.zero;
        PlayerSoidier.Instance.rb.Sleep();
        StartCoroutine(TriggerPulse());
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ITakeDatamage>().TakeDaamge(atk);
    }

}
