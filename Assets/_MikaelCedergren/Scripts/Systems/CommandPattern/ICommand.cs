public interface ICommand {
    void Execute();
    void Preview();
    void RevokePreview();
    void Undo();
}
