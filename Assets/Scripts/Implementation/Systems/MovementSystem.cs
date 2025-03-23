using UnityEngine;

public class MovementSystem : BuffableComponent
{
    public float Speed;

    public void Move(Vector2 direction)
    {
        transform.position += (Vector3)direction * Speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Debug.Log(Speed);
    }
}