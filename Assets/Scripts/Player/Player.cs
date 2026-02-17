using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float maxhealth = 100f;
    public float health = 100f;
    public float speed = 5f;
    public float power = 5f;
    public float level = 1f;

    [SerializeField] UIDocument uiDocument;
    private ProgressBar healthbar;

    void Awake()
    {
        maxhealth = health;
    }
    private void OnEnable()
    {
        if (uiDocument != null)
        {
            healthbar = uiDocument.rootVisualElement.Q<ProgressBar>("HealthBar");

            if (healthbar != null)
            {
                healthbar.style.display = DisplayStyle.Flex; 
                healthbar.lowValue = 0f;
                healthbar.highValue = 1f; 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        if (health <= 0)
        {
            Debug.Log("Player is dead.");
            Destroy(gameObject);
        }
    }
    void UpdateHealthUI()
    {
        if (healthbar != null)
        {
            float healthPercent = health / maxhealth;
            healthbar.value = healthPercent;
            healthbar.title = $"{Mathf.Ceil(health)} / {maxhealth}";
        }
    }
}
