using UnityEngine;
using UnityEngine.InputSystem;
using IPlayerInput;
using static IPlayerInput.IPlayerIInput;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour, IPlayerActions
{
    public static PlayerContoller Instance;
    public IPlayerIInput playerInput;
    [SerializeField] PlayerBase playerBase;
    [SerializeField] PlayerSoidier playerSoidier;

    [SerializeField] Button btnChangeMaskHeavyAttack;
    [SerializeField] Button btnChangeMaskSpeedster;
    [SerializeField] Button btnChangeMaskKnockback;
    [SerializeField] Button btnChangeMaskPiercing;

    [SerializeField] StartGame stg;
    void Awake()
    {
        playerInput = new IPlayerIInput();
        playerInput.Player.SetCallbacks(this);
        Instance = this;

        btnChangeMaskHeavyAttack.onClick.AddListener(() => BtnChange(btnChangeMaskHeavyAttack.gameObject, MaskState.HeavyAttack));
        btnChangeMaskSpeedster.onClick.AddListener(() => BtnChange(btnChangeMaskSpeedster.gameObject, MaskState.Speedster));
        btnChangeMaskKnockback.onClick.AddListener(() => BtnChange(btnChangeMaskKnockback.gameObject, MaskState.Knockback));
        btnChangeMaskPiercing.onClick.AddListener(() => BtnChange(btnChangeMaskPiercing.gameObject, MaskState.Piercing));
    }

    public void OnQ(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerSoidier.ChangeMode(MaskState.HeavyAttack);
        }
    }
    public void OnW(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerSoidier.ChangeMode(MaskState.Speedster);
        }
    }

    public void OnE(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerSoidier.ChangeMode(MaskState.Knockback);
        }
    }

    public void OnR(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerSoidier.ChangeMode(MaskState.Piercing);
        }
    }

    void BtnChange(GameObject _btn, MaskState _mode)
    {
        if (stg != null)
        {
            stg.onStart = true;
        }
        playerSoidier.ChangeMode(_mode);
        if (!PlayerSoidier.Instance.canChangeMode) return;
        btnChangeMaskHeavyAttack.GetComponent<Image>().color = Color.black;
        btnChangeMaskSpeedster.GetComponent<Image>().color = Color.black;
        btnChangeMaskKnockback.GetComponent<Image>().color = Color.black;
        btnChangeMaskPiercing.GetComponent<Image>().color = Color.black;
        _btn.GetComponent<Image>().color = Color.white;
    }
}
