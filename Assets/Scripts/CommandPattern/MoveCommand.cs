using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector2 _direction;

    public MoveCommand(Vector2 direction)
    {
        _direction = direction;
    }

    public void Execute(Player player)
    {
        player.SetMoveDirection(_direction);
    }
}