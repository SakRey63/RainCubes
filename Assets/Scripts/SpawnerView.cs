using TMPro;
using UnityEngine;

public class SpawnerView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _info;

    private void OnEnable()
    {
        _spawner.PoolChanged += ShowInfo;
    }

    private void OnDisable()
    {
        _spawner.PoolChanged -= ShowInfo;
    }

    private void ShowInfo(PoolInfo info)
    {
        _info.text = ($"количество заспавненых объектов за всё время - {info.AmountAllTime}\n" +
                      $"количество созданных объектов - {info.PoolCountAll}\n" +
                      $"количество активных объектов на сцене - {info.PoolCountActive}");
    }
}