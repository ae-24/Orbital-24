using System.Collections;
using UnityEngine;

public class LimitedVisionManager : MonoBehaviour
{
    public static LimitedVisionManager Instance { get; private set; }
    [SerializeField] public GameObject vignetteImage;
    public Canvas vignetteCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (vignetteImage != null)
            {
                vignetteCanvas = vignetteImage.GetComponentInParent<Canvas>();
                if (vignetteCanvas != null)
                {
                    DontDestroyOnLoad(vignetteCanvas.gameObject);
                }
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
