using TMPro;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Transform[] _spikePatroolingPoints;
    [SerializeField] private TMP_Text _playerHealthLabel;

    private void Awake()
    {
        IInputsHandler inputsHandler = new KeyboardInputsHandler();

        PlayerFactory playerFactory = new();
        EnemySpikeFactory enemySpikeFactory = new();
        TargetsProvider targetsProvider = new();
        TartgetsFinderForSkillCast tartgetsFinderForSkillCast = new(targetsProvider);

        int playerTeamIndex = 0;
        Vector2 playerSpawnPosiiton = Vector2.zero;
        PlayerView player = playerFactory.Create(
            targetsProvider,
            playerSpawnPosiiton,
            inputsHandler,
            tartgetsFinderForSkillCast,
            playerTeamIndex,
            _playerHealthLabel);

        int enemyTeamIndex = 1;
        Vector2 spikeSpawnPoint = new(3, 0.5f);
        float spikeHealth = 50f;
        float spikeSpeed = 1f;
        float spikeChaseSpeed = 2f;
        float spikeChaseDistance = 3f;
        EnemySpike enemySpike = enemySpikeFactory.Create(
            targetsProvider,
            spikeSpawnPoint,
            enemyTeamIndex,
            spikeHealth,
            spikeSpeed,
            spikeChaseSpeed,
            spikeChaseDistance,
            _spikePatroolingPoints,
            player.transform);

        _cameraMovement.SetTarget(player.transform);
    }
}
