using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDatamage
{
    [SerializeField] Animator anim;
    EnemyManger enm;
    [SerializeField] StatData dataStat;
    [SerializeField] BaseStat stat;
    bool onAtk;

    [SerializeField] float delayAttack;
    [Header("Time")]
    [SerializeField] float die;
    public void SetUp(EnemyManger _enm)
    {
        enm = _enm;
        stat = dataStat.data.Clone();
        StartMove();
        // StartCoroutine(CheckGameOnPlay());
    }

    public void StartMove()
    {
        StartCoroutine(MoveToPlayerBase());
    }
    IEnumerator MoveToPlayerBase()
    {
        while (!onAtk)
        {
            if (transform.position.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            transform.position = Vector2.MoveTowards(transform.position,
            enm.plaBase.transform.position, Time.deltaTime * stat.speedMove);
            float distanceEn = Vector2.Distance(transform.position, enm.plaBase.transform.position);
            onAtk = distanceEn <= stat.atkRange;
            yield return null;
        }
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(delayAttack);
        enm.plaBase.TakeDaamge(stat.atk);
        yield return new WaitForSeconds(5);
        StartMove();
    }


    public float TakeDaamge(float _damage)
    {
        stat.hp -= _damage;
        if (stat.hp <= 0)
        {
            //Die
            StartCoroutine(CallDie());
        }
        return stat.hp;
    }

    IEnumerator CallDie()
    {
        yield return new WaitForSeconds(die);
        Destroy(this.gameObject);
    }

    IEnumerator CheckGameOnPlay()
    {
        while (true)
        {
            if (PlayerBase.Instance.allobjectInGame.activeSelf == false)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}

public interface ITakeDatamage
{
    float TakeDaamge(float _damage);
}