namespace OSK.MVVM
{
    public interface IViewFor 
    {
        void SetViewModel(ViewModelBase vm);
        void OnOpen();
        void OnClose();
    }
}