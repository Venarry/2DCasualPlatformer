using UnityEngine;

public class PlayerFactory
{
    private readonly PlayerView _prefab = Resources.Load<PlayerView>(Paths.Player);

    public PlayerView Create(Vector3 position)
    {
        PlayerView player = Object.Instantiate(_prefab, position, Quaternion.identity);

        int maxHealth = 100;
        HealthModel healthModel = new(maxHealth);
        HealthPresenter healthPresenter = new(healthModel);

        PlayerHealthView healthView = player.GetComponent<PlayerHealthView>();
        healthView.Init(healthPresenter);

        return player;
    }
}
