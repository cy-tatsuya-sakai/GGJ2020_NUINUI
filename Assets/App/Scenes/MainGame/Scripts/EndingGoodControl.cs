using System.Collections;
using System.Collections.Generic;
using Lib.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndingGoodControl : MonoBehaviour
{
    [SerializeField, Header("キー入力待ち時間")]
    float WaitForInput = 2.0f;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.BGM.PlayCrossFade(_BGM._GOOD);
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > WaitForInput)
        {
            if (Input.anyKeyDown)
            {
                SoundManager.Instance.SE.Play(_SE._OK);
                SceneManager.LoadScene("Credits");
            }
        }

        // counter
        counter += Time.deltaTime;
    }
}
