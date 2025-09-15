// PlayerVM example
namespace OSK.MVVM
{


    public class PlayerViewModel : ViewModelBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _gold;

        public int Gold
        {
            get => _gold;
            set => SetProperty(ref _gold, value);
        }

        public RelayCommand OnClaimReward { get; }

        public PlayerViewModel()
        {
            OnClaimReward = new RelayCommand(_ => { Gold += 100; });
        }
    }
}