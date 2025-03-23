using UnityEngine;

public class HealthArea : MonoBehaviour
{
    //[SerializeField] private HealingBuff _healingBuff;
    [SerializeField] private MovementBuff _movementBuff;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var buffHandler = other.GetComponent<BuffHandler>();

        if (buffHandler != null)
        {
            buffHandler.Apply(_movementBuff);
        }
    }
}