using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraLook : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float lookSensitivity = 0.2f;

    private CinemachineFreeLook cinemachine;
    private PlayerControl playerInput;
    #endregion


    #region BuiltIn Methods
    private void Awake()
    {
        playerInput = new PlayerControl();
        cinemachine = GetComponent<CinemachineFreeLook>();

    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        Vector2 delta = playerInput.PlayerMain.Look.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += delta.x * 150 * lookSensitivity * Time.deltaTime;
        cinemachine.m_YAxis.Value += delta.y * lookSensitivity * Time.deltaTime;

    }
    #endregion


}
