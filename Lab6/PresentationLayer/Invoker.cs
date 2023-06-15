namespace PresentationLayer.RunUser;

public class Invoker
{
    private ICommand _command;
 
    public Invoker() { }
 
    public void SetCommand(ICommand com)
    {
        _command = com;
    }
 
    public void StartWork()
    {
        _command.Execute();
    }
    public void FinishWork()
    {
        _command.Undo();
    }
}