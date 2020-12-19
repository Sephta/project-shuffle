using UnityEngine;
using NaughtyAttributes;


/// <summary>
/// Flips sprite associated with an objects' sprite renderer based on screen pos of mouse
/// </summary>
public class FlipSprite : MonoBehaviour
{

    [Header("Dependencies")]
    public SpriteRenderer _sr = null;
    public Camera _cam = null;

    [Header("Debug Data")]
    [ReadOnly] public Vector3 mousePos = Vector2.zero;
    [SerializeField, ReadOnly] private float angle = 0;

#region Unity Functions
    void Awake()
    {
        if (_sr == null && GetComponent<SpriteRenderer>() != null)
            _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Flip();
    }
#endregion

    private void Flip()
    {
        Vector2 direction = Vector2.zero;
        mousePos = Input.mousePosition;
        
        if (_cam != null)
            direction = mousePos - _cam.WorldToScreenPoint(transform.position);
        else
            Debug.LogError("ERROR: script FlipSprite on " + gameObject.name + " has no camera to reference.");

        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        _sr.flipX = (angle < 0) ? true : false;
    }
}
