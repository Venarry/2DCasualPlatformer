using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _template;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _respawnTime;

    private Dictionary<Coin, Transform> _spawnedCoins = new Dictionary<Coin, Transform>();

    private void Start()
    {
        FillAllPoints();
    }

    private void FillAllPoints()
    {
        foreach (Transform point in _spawnPoints)
        {
            SpawnCoin(point);
        }
    }

    private void OnPickUp(Coin coin)
    {
        if (_spawnedCoins.ContainsKey(coin) == false)
            throw new System.Exception();

        Transform currentCointPoint = _spawnedCoins[coin];
        _spawnedCoins.Remove(coin);
        coin.Picked -= OnPickUp;

        StartCoroutine(Spawn(currentCointPoint));
    }

    private IEnumerator Spawn(Transform point)
    {
        yield return new WaitForSeconds(_respawnTime);
        SpawnCoin(point);
    }

    private void SpawnCoin(Transform point)
    {
        Coin coin = Instantiate(_template, point);
        _spawnedCoins.Add(coin, point);
        coin.Picked += OnPickUp;
    }
}
