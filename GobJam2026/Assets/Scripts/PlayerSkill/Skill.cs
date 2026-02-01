using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour, ISkill
{
    protected Collider2D col;
    [SerializeField] protected float atk;
    public virtual void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
        this.gameObject.SetActive(false);
    }
    public virtual void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(TriggerPulse());
    }
    public virtual IEnumerator TriggerPulse()
    {
        PlayerSoidier.Instance.canChangeMode = false;
        col.enabled = true;
        yield return new WaitForFixedUpdate();
        col.enabled = false;
        yield return new WaitForSeconds(PlayerSoidier.Instance.currentMask.skillCooldown);
        gameObject.SetActive(false);
        PlayerSoidier.Instance.EndAttack();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }
}

public interface ISkill
{
    void Activate();
}
