using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Image bloodImage;
    [SerializeField] float fadeDuration;

    void Start()
    {
        SetAlpha(0f);
    }

    public void ShowDamage()
    {
        StopAllCoroutines();
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter()
    {
        SetAlpha(1f);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed/fadeDuration);
            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        if (bloodImage != null)
        {
            Color color = bloodImage.color;
            color.a = alpha;
            bloodImage.color = color;
        }
    }
}
