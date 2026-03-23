using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float Currenthealth = 100f;
    public float speed = 5f;
    public float power = 5f;
    public float level = 1f;

    [SerializeField] UIDocument uiDocument;
    private ProgressBar Currenthealthbar;

    void Awake()
    {
        Currenthealth = MaxHealth;
    }
    private void OnEnable()
    {
        if (uiDocument != null)
        {
            Currenthealthbar = uiDocument.rootVisualElement.Q<ProgressBar>("HealthBar");

            if (Currenthealthbar != null)
            {
                Currenthealthbar.style.display = DisplayStyle.Flex; 
                Currenthealthbar.lowValue = 0f;
                Currenthealthbar.highValue = 1f; 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrenthealthUI();
        if (Currenthealth <= 0)
        {
            Debug.Log("Player is dead.");
            Destroy(gameObject);
        }
    }
    void UpdateCurrenthealthUI()
    {
        if (Currenthealthbar != null)
        {
            float CurrenthealthPercent = Currenthealth / MaxHealth;
            Currenthealthbar.value = CurrenthealthPercent;
            Currenthealthbar.title = $"{Mathf.Ceil(Currenthealth)} / {MaxHealth}";
        }
    }
    public void TakeDamage(float damage) 
    {
        Currenthealth -= damage;
    }
}
