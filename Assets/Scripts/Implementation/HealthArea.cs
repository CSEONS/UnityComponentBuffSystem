using UnityEngine;

public class HealthArea : MonoBehaviour
{
    [SerializeField] private HealingBuff _healingBuff;

    private void OnTriggerStay2D(Collider2D other)
    {
        var buffHandler = other.GetComponent<BuffHandler>();

        if (buffHandler != null)
        {
            buffHandler.Apply(_healingBuff);
        }
    }
}