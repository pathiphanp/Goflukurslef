using System.Collections;
using UnityEngine;

public class CameraSheak : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] private bool onShake;
    [Header("Camera Boundary")]
    [SerializeField] private float shakePower;
    [SerializeField] float speedShake;
    Vector3 startCamPos;


    private void Start()
    {
        playerCamera = GetComponent<Camera>();
        startCamPos = playerCamera.transform.localPosition;
    }

    IEnumerator CallShake()
    {
        Vector3 currentCamPos = startCamPos;
        while (onShake)
        {
            float randomX = Random.Range(currentCamPos.x - shakePower, currentCamPos.x + shakePower);
            float randomZ = Random.Range(currentCamPos.z - shakePower, currentCamPos.z + shakePower);

            Vector3 newPostion = new Vector3(randomX, startCamPos.y, randomZ);
            playerCamera.transform.localPosition = Vector3.MoveTowards(playerCamera.transform.localPosition, newPostion, Time.deltaTime * speedShake);
            yield return null;
        }
        playerCamera.transform.localPosition = startCamPos;
    }

    public void SetUpShakeCamera(bool _input)
    {
        onShake = _input;
        if (_input)
        {
            StartCoroutine(CallShake());
        }
    }
}
