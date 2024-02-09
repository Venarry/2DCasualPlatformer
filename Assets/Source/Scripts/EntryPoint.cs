using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;

    private void Awake()
    {
        PlayerFactory playerFactory = new();

        Vector2 playerSpawnPosiiton = Vector2.zero;
        Player player = playerFactory.Create(playerSpawnPosiiton);

        _cameraMovement.SetTarget(player.transform);
    }
}
