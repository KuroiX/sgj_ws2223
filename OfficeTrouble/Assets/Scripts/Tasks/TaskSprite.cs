using UnityEngine;
using UnityEngine.UI;

public class TaskSprite : MonoBehaviour
{
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    private Image f;
    
    private void Awake()
    {
        f = GetComponent<Image>();
        f.sprite = inactiveSprite;
    }

    public void Activate()
    {
        f.sprite = activeSprite;
    }

    public void Deactivate()
    {
        f.sprite = inactiveSprite;
    }
}
