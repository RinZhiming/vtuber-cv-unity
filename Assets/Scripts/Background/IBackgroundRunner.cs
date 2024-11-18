using System;
using UnityEngine;
using UnityEngine.UI;

namespace Background
{
    public interface IBackgroundRunner
    {
        public void Init(ToggleGroup group);
        public IDisposable SelectToggle(ToggleGroup group, Image image);
    }
}