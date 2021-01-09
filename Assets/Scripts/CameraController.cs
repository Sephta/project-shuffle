using UnityEngine;
using NaughtyAttributes;
using MilkShake;


public class CameraController : MonoBehaviour
{
    // EVENT CHANNELS
    [Foldout("Event Channels")] public ShakePresetEventChannelSO CameraShakeEvent;

#region Debug Data
#if UNITY_EDITOR
    private bool ShowDebugData = false;
#endif
    [SerializeField, ReadOnly, ShowIf("ShowDebugData")] private Vector2 mousePos = Vector2.zero;
    [SerializeField, ReadOnly, ShowIf("ShowDebugData")] private Vector3 targetPos = Vector3.zero;
    [SerializeField, ReadOnly, ShowIf("ShowDebugData")] private Vector3 refVel = Vector3.zero;
#endregion

    [Header("Dependencies")]
    [Required] public Transform player = null;
    [Required] public GameObject cam = null;


    [Header("Camera Configurable Data")]
    
    [SerializeField, Range(0f, 5f), Tooltip("How far away it should be when the mouse is at the edge of the screen.")]
    private float cameraDist = 0f;
    
    [SerializeField, Range(0.1f, 1f)]
    private float smoothTime = 0.2f;
    
    [SerializeField]
    private float zStart = 0f;

    [SerializeField, Tooltip("Determins whether or not the mouse positional data is normalized.")]
    private bool normalizeMousePosData = false;


#region Unity Functions
    void Awake()
    {
        if (player == null && GameObject.FindObjectOfType<PlayerController>() != null)
            player = GameObject.FindObjectOfType<PlayerController>().transform;
        
        if (cam == null && transform.GetChild(0) != null)
            cam = transform.GetChild(0).gameObject;
    }

    void Start()
    {
        zStart = transform.position.z;

        if (player != null)
            transform.position = player.position;
    }

    void OnEnable()
    {
        if (CameraShakeEvent != null)
        {
            CameraShakeEvent.OnEventRaised += ShakeCamera;
        }
    }
    
    void OnDisable()
    {
        if (CameraShakeEvent != null)
        {
            CameraShakeEvent.OnEventRaised -= ShakeCamera;
        }
    }

    void Update()
    {
        mousePos = CaptureMousePos();
        targetPos = UpdateTargetPos();
        UpdateCamPos();
    }
#endregion

#region Class Methods
    private void ShakeCamera(ShakePreset sPreset)
    {
        if (sPreset != null)
            Shaker.ShakeAll(sPreset);
    }
    
    private void UpdateCamPos()
    {
        Vector3 newPos = Vector3.zero;
        newPos = Vector3.SmoothDamp(transform.position, targetPos, ref refVel, smoothTime);
        transform.position = newPos;
    }

    private Vector2 CaptureMousePos()
    {
        Camera playerCam = cam.GetComponent<Camera>();
        Vector2 result = playerCam.ScreenToViewportPoint(Input.mousePosition);

        // Vector math to adjust the offset as relative to center of screen
        result *= 2;
        result -= Vector2.one;

        // Distance remains consistant around the edges of the screen
        if (normalizeMousePosData)
        {
            float max = 0.9f;
            if (Mathf.Abs(result.x) > max || Mathf.Abs(result.y) > max)
            {
                result = result.normalized;
            }
        }

        return result;
    }

    private Vector3 UpdateTargetPos()
    {
        Vector3 result = new Vector3(player.position.x + (mousePos.x * cameraDist),
                                      player.position.y + (mousePos.y * cameraDist),
                                      zStart);

        return result;
    }

#if UNITY_EDITOR
    [Button("Press to show Debug Data")]
    private void ToggleDebugData()
    {
        ShowDebugData = !ShowDebugData;
    }
#endif
#endregion
}
