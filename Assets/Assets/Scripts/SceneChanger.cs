using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToFamilyView()
    {
        SceneManager.LoadScene("FamilyScene");
    }

    public void ChangeToGameplayView()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
