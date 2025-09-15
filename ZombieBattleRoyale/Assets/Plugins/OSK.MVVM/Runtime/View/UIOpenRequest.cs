namespace OSK.MVVM
{
    public class UIOpenRequest
    {
        public string key; // identifier for prefab/type
        public ViewModelBase viewModel;
        public int priority;
        public bool hidePrevious = false; // whether to hide previous UI when opening this one

        public UIOpenRequest(string key, ViewModelBase vm, int priority = 0, bool hidePrevious = false)
        {
            this.key = key;
            this.viewModel = vm;
            this.priority = priority;
            this.hidePrevious = hidePrevious;
        }

        public UIOpenRequest(){}
    }
}