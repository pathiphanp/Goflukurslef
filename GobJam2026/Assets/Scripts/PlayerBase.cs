using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerBase : MonoBehaviour, ITakeDatamage
{
    public static PlayerBase Instance;
    public CameraSheak cameraSheak;
    GameObject enemyClose;

    [SerializeField] public GameObject allobjectInGame;
    [SerializeField] TMP_Text timeShow;
    [SerializeField] GameObject uiWin;
    [SerializeField] GameObject uiLose;

    [SerializeField] float timeGame;

    [Header("Stat")]
    public float hp;
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
    }

    public void CheckCloseTarget(GameObject _enemy)
    {
        if (enemyClose == null)
        {
            UpdateNewTarget(_enemy);
        }
        float _pointNewTarget = Vector2.Distance(transform.position, _enemy.transform.position);
        float _pointcurrentTarget = Vector2.Distance(transform.position, enemyClose.transform.position);
        if (_pointcurrentTarget < _pointNewTarget) return;
        UpdateNewTarget(_enemy);
    }

    private void UpdateNewTarget(GameObject _target)
    {
        enemyClose = _target;
        PlayerSoidier.Instance.SendCloseTargetBase(enemyClose);
    }

    public float TakeDaamge(float _damage)
    {
        hp -= _damage;
        if (hp <= 0)
        {
            StopAllCoroutines();
            allobjectInGame.SetActive(false);
            uiLose.SetActive(true);
        }
        return hp;
    }
    public void StartGame()
    {
        StartCoroutine(CountEndGame());
    }
    IEnumerator CountEndGame()
    {
        while (timeGame > 0)
        {
            timeGame -= Time.deltaTime;
            timeShow.text = timeGame.ToString("0");
            yield return null;
        }
        allobjectInGame.SetActive(false);
        uiWin.SetActive(true);
    }
}
