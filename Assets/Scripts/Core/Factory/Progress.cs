using UnityEngine;

namespace Core.Factory
{
    public class Progress : IProgress
    {
        private readonly ProgressBar _progressBar;
        private readonly ITimer _timer;

        public Progress(ProgressBar progressBar, ITimer timer, float maxSliderValue)
        {
            _progressBar = progressBar;
            _timer = timer;

            InitializeMaxSliderValue(maxSliderValue);
        }

        public bool IsActive { get; private set; }

        public void DeactivateProgress()
        {
            _progressBar.Slider.gameObject.SetActive(false);
            IsActive = false;
        }

        public void ActivateProgress()
        {
            _progressBar.Slider.gameObject.SetActive(true);
            IsActive = true;
        }

        public void Tick()
        {
            if (IsActive)
            {
                _progressBar.Slider.value = _timer.Delay;
            }
        }

        private void InitializeMaxSliderValue(float maxSliderValue) => 
            _progressBar.Slider.maxValue = maxSliderValue;
    }
}