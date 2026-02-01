using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject[] allObjcetStatGame;
    [HideInInspector] public bool onStart;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(CallStartGame());
    }
    IEnumerator CallStartGame()
    {
        while (!onStart)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerSoidier.Instance.ChangeMode(MaskState.HeavyAttack);
                onStart = true;
            }
            yield return null;
        }
        foreach (GameObject at in allObjcetStatGame)
        {
            at.SetActive(true);
        }
        PlayerContoller.Instance.playerInput.Player.Enable();
        PlayerBase.Instance.StartGame();
        Destroy(this.gameObject);
    }
}
