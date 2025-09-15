using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OSK.MVVM
{
    public class UIManager : SingletonScene<UIManager>
    {
        [Serializable]
        public class PrefabEntry
        {
            public string key;
            public GameObject prefab;
        }

        [SerializeField] private Transform uiRoot;
        [SerializeField] private List<PrefabEntry> prefabs;
        private Dictionary<string, GameObject> prefabMap = new Dictionary<string, GameObject>();

        // pooling: key -> stack of instances
        private Dictionary<string, Stack<GameObject>> pool = new Dictionary<string, Stack<GameObject>>();

        // danh sách các view đang mở
        private List<(string key, GameObject view)> activeViews = new List<(string, GameObject)>();

        // open queue with priority
        private SortedDictionary<int, Queue<UIOpenRequest>> openQueue = new SortedDictionary<int, Queue<UIOpenRequest>>();
        private bool processingQueue = false;

        private void Awake()
        {
            base.Awake();
            foreach (var e in prefabs)
            {
                if (!string.IsNullOrEmpty(e.key) && e.prefab != null)
                    prefabMap[e.key] = e.prefab;
            }
        }

        public void Preload(string key)
        {
            if (!prefabMap.TryGetValue(key, out var prefab)) return;

            // nếu đã có trong pool thì thôi
            if (pool.TryGetValue(key, out var stack) && stack.Count > 0) return;

            GameObject inst = Instantiate(prefab, uiRoot);
            inst.SetActive(false);
            PushToPool(key, inst);
        }

        // Enqueue open request
        public void Open(UIOpenRequest req)
        {
            if (!openQueue.TryGetValue(req.priority, out var q))
            {
                q = new Queue<UIOpenRequest>();
                openQueue[req.priority] = q;
            }

            q.Enqueue(req);
            TryProcessQueue();
        }

        private void TryProcessQueue()
        {
            if (processingQueue) return;
            processingQueue = true;
            ProcessNextInQueue();
        }

        private void ProcessNextInQueue()
        {
            if (openQueue.Count == 0)
            {
                processingQueue = false;
                return;
            }

            var firstKey = openQueue.Keys.Min();
            var queue = openQueue[firstKey];
            if (queue.Count == 0)
            {
                openQueue.Remove(firstKey);
                ProcessNextInQueue();
                return;
            }

            var req = queue.Dequeue();
            if (queue.Count == 0) openQueue.Remove(firstKey);

            OpenNow(req);
            ProcessNextInQueue();
        }

        private async void OpenNow(UIOpenRequest req)
        {
            if (!prefabMap.TryGetValue(req.key, out var prefab))
            {
                Debug.LogError($"No prefab registered for key {req.key}");
                return;
            }

            if (req.hidePrevious && activeViews.Count > 0)
            {
                await CloseTopView();
            }

            GameObject inst = PopFromPool(req.key) ?? Instantiate(prefab, uiRoot);
            //inst.SetActive(true);
            if (inst.TryGetComponent<IViewFor>(out var iView))
            {
                iView.SetViewModel(req.viewModel);
            }

            if (inst.TryGetComponent<IUITransition>(out var transition))
            {
                inst.gameObject.SetActive(true);
                await transition.PlayOpen(inst, () => { iView.OnOpen(); });
            }
            else
            {
                iView.OnOpen();
            }

            activeViews.Add((req.key, inst));
        }

        /*public void Close(GameObject view)
        {
            var entry = activeViews.FirstOrDefault(x => x.view == view);
            if (entry.view != null)
            {
                activeViews.Remove(entry);
                //entry.view.SetActive(false);
                if (entry.view.TryGetComponent<IViewFor>(out var iView))
                {
                    iView.OnClose();
                }
                PushToPool(entry.key, entry.view);
            }
        }*/

        public async UniTask CloseSync(GameObject view)
        {
            var entry = activeViews.FirstOrDefault(x => x.view == view);
            if (entry.view == null) return;

            activeViews.Remove(entry);

            if (view.TryGetComponent<IUITransition>(out var transition))
            {
                await transition.PlayClose(view, () =>
                {
                    if (entry.view.TryGetComponent<IViewFor>(out var iView))
                    {
                        iView.OnClose();
                    }
                });
            }
            else
            {
                // view.SetActive(false);
                if (entry.view.TryGetComponent<IViewFor>(out var iView))
                {
                    iView.OnClose();
                }
            }

            PushToPool(entry.key, view);
        }

        public async UniTask CloseTopView()
        {
            try
            {
                if (activeViews.Count == 0) return;
                var top = activeViews[^1]; // phần tử cuối
                await CloseSync(top.view);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error closing top view: {e}");
            }
        }

        public void HideAll()
        {
            foreach (var entry in activeViews)
            {
                //entry.view.SetActive(false);
                if (entry.view.TryGetComponent<IViewFor>(out var iView))
                {
                    iView.OnClose();
                }

                PushToPool(entry.key, entry.view);
            }

            activeViews.Clear();
        }

        public void Destroy(GameObject view)
        {
            var entry = activeViews.FirstOrDefault(x => x.view == view);
            if (entry.view != null)
            {
                activeViews.Remove(entry);
                if (entry.view.TryGetComponent<IViewFor>(out var iView))
                {
                    iView.OnClose();
                }

                Destroy(entry.view);
            }
        }

        public void DestroyAll()
        {
            foreach (var entry in activeViews)
            {
                if (entry.view.TryGetComponent<IViewFor>(out var iView))
                {
                    iView.OnClose();
                }

                Destroy(entry.view);
            }

            activeViews.Clear();

            foreach (var stack in pool.Values)
            {
                while (stack.Count > 0)
                {
                    var obj = stack.Pop();
                    Destroy(obj);
                }
            }

            pool.Clear();
        }

        private GameObject PopFromPool(string key)
        {
            if (pool.TryGetValue(key, out var stack) && stack.Count > 0)
                return stack.Pop();
            return null;
        }

        private void PushToPool(string key, GameObject obj)
        {
            if (!pool.TryGetValue(key, out var stack))
            {
                stack = new Stack<GameObject>();
                pool[key] = stack;
            }

            stack.Push(obj);
        }
    }
}