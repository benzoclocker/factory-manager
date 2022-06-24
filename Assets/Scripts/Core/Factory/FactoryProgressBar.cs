using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Factory
{
    public class FactoryProgressBar : MonoBehaviour
    {
        public Slider ProgressBar;

        private void Start()
        {
            ProgressBar.maxValue = 4.0f;
            ProgressBar.minValue = 0.0f;
        }

        public void FillProgressBar(float value)
        {
            
        }
    }
}