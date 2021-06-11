public class RbyTurn {

    public string Move;
    public string MetronomeMove;
    public string Pokemon;
    public int Flags;

    public RbyTurn(string move, int flags = 0) {
        Move = move.ToUpper();
        Flags = flags;

        if((Flags & 0x3f) == 0) {
            Flags |= 39;
        }
    }

    public RbyTurn(string item, string pokemon) {
        Move = item;
        Pokemon = pokemon;
    }

    public RbyTurn(string move, int flags = 0, string metronomeMove) {
        Move = move.ToUpper();
        Flags = flags;
        MetronomeMove = metronomeMove;
    }
}