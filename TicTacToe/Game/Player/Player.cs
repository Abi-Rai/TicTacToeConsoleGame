namespace TicTacToe.Game.Player
{
    public class Player
    {
        public string Name { get; private set; }
        public string Marker { get; private set; }
        public bool Human { get; private set; }
        public Player(string name, string marker, bool human = true)
        {
            Name = name;
            Marker = marker;
            Human = human;
        }
        public void ChangeName(string newName)
        {
            Name = newName;
        }
        public void ChangeMarker(string newMarker)
        {
            Marker = newMarker;
        }
        public void SetPlayerAsComputer()
        {
            Human = false;
            Name = "COMPUTER";
        }
    }
}
