using UnityEngine;

public class HatPickup : MonoBehaviour
{
    public enum AbilityType
    {
        Dash,
        WallJump,
        WallSlide
    }

    [SerializeField] private AbilityType abilityType;
    [SerializeField] private GameObject pickupEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null) return;

        switch (abilityType)
        {
            case AbilityType.Dash:
                player.UnlockDash();
                GameEvents.ShowMessage("I can dash now!");
                break;
            case AbilityType.WallSlide:
                player.UnlockWallSlide();
                GameEvents.ShowMessage("I can slide on walls now!");
                break;
            case AbilityType.WallJump:
                player.UnlockWallJump();
                GameEvents.ShowMessage("I can jump on walls during sliding now!");
                break;
        }

        if (pickupEffectPrefab != null)
            Instantiate(pickupEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}