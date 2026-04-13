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
                UIMessageManager.Instance.ShowMessage("I can dash now!");
                break;
            case AbilityType.WallSlide:
                player.UnlockWallSlide();
                UIMessageManager.Instance.ShowMessage("I can slide on walls now!");
                break;
        }

        if (pickupEffectPrefab != null)
            Instantiate(pickupEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}