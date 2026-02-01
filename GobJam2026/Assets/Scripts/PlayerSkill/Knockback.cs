using System.Collections;
using UnityEngine;

public class Knockback : Skill
{
    [SerializeField] float knockbackPown;
    [SerializeField] float durationKnockback;
    public override void Start()
    {
        base.Start();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerSoidier.Instance.anim.SetTrigger("Knockback");
        StartCoroutine(CallKnockback(collision.gameObject));
    }

    IEnumerator CallKnockback(GameObject _target)
    {
        Vector2 _knockbackDi = -(PlayerBase.Instance.transform.position - _target.transform.position).normalized;
        _target.GetComponent<Rigidbody2D>().AddForce(_knockbackDi * knockbackPown, ForceMode2D.Impulse);
        yield return new WaitForSeconds(durationKnockback);
        _target.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        _target.GetComponent<Rigidbody2D>().Sleep();
        _target.GetComponent<Enemy>().StartMove();
    }
}
