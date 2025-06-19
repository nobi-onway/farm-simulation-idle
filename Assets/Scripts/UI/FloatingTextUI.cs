using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class FloatingTextUI : MonoSingleton<FloatingTextUI>
{
    [SerializeField] private TextMeshProUGUI _floatingTextPrefab;

    public void ShowText(string text)
    {
        TextMeshProUGUI floatingText = Instantiate(_floatingTextPrefab, this.transform);
        floatingText.SetText(text);

        StartCoroutine(IE_FloatingEffect(floatingText, Input.mousePosition, 1f));
    }

    private IEnumerator IE_FloatingEffect(TextMeshProUGUI floatingText, Vector3 position, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            floatingText.color = Color.Lerp(Color.red, Color.clear, timer / duration);
            floatingText.transform.position = Vector3.Lerp(position, position + Vector3.up * 100, timer / duration);

            yield return null;
        }

        Destroy(floatingText.gameObject);
    }
}