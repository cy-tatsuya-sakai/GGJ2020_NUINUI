﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lib.Sound
{
    /// <summary>
    /// SoundManagerのテスト
    /// </summary>
    public class SoundTest : MonoBehaviour
    {
        private SoundManager _soundManager = default;
        [SerializeField] private Text _soundKeyName = default;

        private void Awake()
        {
            _soundManager = SoundManager.Instance;
        }

        public void OnClickSE()
        {
            _soundManager.SE.Play(_soundKeyName.text);
        }

        public void OnClickBGM()
        {
            _soundManager.BGM.PlayCrossFade(_soundKeyName.text);
        }

        public void OnClickJingle()
        {
            _soundManager.Jingle.Play(_soundKeyName.text);
        }

        public void OnClickStop()
        {
            _soundManager.StopAll();
        }
    }
}