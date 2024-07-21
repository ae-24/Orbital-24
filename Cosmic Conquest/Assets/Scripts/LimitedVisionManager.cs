using System.Collections;
using UnityEngine;

public class LimitedVisionManager : MonoBehaviour
{
    public static LimitedVisionManager Instance { get; private set; }
    [SerializeField] private GameObject vignetteImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Ensure the vignette image also persists across scenes
            if (vignetteImage != null)
            {
                DontDestroyOnLoad(vignetteImage);
                vignetteImage.SetActive(false);
            }
            else
            {
                Debug.LogError("Vignette image is not assigned.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (vignetteImage != null)
        {
            Debug.Log("Vignette image is correctly assigned at Start.");
        }
        else
        {
            Debug.LogError("Vignette image is not assigned at Start.");
        }
    }

    public void TriggerLimitedVision(float duration)
    {
        Debug.Log("Limited vision triggered for " + duration + " seconds.");
        StartCoroutine(LimitedVisionCoroutine(duration));
    }

    private IEnumerator LimitedVisionCoroutine(float duration)
    {
        if (vignetteImage != null)
        {
            vignetteImage.SetActive(true);
            vignetteImage.transform.SetAsLastSibling();
            yield return new WaitForSeconds(duration);
            vignetteImage.SetActive(false);
            Debug.Log("Limited vision ended.");
        }
        else
        {
            Debug.LogError("Vignette image is not assigned.");
        }
    }
}
