using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class SoundPlay : MonoBehaviour
    {
        private static SoundPlay instance;

        public static SoundPlay Instance => instance;

        [SerializeField]
        private List<AudioFile> AudioFile;

        [SerializeField]
        private AudioSource AudioSource;

        [SerializeField]
        private AudioSource bgm;


        private void Awake()
        {
            // If an instance already exists and it's not this one, destroy this duplicate
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                // Otherwise, set this as the instance
                instance = this;
                // Optional: Keeps the object alive when loading new scenes
                DontDestroyOnLoad(this.gameObject);
            }
        }


        public void PlaySound(string name)
        {
            var audio = AudioFile.Find(x => x.name == name);
            if (audio == null) return;
            AudioSource.PlayOneShot(audio.audioClip);
        }

        public void PlayBGM()
        {
            bgm.Play();
        }

        public void StopBGM()
        { bgm.Stop(); }
    }

    [Serializable]
    public class AudioFile
    {
        public string name;
        public AudioClip audioClip;
    }
}
