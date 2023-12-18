using System.Collections;
using UnityEngine;
using Common.DesignPatterns;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UniRx;

namespace Application.SceneManagement
{
    /// <summary>
    /// The main class that loads the different scenes
    /// Controls which scenes load when
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        #region variables

        [SerializeField] private SceneAssetList _sceneList;

        public LoadingScreenType CurrentLoadingScreenType;
        [HideInInspector] public ReactiveProp<SceneID> CurrentSceneID;


        #endregion

        // Start is called before the first frame update
        void Start()
        {
            CurrentSceneID = new ReactiveProp<SceneID>();

            // Do error checks here
            DevUtils.Assert(_sceneList == null, "There is no valid scenelist. Please make sure there is a valid sceneList", LogType.Error);
            DevUtils.Assert(_sceneList.SceneList.Count == 0, "There are no valid scenes in scenelist. Please make sure there is at least one valid scene.");

            CurrentSceneID.SetValue(_sceneList.SceneList.Where(x => x.SceneName == SceneManager.GetActiveScene().name).Select(x => x.SceneAssetID).First());
        }

        /// <summary>
        /// Change the scene
        /// </summary>
        /// <param name="newSceneID">The scene ID of the new scene</param>
        /// <param name="loadScreenType">The loading screen type to load</param>
        public void ChangeScene(SceneID newSceneID, LoadingScreenType loadScreenType)
        {
            DOTween.KillAll();

            // Check for any null cases
            if (_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Count() > 0)
            {
                // If you want the loading screen to show up just set the 2nd parameter to true
                // If checked as true for loadToLoadingScreen, load the loading screen 
                StartCoroutine(LoadSceneCoroutine(newSceneID, loadScreenType));
            }
            else
            {
                Debug.LogWarning("Requested Scene ID of " + newSceneID.ToString() + " is not found. Please make sure you specify a valid scene name in the ID");
            }
        }

        public void ChangeScene(SceneID newSceneID)
        {
            DOTween.KillAll();

            // Check for any null cases
            if (_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Count() > 0)
            {
                CurrentSceneID.SetValue(newSceneID);
                SceneManager.LoadScene(GetSceneName(newSceneID));
            }
            else
            {
                Debug.LogWarning("Requested Scene ID of " + newSceneID.ToString() + " is not found. Please make sure you specify a valid scene name in the ID");
            }
        }

        private IEnumerator LoadSceneCoroutine(SceneID newSceneID, LoadingScreenType loadScreenType)
        {
            CurrentSceneID.SetValue(newSceneID);
            string loadingScreenSceneName = GetSceneName(SceneID.LOADING);

            // Loads the Loading Screen 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadingScreenSceneName);

            // Wait for 1 second minimum (showing the loading screen)
            yield return new WaitForSeconds(1.0f);

            // If the current load for the scene is not done
            if (!asyncLoad.isDone)
            {
                // wait until the asynchronous loading is done
                yield return new WaitUntil(() => asyncLoad.isDone);
            }

            // Once complete
            // Loads the expected scene
            DOTween.KillAll();
            SceneManager.LoadScene(GetSceneName(newSceneID));
        }

        // Returns the string name based on the given string ID
        private string GetSceneName(SceneID sceneToSearch)
        {
            return _sceneList.SceneList.Where(s => s.SceneAssetID == sceneToSearch).Select(s => s.SceneName).LastOrDefault();
        }
    }

    public enum LoadingScreenType
    {
        NONE,
        DEFAULT,
    }

}

