using EvoCraft.Common;

namespace EvoCraft.Core
{
    public static class PlayerControlledExtension
    {
        public static void TakeDamage(this PlayerControlled playerControlled, int damage)
        {
            playerControlled.ActualHealthPoints -= damage;
        }

        public static void TakeHealing(this PlayerControlled playerControlled, int points)
        {
            playerControlled.ActualHealthPoints += points;
            if (playerControlled.ActualHealthPoints > playerControlled.MaximalHealthPoints)
            {
                playerControlled.ActualHealthPoints = playerControlled.MaximalHealthPoints;
            }
        }
    }
}
