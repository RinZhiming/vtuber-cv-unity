using Zenject;

namespace Background
{
    public class BackgroundModel
    {
        [Inject] public Background[] Backgrounds { get; set; }
    }
}