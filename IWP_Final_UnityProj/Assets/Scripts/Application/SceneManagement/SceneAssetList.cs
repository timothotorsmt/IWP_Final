using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Application.SceneManagement
{
    // Create a menu thing to show
    [CreateAssetMenu(menuName = "Custom app data containers/Scene Asset List", fileName = "New Scene list", order = 10)]
    public class SceneAssetList : ScriptableObject
    {
        // Physical scene list
        public List<Scene> SceneList;
    }

    [System.Serializable]
    public class Scene
    {
        // A unique identifier of the current scene asset
        public SceneID SceneAssetID;
        public string SceneName;
    }

    // This enumerator gives scenes a unique identification code
    // Scene changes are made by calls based on the ID
    public enum SceneID
    {
        LOADING,
        MAIN_MENU,

        // GAME SCENES

        // Demo Game Scene - TO DELETE
        SYS_TEST_SCENE
    }
}
