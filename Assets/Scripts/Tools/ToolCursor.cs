using UnityEngine;
using UnityEngine.InputSystem;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ToolCursor : MonoBehaviour
{

    public static ToolCursor Instance;
    private Camera _mainCamera;
    private CircleCollider2D brushCollider;
    public bool isPick = true;

    [SerializeField] private Sprite idlePickSprite;
    [SerializeField] private Sprite pressedPickSprite;
    [SerializeField] private Sprite idleBrushSprite;
    [SerializeField] private Sprite pressedBrushSprite;

    private SpriteRenderer sr;

    private void Awake()
    {
        Instance = this;
        sr = GetComponent<SpriteRenderer>();
        _mainCamera = Camera.main;

        brushCollider = GetComponent<CircleCollider2D>();
        brushCollider.enabled = false;
    }

    public float GetBrushRadius()
    {
        return brushCollider.radius;
    }

    public void SwapDefaultSprite()
    {
        isPick = !isPick;
        if (isPick)
        {
            brushCollider.enabled = false;
            sr.sprite = idlePickSprite;
        }
        else
        {
            sr.sprite = idleBrushSprite;
            brushCollider.enabled = true;
        }
    }

    public void SetSprite(int frame)
    {
        if (isPick)
        {
            if (frame == 0)
            {
                sr.sprite = idlePickSprite;
            }
            else if (frame == 1)
            {
                sr.sprite = pressedPickSprite;
            }
        }
        else
        {
            if (frame == 0)
            {
                sr.sprite = idleBrushSprite;
            }
            else if (frame == 1)
            {
                sr.sprite = pressedBrushSprite;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f; // Ensure it's on the right layer
        transform.position = worldPosition;
    }
}
