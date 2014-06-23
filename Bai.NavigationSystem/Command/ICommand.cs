namespace Bai.NavigationSystem.Command
{
    public interface ICommand
    {
        CommandType GetCommandType();
        void Execute();
    }
}
