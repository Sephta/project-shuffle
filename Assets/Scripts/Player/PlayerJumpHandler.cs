using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class PlayerJumpHandler : MonoBehaviour
{
    [Header("Dependencies")]
    // public Transform target;
    public List<Transform> targets = new List<Transform>();
    public List<Rigidbody2D> targets_rb = new List<Rigidbody2D>();

    [Header("Configurable Jump Data")]
    [SerializeField] private float jumpHeight = 0f;
    [SerializeField] private float jumpTime = 0f;
    [SerializeField] private AnimationCurve _customJumpCurve;

    [Header("Debug Data")]
    [SerializeField, ReadOnly] private bool isJumping = false;

#region Unity Functions
    // void Awake() {}
    // void OnEnable() {}
    // void OnDisable() {}
    // void Start() {}
    
    void Update()
    {
        // Check if input manager is active
        if (InputManager._inst != null)
        {
            // Check for jump action
            if (Input.GetKeyDown(InputManager._inst._keyBindings[InputAction.jump]))
            {
                // if not already jumping, then jump
                if (!isJumping)
                {
                    // for (int i = 0; i < targets.Count; i++)
                        // StartCoroutine(ExecutePlayerJump(targets[i], targets[i].localPosition.y, targets[i].localPosition.y + jumpHeight, jumpTime));

                    // float jumpVelocity = Mathf.Sqrt(2 * -9.8f * jumpHeight);
                    // for (int i = 0; i < targets_rb.Count; i++)
                    //     targets_rb[i].AddForce(Vector3.up * jumpVelocity /* * Time.fixedDeltaTime */, ForceMode2D.Impulse);
                }
            }
        }
    }

    // void FixedUpdate() {}
#endregion

#region Class Methods

    private IEnumerator ExecutePlayerJump(Transform t, float origin, float destination, float duration)
    {
        isJumping = true;
        Debug.Log("Player jumping...");
        float current = 0f;
        while (current <= duration)
        {
            current = current + Time.deltaTime;

            float percentCompleted = Mathf.Clamp01(current / duration);
            float percentOnCurve = _customJumpCurve.Evaluate(percentCompleted);
            float evaluation = Mathf.LerpUnclamped(origin, destination, percentOnCurve); 
            
            t.localPosition = new Vector3(t.localPosition.x,
                                          evaluation,
                                          t.localPosition.z);
            
            yield return null;
        }
        // yield return new WaitForSecondsRealtime(seconds);
        Debug.Log("Player done jumping...");
        isJumping = false;
    }

#endregion
}
