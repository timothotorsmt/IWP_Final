using Application.SceneManagement;
using Common.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Application
{
    /// <summary>
    /// Controls all high level and technical parts of the application
    /// Should not contain any game code. If you see any game code please beat the shit out of me because i'm either drunk or on drugs
    /// 
    /// Is a singleton persistent but should not be deleted. ever.
    /// </summary>
    public class ApplicationManager : SingletonPersistent<ApplicationManager>
    {
        #region SceneManagement

        // Scene Management 
        public SceneLoader ApplicationSceneLoader;

        #endregion

        // Settings 


        // Main UI Settings


        // Controls


        // Saving and loading

        // Start is called before the first frame update
        void Start()
        {
        }
    }
}
