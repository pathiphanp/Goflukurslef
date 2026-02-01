using System.Collections;
using UnityEngine;

public class CheckClose : MonoBehaviour
{
    Collider2D col;
    [SerializeField] PlayerBase plaBase;
    void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        plaBase.CheckCloseTarget(collision.gameObject);
    }

    public void FindCloseTarget()
    {
        StartCoroutine(TriggerPulse());
    }
    IEnumerator TriggerPulse()
    {
        col.enabled = true;
        yield return new WaitForFixedUpdate();
        col.enabled = false;
    }
}
