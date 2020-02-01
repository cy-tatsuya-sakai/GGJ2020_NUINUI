using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lib.Sound;

public class TitleController : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.BGM.PlayCrossFade(_BGM._GAME_MUSIC);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)
            || Input.anyKeyDown)
        {
            SceneManager.LoadScene("HowToPlay");
        }
    }
}
