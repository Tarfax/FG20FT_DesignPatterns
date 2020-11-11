public class AttackCommand : ICommand {

    private Tile tile;
    private Hero hero;
    private Enemy enemy;

    public AttackCommand(Hero hero, Tile tile, Enemy enemy) {
        this.hero = hero;
        this.tile = tile;
        this.enemy = enemy;
    }

    public void Execute() {
        MoveCharacterState state = hero.ChangeToState<MoveCharacterState>();
        state.MoveTo(new MoveToCommand() { Tile = tile, Enemy = enemy }); 
    }

    public void Preview() {
        tile.IsPreview(true);
        tile.Occupy(hero);
    }

    public void RevokePreview() {
        tile.IsPreview(false);
    }

    public void Undo() {
        tile.Occupy(null);
    }
}