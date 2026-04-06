using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour
{
    public Slider hpBar;
    public Image boostIndicator;

    private Coroutine boostRoutine;

    void Start()
    {
        boostIndicator.gameObject.SetActive(false);
    }

    // Called when HP changes
    public void UpdateHP(float current, float max)
    {
        Debug.Log("UpdateHP called: " + current + "/" + max);
        hpBar.value = current / max * 100;
    }

    public void ShowBoost(float duration)
    {
        boostIndicator.gameObject.SetActive(true);
        boostIndicator.fillAmount = 1f;

        // Stop existing boost first
        if (boostRoutine != null)
        {
            StopCoroutine(boostRoutine);
        }

        boostRoutine = StartCoroutine(BoostTimerRoutine(duration));
    }

    public void HideBoost()
    {

        if (boostRoutine != null)
        {
            StopCoroutine(boostRoutine);
        }

        boostIndicator.fillAmount = 1f;
        boostIndicator.gameObject.SetActive(false);
    }

    // Drains radial over boost duration

    private IEnumerator BoostTimerRoutine(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            boostIndicator.fillAmount = 1f - (elapsed / duration);

            yield return null;
        }

        boostIndicator.fillAmount = 0f;
        boostIndicator.gameObject.SetActive(false);
    }
}
