using UnityEngine;

public class PlayerFactory
{
    private readonly Player _player = Resources.Load<Player>(Paths.Player);

    public Player Create(Vector3 position)
    {
        Player player = Object.Instantiate(_player, position, Quaternion.identity);

        int maxHealth = 100;
        HealthModel healthModel = new(maxHealth);
        HealthPresenter healthPresenter = new(healthModel);

        HealthView healthView = player.GetComponent<HealthView>();
        healthView.Init(healthPresenter);

        CharacterMovement characterMovement = player.GetComponent<CharacterMovement>();

        player.Init(healthView, characterMovement);

        return player;
    }
}
