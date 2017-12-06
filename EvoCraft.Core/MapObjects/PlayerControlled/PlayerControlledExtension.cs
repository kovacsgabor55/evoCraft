using EvoCraft.Common.MapObjects.PlayerControlled;

namespace EvoCraft.Core
{
    public static class PlayerControlledExtension
    {
        public static void TakeDamage(this PlayerControlledClass playerControlled, int damage)
        {
            playerControlled.ActualHealthPoints -= damage;
        }

        public static void TakeHealing(this PlayerControlledClass playerControlled, int points)
        {
            playerControlled.ActualHealthPoints += points;
            if (playerControlled.ActualHealthPoints > playerControlled.MaximalHealthPoints)
            {
                playerControlled.ActualHealthPoints = playerControlled.MaximalHealthPoints;
            }
        }
    }
}
