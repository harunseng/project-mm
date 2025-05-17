using System;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Constants;
using ProjectMM.Core.Scene;
using ProjectMM.UI.Elements;
using UnityEngine;

namespace ProjectMM.Scope.Splash
{
    public class SplashView : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private ProgressBar _ProgressBar;

        #endregion

        public void Start()
        {
            SceneLoader.LoadSceneAsync(GameConstants.SceneNames.Home, this.GetCancellationTokenOnDestroy(), new Progress<float>(progress => _ProgressBar.SetFillAmount(progress))).Forget();
        }
    }
}