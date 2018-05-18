using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingsScript  {

    #region Singleton Pattern
    private static PlayerSettingsScript m_instance;

    public static PlayerSettingsScript Instance {
        get {
            //No PlayerSettingsScript available
            if (m_instance == null) {
                //Create new
                m_instance = new PlayerSettingsScript();
                //Initialize
                m_instance.Init();
            }
            return m_instance;
        }
    }

    #endregion

    // default setup (could be loaded from a pref file in future).
    private void Init() {
        // controller to Keyboard
        m_controller = 0;
    }

    // Prefs and getters/setters below

    // input controller 0=kbd,1=gpad 
    private int m_controller;

    public int Controller {
        get { return m_controller; }
        set { m_controller = value; }
    }
}
