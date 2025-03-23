using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public HealthSystem _healthSystem;
    [SerializeField] public MovementSystem _movementSystem;

    [SerializeField] public BuffHandler _buffHandler;

    private void Update()
    {
        Vector2 direction = new Vector2()
        {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical"),
        };

        _movementSystem.Move(direction);
    }
}