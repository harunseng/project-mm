using System;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Constants;
using ProjectMM.Core.Scene;
using ProjectMM.UI.Elements;
using UnityEngine;

namespace ProjectMM.Scope.Loader
{
    /// <summary>
    /// View that displays loading progress while transitioning to the gameplay scene
    /// </summary>
    public class LoaderView : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private ProgressBar _ProgressBar;

        #endregion

        public void LoadGameplayScene()
        {
            SceneLoader.LoadSceneAsync(GameConstants.SceneNames.Gameplay, this.GetCancellationTokenOnDestroy(), new Progress<float>(progress => _ProgressBar.SetFillAmount(progress))).Forget();
        }
    }
}
