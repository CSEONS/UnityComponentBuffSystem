using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private MovementSystem _movementSystem;

    [SerializeField] private BuffHandler _buffHandler;

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