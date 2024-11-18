using UnityEngine;
using Zenject;

namespace Facial.FacialMovement
{
    public class FacialMovementModel
    {
        [Inject] public FacialMovementMapper FacialMovementMapper { get; set; }
    }
}