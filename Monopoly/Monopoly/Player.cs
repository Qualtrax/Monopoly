using System;

namespace Monopoly
{
    public class Player
    {
        public Int32 Location { get; set; }
        public String Name { get; private set; }
        public Int32 RoundsPlayed { get; set; }
        public Int32 Balance { get; set; }

        public Player (String Name)
        {
            this.Name = Name;
        }
    }
}
