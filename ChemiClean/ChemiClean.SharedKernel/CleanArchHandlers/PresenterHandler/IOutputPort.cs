namespace ChemiClean.SharedKernel
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void HandlePresenter(TUseCaseResponse response);
    }
}