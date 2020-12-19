using UnityEngine;
using NaughtyAttributes;


/// <summary>
/// Gets the attatched objects' sprite and points it towards the mouse
/// </summary>
public class LookAtMouse : MonoBehaviour
{
    public SpriteRenderer _sr = null;
    [ReadOnly] public float angle = 0f;

    void Update()
    {
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle > 90 || angle < -90)
        {
            _sr.flipX = true;
            angle += 180f;
        }
        else
        {
            _sr.flipX = false;
        }

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}