namespace LooselyCoupledMVP.Domain
{
    public interface ICommand<T>
    {
        void Execute(T argument);
    }
}