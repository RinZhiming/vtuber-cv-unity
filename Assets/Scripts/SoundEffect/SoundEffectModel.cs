using Zenject;

namespace SoundEffect
{
    public class SoundEffectModel
    {
        [Inject] public SoundEffect[] SoundEffects { get; set; }
    }
}