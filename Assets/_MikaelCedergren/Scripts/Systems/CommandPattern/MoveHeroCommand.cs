using UnityEngine;

public class MoveHeroCommand : ICommand {

    private Tile tile;
    private Hero hero;

    public MoveHeroCommand(Hero hero, Tile tile) {
        this.hero = hero;
        this.tile = tile;
    }
    public void Execute() {
        MoveCharacterState state = hero.ChangeToState<MoveCharacterState>();
        tile.IsPreview(false);
        state.MoveTo(new MoveToCommand() { Tile = tile, Enemy = null });
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
