using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements; // 1. REQUIRED NAMESPACE

[RequireComponent(typeof(Rigidbody))]
public class SuperJump : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] UIDocument uiDocument; 
    private ProgressBar chargeBar;

    [Header("Settings")]
    [SerializeField]  float minForce = 1.2f;       
    [SerializeField]  float maxForce = 50f;       
    [SerializeField]  float maxChargeTime = 3f; 
    [SerializeField]  float cooldownTime = 1f;   // Time between jumps

    [SerializeField] InputActionAsset inputActions;
    InputAction superJump;

    // State Variables
    [SerializeField] Rigidbody rb;
     float chargeStartTime;
     bool isCharging;
     float lastJumpTime;
     float jumpForce;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        superJump = inputActions.FindAction("SuperJump");
    }

    void OnEnable()
    {
        // --- UI SETUP ---
        if (uiDocument != null)
        {
            Debug.Log("uifound");
            chargeBar = uiDocument.rootVisualElement.Q<ProgressBar>("SuperJumpCharge");

            if (chargeBar != null)
            {
                chargeBar.style.display = DisplayStyle.None; // Hide initially
                chargeBar.highValue = 1f; // Set max value to 1 (100%)
            }
        }

        superJump.Enable();
        superJump.started += OnChargeStarted;
        superJump.canceled += OnChargeReleased;
    }

    void OnDisable()
    {
        superJump.started -= OnChargeStarted;
        superJump.canceled -= OnChargeReleased;
        superJump.Disable();
    }

    // --- NEW: Update loop to animate the bar ---
    void Update()
    {
        if (isCharging && chargeBar != null)
        {
            float timeHeld = Time.time - chargeStartTime;
            float percent = Mathf.Clamp01(timeHeld / maxChargeTime);

            chargeBar.value = percent; // Fills the bar visually
        }
    }

    private void OnChargeStarted(InputAction.CallbackContext context)
    {
        // Only start charging if cooldown is finished
        if (Time.time >= lastJumpTime + cooldownTime)
        {
            isCharging = true;
            chargeStartTime = Time.time;

            // Show the UI Bar
            if (chargeBar != null)
            {
                chargeBar.style.display = DisplayStyle.Flex;
                chargeBar.value = 0f;
            }

            Debug.Log("Charging Super Jump...");
        }
    }

    private void OnChargeReleased(InputAction.CallbackContext context)
    {
        // Only jump if we were actually charging
        if (isCharging)
        {
            // Hide the UI Bar
            if (chargeBar != null)
            {
                chargeBar.style.display = DisplayStyle.None;
            }

            PerformSuperJump();
            isCharging = false;
        }
    }

    private void PerformSuperJump()
    {
        // 1. Calculate how long we held the button
        float timeHeld = Time.time - chargeStartTime;

        // 2. Convert time to a percentage (0.0 to 1.0)
        float chargePercent = Mathf.Clamp01(timeHeld / maxChargeTime);

        // 3. Calculate the exact force
        // Note: Ensure your Movements script has a public 'jumpForce' variable!
        jumpForce = GetComponent<Movements>().jumpForce;
        float finalForce = Mathf.Lerp(minForce, maxForce, chargePercent);

        // 4. Launch! 
        Debug.Log($"Super Jump Force: {transform.up * finalForce * jumpForce}");
        rb.AddForce((transform.up * finalForce * jumpForce), ForceMode.Impulse);

        // 5. Set Cooldown
        lastJumpTime = Time.time;
    }
}