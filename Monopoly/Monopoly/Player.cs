using System;

namespace Monopoly
{
    public class Player
    {
        public Int32 Location { get; set; }
        public String Name { get; private set; }
        public Int32 RoundsPlayed { get; private set; }
        public Int32 Balance { get; set; }

        public Player (String Name)
        {
            this.Name = Name;
        }

        public void IncrementRoundsPlayed()
        {
            RoundsPlayed++;
        }

        public void AddFunds(Int32 amountToAdd)
        {
            Balance += amountToAdd;
        }

        public void RemoveFunds(Int32 amountToAdd)
        {
            Balance -= amountToAdd;
        }
    }
}
