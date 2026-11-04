using UnityEngine;

public class OverheatController : MonoBehaviour
{
    [SerializeField] private float heat = 0f;
    [SerializeField] private bool isOverheated = false;

    public float heatUpSpeed = 25f;
    public float coolDownSpeed = 20f;
    public float maxScaleMultiplier = 1.5f;
    public float shakeAngle = 5f;
    public float shakeSpeed = 25f;

    private float overheatTimer = 0f;

    private Renderer playerRenderer;
    private Vector3 originalScale;
    private Quaternion originalRotation;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();

        if (playerRenderer == null)
        {
            enabled = false;
            return;
        }

        originalScale = transform.localScale;
        originalRotation = transform.localRotation;

        heat = 0f;
        isOverheated = false;
        overheatTimer = 0f;

        UpdateVisuals(false);

        Debug.Log("Space 를 누르고 있으면 사용합니다.");
    }

    void Update()
    {
        bool isHeating = false;

        if (isOverheated)
        {
            overheatTimer += Time.deltaTime;

            if (overheatTimer >= 2f)
            {
                float coolingTime = Mathf.Min(Time.deltaTime, overheatTimer - 2f);
                heat -= coolDownSpeed * coolingTime;
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            heat += heatUpSpeed * Time.deltaTime;
            isHeating = true;
        }
        else
        {
            heat -= coolDownSpeed * Time.deltaTime;
        }

        heat = Mathf.Clamp(heat, 0f, 100f);

        if (!isOverheated && heat >= 80f)
        {
            isOverheated = true;
            overheatTimer = 0f;

            Debug.Log("[과열] 사용 중지. 2초 후 냉각을 시작합니다.");
        }
        else if (isOverheated && heat <= 20f)
        {
            isOverheated = false;
            overheatTimer = 0f;

            Debug.Log("[냉각 완료] 다시 사용할 수 있습니다.");
        }

        UpdateVisuals(isHeating);
    }

    void UpdateVisuals(bool isHeating)
    {
        if (heat >= 80f)
        {
            playerRenderer.material.color = Color.red;
        }
        else if (heat >= 40f)
        {
            playerRenderer.material.color = Color.yellow;
        }
        else
        {
            playerRenderer.material.color = Color.blue;
        }

        float heatRatio = heat / 100f;
        float scaleMultiplier = Mathf.Lerp(1f, maxScaleMultiplier, heatRatio);

        transform.localScale = originalScale * scaleMultiplier;

        if (isHeating)
        {
            float angle = Mathf.Sin(Time.time * shakeSpeed) * shakeAngle;

            transform.localRotation = originalRotation * Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            transform.localRotation = originalRotation;
        }
    }
}