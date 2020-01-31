using Lib.Util;
using UnityEngine;
using UnityEngine.Audio;

namespace Lib.Sound
{
    /// <summary>
    /// サウンド再生管理
    /// </summary>
    public partial class SoundManager : SingletonBehaviour<SoundManager>
    {
        [SerializeField] private AudioMixerGroup    _mixerGroupMaster;
        [SerializeField] private SoundPlayer        _soundPlayerSE;
        [SerializeField] private SoundPlayer        _soundPlayerBGM;
        [SerializeField] private SoundPlayer        _soundPlayerJingle;

        public SoundPlayer SE       { get { return _soundPlayerSE; } }
        public SoundPlayer BGM      { get { return _soundPlayerBGM; } }
        public SoundPlayer Jingle   { get { return _soundPlayerJingle; } }

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            _soundPlayerSE.Init();
            _soundPlayerBGM.Init();
            _soundPlayerJingle.Init();
        }

        /// <summary>
        /// すべて停止
        /// </summary>
        public void StopAll(float fadeSec = SoundPlayer.FADE_SEC)
        {
            _soundPlayerSE.Stop(fadeSec);
            _soundPlayerBGM.Stop(fadeSec);
            _soundPlayerJingle.Stop(fadeSec);
        }

        /// <summary>
        /// マスターのボリュームを設定
        /// </summary>
        public void SetVolumeMaster(float volume)
        {
            _mixerGroupMaster.audioMixer.SetVolume(_mixerGroupMaster.name, volume);
        }

        /// <summary>
        /// マスターのボリュームを取得
        /// </summary>
        public float GetVolumeMaster()
        {
            return _mixerGroupMaster.audioMixer.GetVolume(_mixerGroupMaster.name);
        }
    }
}
