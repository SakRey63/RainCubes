using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Color _color;

    public Color NewColor => _color;
}
