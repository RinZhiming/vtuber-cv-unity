using UnityEngine.UI;
using Zenject;

namespace Audio.AudioPreset
{
    public class AudioPresetModel
    {
        [Inject] public AudioPreset[] AudioPresets { get; set; }
        [Inject] public Toggle CustomToggle { get; set; }
    }
}