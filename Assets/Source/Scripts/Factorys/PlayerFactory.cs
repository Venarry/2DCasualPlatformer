using UnityEngine;

public class PlayerFactory
{
    private readonly PlayerView _player = Resources.Load<PlayerView>(Paths.Player);

    public PlayerView Create(Vector3 position)
    {
        PlayerView player = Object.Instantiate(_player, position, Quaternion.identity);

        int maxHealth = 100;
        HealthModel healthModel = new(maxHealth);
        HealthPresenter healthPresenter = new(healthModel);

        PlayerHealthView healthView = player.GetComponent<PlayerHealthView>();
        healthView.Init(healthPresenter);

        DeathHandler deathHandler = new();

        return player;
    }
}
