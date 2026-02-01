using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{
    public static EnemyManger Instance;
    public PlayerBase plaBase;
    [SerializeField] List<GameObject> allEnemy = new List<GameObject>();

    [Header("Spawn Position")]
    [SerializeField] positionSpawn[] positionSpawn;

    [Header("Time Corot")]
    public static readonly WaitForSeconds spawnEnemy = new WaitForSeconds(0.2f);

    [SerializeField] GameObject allobjNode;
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

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        foreach (GameObject ae in allEnemy)
        {
            Enemy _newEn = Instantiate(ae, RandomSpawn(), ae.transform.localRotation, allobjNode.transform).GetComponent<Enemy>();
            _newEn.SetUp(this);
            yield return spawnEnemy;
        }
        yield return new WaitForSeconds(10f);
        StartCoroutine(SpawnEnemy());
    }

    public Vector2 RandomSpawn()
    {
        int RndSpawnZone = Random.Range(0, 4);
        float rndX = 0;
        float rndY = 0;
        switch (positionSpawn[RndSpawnZone].typeRnd)
        {
            case RndPoint.X:
                rndX = Random.Range(positionSpawn[RndSpawnZone].positionSpawnSt.transform.position.x
                , positionSpawn[RndSpawnZone].positionSpawnEnd.transform.position.x);
                rndY = positionSpawn[RndSpawnZone].positionSpawnSt.transform.position.y;
                break;
            case RndPoint.Y:
                rndX = positionSpawn[RndSpawnZone].positionSpawnSt.transform.position.x;
                rndY = Random.Range(positionSpawn[RndSpawnZone].positionSpawnSt.transform.position.y,
                positionSpawn[RndSpawnZone].positionSpawnEnd.transform.position.y);
                break;
        }
        Vector2 _newPosiSpawn = new Vector2(rndX, rndY);
        return _newPosiSpawn;
    }
}

public enum RndPoint
{
    X, Y
}
[System.Serializable]
public class positionSpawn
{
    public RndPoint typeRnd;
    public GameObject positionSpawnSt;
    public GameObject positionSpawnEnd;
}
