using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour {
    
    public void LoadStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }

}
