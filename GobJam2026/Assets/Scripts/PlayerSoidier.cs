using System.Collections;
using UnityEngine;

public class PlayerSoidier : MonoBehaviour
{
    public Rigidbody2D rb;
    public static PlayerSoidier Instance;

    public CheckClose checkClose;
    public Animator anim;
    [SerializeField] EnemyManger enemyManger;
    [SerializeField] public Enemy currentTarget;
    [SerializeField] float currentAtkRange;
    [SerializeField] float distanceEn;
    [Header("Stat")]
    [SerializeField] float currentSpeedMove;
    public bool onAtk;
    public bool canChangeMode = true;
    [Header("Mask")]
    [SerializeField] SpriteRenderer maskObj;
    [SerializeField] MaskProfire[] maskProfires;
    [HideInInspector] public MaskProfire currentMask;

    public Coroutine coFollowPlayer;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        SetFollowTarget();
    }

    IEnumerator FollowTarget()
    {
        anim.SetTrigger("Idel");
        while (currentTarget == null)
        {
            checkClose.FindCloseTarget();
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Walk");
        while (!onAtk)
        {
            if (currentTarget != null)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                currentTarget.transform.position, Time.deltaTime * currentSpeedMove);
                CheckDistan();
            }
            yield return null;
        }
        currentMask.AttackObj.Activate();
        coFollowPlayer = null;
    }

    void CheckDistan()
    {
        distanceEn = Vector2.Distance(transform.position, currentTarget.transform.position);
        onAtk = distanceEn <= currentAtkRange;
    }

    public void SendCloseTargetBase(GameObject _closeTarget)
    {
        currentTarget = _closeTarget.GetComponent<Enemy>();
        SetFollowTarget();
    }

    public void ChangeMode(MaskState _mode)
    {
        if (!canChangeMode) return;
        foreach (MaskProfire mf in maskProfires)
        {
            mf.AttackObj.gameObject.SetActive(false);
        }
        currentMask = FindProfireMask(_mode);
        if (_mode == MaskState.Speedster)
        {
            onAtk = true;
            SetFollowTarget();
            currentMask.AttackObj.Activate();
        }
        maskObj.sprite = currentMask.maskIcon;
        currentSpeedMove = currentMask.speedMove;
        currentAtkRange = currentMask.atkRange;
    }

    public void EndAttack()
    {
        PlayerBase.Instance.cameraSheak.SetUpShakeCamera(false);
        onAtk = false;
        canChangeMode = true;
        SetFollowTarget();
    }

    public void SetFollowTarget()
    {
        if (coFollowPlayer != null)
        {
            StopCoroutine(coFollowPlayer);
            coFollowPlayer = null;
        }
        if (!onAtk)
        {
            coFollowPlayer = StartCoroutine(FollowTarget());
        }
    }

    MaskProfire FindProfireMask(MaskState _mode)
    {
        foreach (MaskProfire mf in maskProfires)
        {
            if (mf.maskState == _mode)
            {
                return mf;
            }
        }
        return default;
    }
}

[System.Serializable]
public class MaskProfire
{
    public MaskState maskState;
    public Sprite maskIcon;
    public float speedMove;
    public float atkRange;
    public Skill AttackObj;
    public float skillCooldown;
    public float attackTime;
}

public enum MaskState
{
    HeavyAttack, Speedster, Knockback, Piercing
}
