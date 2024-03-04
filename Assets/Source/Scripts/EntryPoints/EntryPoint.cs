using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Transform[] _spikePatroolingPoints;

    private void Awake()
    {
        PlayerFactory playerFactory = new();
        EnemySpikeFactory enemySpikeFactory = new();

        Vector2 playerSpawnPosiiton = Vector2.zero;
        PlayerView player = playerFactory.Create(playerSpawnPosiiton);

        Vector2 spikeSpawnPoint = new(3, 0.5f);
        float spikeSpeed = 1f;
        float spikeChaseSpeed = 2f;
        float spikeChaseDistance = 3f;
        EnemySpike enemySpike = enemySpikeFactory.Create(
            spikeSpawnPoint,
            spikeSpeed,
            spikeChaseSpeed,
            spikeChaseDistance,
            _spikePatroolingPoints,
            player.transform);

        _cameraMovement.SetTarget(player.transform);
    }
}
