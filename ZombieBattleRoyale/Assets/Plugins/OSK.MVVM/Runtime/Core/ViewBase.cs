using UnityEngine;
using UnityEngine.UI;

namespace OSK.MVVM
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class ViewBase<T> : MonoBehaviour, IViewFor where T : ViewModelBase
    {
        public T ViewModel { get; private set; }
        protected bool IsOpened { get; private set; }

        [SerializeField] protected int baseSortingOrder = 0;
        protected int SortingOrder
        {
            get => baseSortingOrder;
            set
            {
                baseSortingOrder = value;
                if (canvas != null) 
                    canvas.sortingOrder = baseSortingOrder;
            }
        }
        
        protected Canvas canvas;
        protected GraphicRaycaster raycaster;

        public virtual void SetViewModel(T vm)
        {
            if (ViewModel != null) Remove();
            ViewModel = vm;
            Setup();
        }

        void IViewFor.SetViewModel(ViewModelBase vm)
        {
            SetViewModel((T)vm);
        }

        protected virtual void Setup()
        {
            if (ViewModel != null) ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected virtual void Remove()
        {
            if (ViewModel != null) ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        protected abstract void OnViewModelPropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e);

        public virtual void OnOpen()
        {
            gameObject.SetActive(true);
            IsOpened = true;
            ApplySorting(0);
            Debug.Log($"Opened view {gameObject.name}");
        }

        public virtual void OnClose()
        {
            Debug.Log($"Closed view {gameObject.name}");
            IsOpened = false;
            gameObject.SetActive(false);
            Remove();
        }

        protected void SortToTop()
        {
            if (canvas == null) canvas = gameObject.GetComponent<Canvas>();
            canvas.sortingOrder = int.MaxValue;
        }

        public void ApplySorting(int layerIndex)
        {
            if (canvas == null) canvas = gameObject.GetComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = SortingOrder + layerIndex;
        }

        protected void BlockRayCasts(bool block)
        {
            if (raycaster == null) raycaster = gameObject.GetComponent<GraphicRaycaster>();
            raycaster.enabled = block; 
        }
    }
}