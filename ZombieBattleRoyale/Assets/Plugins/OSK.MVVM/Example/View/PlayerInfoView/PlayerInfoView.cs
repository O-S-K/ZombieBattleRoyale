using UnityEngine;
using UnityEngine.UI;
namespace OSK.MVVM
{


    public class PlayerInfoView : ViewBase<PlayerViewModel>
    {
        [SerializeField] private Text txtName;
        [SerializeField] private InputField inputName;
        [SerializeField] private Text txtGold;
        [SerializeField] private Button addGoldButton;
        [SerializeField] private Button CloseButton;

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
            // One-way binding
            ViewModel.RegisterBinding(nameof(ViewModel.Name), val =>
            {
                txtName.text = (string)val;
                inputName.text = (string)val;
            });
            ViewModel.RegisterBinding(nameof(ViewModel.Gold), val => { txtGold.text = val.ToString(); });

            // Two-way binding: input → vm
            inputName.onValueChanged.AddListener(newVal =>
            {
                if (ViewModel.Name != newVal)
                    ViewModel.Name = newVal;
            });
            addGoldButton.onClick.AddListener(() => { ViewModel.Gold += 10; });
            CloseButton.onClick.AddListener(() => {  _ = UIManager.Instance.CloseSync(gameObject); });
        }

        protected override void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Name))
            {
                txtName.text = ViewModel.Name;
                inputName.text = ViewModel.Name;
            }
            else if (e.PropertyName == nameof(ViewModel.Gold)) txtGold.text = ViewModel.Gold.ToString();
        }
    }
}