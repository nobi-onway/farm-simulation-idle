using UnityEngine;

public class FarmUI : MonoBehaviour
{
    [SerializeField] private PlotUI _plotUIPrefab;
    [SerializeField] private RectTransform _plotContainer;

    private void OnEnable()
    {
        FarmManager.Instance.OnAddItem += GeneratePlotUI;
    }

    private void OnDisable()
    {
        FarmManager.Instance.OnAddItem -= GeneratePlotUI;
    }

    private void GeneratePlotUI(Plot plot)
    {
        PlotUI plotUIClone = Instantiate(_plotUIPrefab, _plotContainer);
        plotUIClone.Initialize(plot);
    }
}