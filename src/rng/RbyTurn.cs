public class RbyTurn {

    public string Move;
    public string Pokemon;
    public string MetronomeMove;
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
        MetronomeMove = pokemon;
    }
}