using TMPro;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private SkillsView _skillsView;
    [SerializeField] private Transform _skillsParent;
    [SerializeField] private Transform[] _spikePatroolingPoints;
    [SerializeField] private TMP_Text _playerHealthLabel;

    private void Awake()
    {
        IInputsProvider inputsHandler = new KeyboardInputsHandler();

        PlayerFactory playerFactory = new();
        EnemySpikeFactory enemySpikeFactory = new();
        SkillsIconFactory skillsIconFactory = new();
        TargetsProvider targetsProvider = new();
        SkillsHolder skillsModel = new(new());
        TartgetsFinderForSkillCast tartgetsFinderForSkillCast = new(targetsProvider);

        int playerTeamIndex = 0;
        Vector2 playerSpawnPosiiton = Vector2.zero;

        PlayerView player = playerFactory.Create(
            targetsProvider,
            skillsModel,
            playerSpawnPosiiton,
            inputsHandler,
            tartgetsFinderForSkillCast,
            playerTeamIndex,
            _playerHealthLabel);

        _skillsView.Init(_skillsParent, skillsModel, skillsIconFactory);

        int enemyTeamIndex = 1;
        Vector2 spikeSpawnPoint = new(3, 0.5f);
        float spikeHealth = 50f;
        float spikeSpeed = 1f;
        float spikeChaseSpeed = 2f;
        float spikeChaseDistance = 3f;

        enemySpikeFactory.Create(
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
