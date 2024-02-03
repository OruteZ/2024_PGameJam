using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class MouseOverOutline : MonoBehaviour
{
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    private void OnMouseEnter()
    {
        _outline.enabled = true;
    }

    private void OnMouseExit()
    {
        _outline.enabled = false;
    }
}
