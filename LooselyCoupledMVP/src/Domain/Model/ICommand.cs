namespace LooselyCoupledMVP.Domain.Model
{
    public interface ICommand<T>
    {
        void Execute(T argument);
    }
}