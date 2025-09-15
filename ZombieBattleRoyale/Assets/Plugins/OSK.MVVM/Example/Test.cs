using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OSK.MVVM
{


    public class Test : MonoBehaviour
    {
        public PlayerViewModel playerViewModel;
         void Start()
        {
            playerViewModel = new PlayerViewModel { Name = "Hero", Gold = 100 };
            UIManager.Instance.Open(new UIOpenRequest
            {
                key = "PlayerView",
                priority = 0,
                viewModel = playerViewModel
            });
        
            UIManager.Instance.Open(new UIOpenRequest
            {
                key = "PlayerInfoView",
                priority = 0,
                viewModel = playerViewModel,
            });
        } 
    }

}