using System;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Constants;
using ProjectMM.Core.Scene;
using ProjectMM.UI.Elements;
using UnityEngine;

namespace ProjectMM.Scope.Loader
{
    public class LoaderView : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private ProgressBar _ProgressBar;

        #endregion

        public void LoadGameplayScene()
        {
            SceneLoader.LoadSceneAsync(GameConstants.SceneNames.Gameplay, this.GetCancellationTokenOnDestroy(), new Progress<float>(progress => _ProgressBar.SetFillAmount(progress))).Forget();
        }

        public void LoadHomeScene()
        {
            SceneLoader.LoadSceneAsync(GameConstants.SceneNames.Home, this.GetCancellationTokenOnDestroy(), new Progress<float>(progress => _ProgressBar.SetFillAmount(progress))).Forget();
        }
    }
}