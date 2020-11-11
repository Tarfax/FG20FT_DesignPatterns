public struct MoveToCommand {
    public Tile Tile;
    public Enemy Enemy;

    public bool HasAttackCommand() {
        return Enemy != null;
    }
}