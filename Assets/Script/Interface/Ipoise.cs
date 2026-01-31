using System;
namespace Assets.Script.Interface
{
    internal interface Ipoise
    {   
        float Maxpoise { get; set; }

        public event Action<float> OnPoiseChange;

        public void OnPoiseDamage(float damage);

        public event Action OnPoiseDepleted;

        public void SetPoise();

    }
}
