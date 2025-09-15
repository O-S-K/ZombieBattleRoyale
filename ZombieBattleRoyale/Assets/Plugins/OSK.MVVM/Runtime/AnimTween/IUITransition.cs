using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
namespace OSK.MVVM
{


    public interface IUITransition
    {
        UniTask PlayOpen(GameObject view,Action onComplete);
        UniTask PlayClose(GameObject view, Action onComplete);
    }
}