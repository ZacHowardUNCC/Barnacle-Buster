using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    public static InputHandler Instance;
    private PlayerInput playerInput;

    private bool pickClickActive = false;
    [SerializeField] private float spriteResetGraceTime = 0.25f; // time until the pick tool sprite is reset if dragged off of a barnacle
    private float pickClickTimer = 0f;
    private bool draggingActive = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate input handlers that arent the first one
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerInput = GetComponent<PlayerInput>();
    }

    public void SwitchToUI()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }

    public void SwitchToGameplay()
    {
        playerInput.SwitchCurrentActionMap("Job Gameplay");
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (Camera.main == null) return;
        //Debug.Log("Current map: " + playerInput.currentActionMap.name);
        if (ToolManager.Instance.IsPickTool())
        {
            if (context.started)
            {
                var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
                if (!rayHit.collider) return;

                // Debug.Log(rayHit.collider.gameObject.name);
                //var barnacle = rayHit.collider.GetComponent<IsClickableBarnacle>();

                var barnacle = rayHit.collider.GetComponentInParent<IsClickableBarnacle>();
                if (barnacle == null) return;

                // Debug.Log("Pick tool is active");
                pickClickActive = true;
                ToolManager.Instance.AnimateTool(1);
                barnacle.OnClickByPick();
            }
            else if (context.canceled)
            {
                ToolManager.Instance.AnimateTool(0);
                pickClickActive = false;
            }
        }
        else if (ToolManager.Instance.IsBrushTool())
        {
            if (context.started)
            {
                draggingActive = true;
                ToolManager.Instance.AnimateTool(1);
            }
            else if (context.canceled)
            {
                ToolManager.Instance.AnimateTool(0);
                draggingActive = false;
            }
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (ToolManager.Instance.IsPickTool() && ToolManager.Instance.brushTool.IsBroken) return;
        ToolManager.Instance.SwapTool();
    }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (pickClickActive)
            {
                pickClickTimer += Time.deltaTime;

                var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
                if ((!rayHit.collider || rayHit.collider.GetComponentInParent<IsClickableBarnacle>() == null) && pickClickTimer > spriteResetGraceTime)
                {
                    // Cursor is no longer over barnacle
                    ToolManager.Instance.AnimateTool(0);
                    pickClickTimer = 0f;
                }
            }
            else
            {
                pickClickTimer = 0f;
            }
            if (draggingActive) // Using this method due to lack of ridgidbody
            {
                // Reduce brush durability
                float amount = ToolManager.Instance.BrushDurabilityLossPerSecond;
                ToolManager.Instance.brushTool.DecreaseDurability(amount * Time.deltaTime);
                Debug.Log(ToolManager.Instance.brushTool.CurrentDurability);

                // Immediately stop brushing logic if brush durability is 0
                if (ToolManager.Instance.brushTool.IsBroken)
                {
                    draggingActive = false;
                    ToolManager.Instance.SetTool(ToolManager.ToolType.Pick);
                    ToolCursor.Instance.SwapDefaultSprite();
                    return; // Stop brushing further
                }

                // Get position of the brush tool (which is the ToolCursor's position. Be careful not to offset collider, instead edit the pivot point in sprite editor)
                Vector2 brushPosition = ToolCursor.Instance.transform.position;

                // Radius of the brush collider
                float brushRadius = ToolCursor.Instance.GetBrushRadius();

                // LayerMask to only hit small barnacles
                LayerMask barnacleLayer = LayerMask.GetMask("Small Barnacle"); // optional

                // Get all overlapping colliders
                Collider2D[] hits = Physics2D.OverlapCircleAll(brushPosition, brushRadius, barnacleLayer);

                foreach (var hit in hits)
                {
                    SmallBarnacle barnacle = hit.GetComponent<SmallBarnacle>();
                    if (barnacle != null)
                    {
                        barnacle.OnBrushedOver(); // Remove small barnacle
                    }
                }
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError("NullReferenceException in Update: " + e.Message + "\n" + e.StackTrace);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
