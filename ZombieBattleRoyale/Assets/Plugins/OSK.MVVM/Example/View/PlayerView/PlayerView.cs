using UnityEngine;
using UnityEngine.UI;
namespace OSK.MVVM
{ 
    public class PlayerView : ViewBase<PlayerViewModel>
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Button showInfoPlayerButton;
    
        public override void OnOpen()
        {
            base.OnOpen();
        }

        public override void OnClose()
        {
            base.OnClose();
        }
    
        protected override void Setup()
        {
            base.Setup();
            RefreshAll();
            showInfoPlayerButton .onClick.AddListener(() =>
            {
                UIManager.Instance.Open(new UIOpenRequest
                {
                    key = "PlayerInfoView",
                    priority = 0,
                    viewModel = FindAnyObjectByType<Test>().playerViewModel,
                    hidePrevious = false
                });
            });
        }

        protected override void Remove()
        {
            showInfoPlayerButton.onClick.RemoveAllListeners();
            base.Remove();
        }

        protected override void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Name)) nameText.text = ViewModel.Name;
        }

        private void RefreshAll()
        {
            if (ViewModel == null) return;
            nameText.text = ViewModel.Name;
        }
    }
}