using UnityEngine;

namespace Services.Input
{
    public class InputService : IInputService
    {
        private const string VerticalAxisName = "Vertical";
        private const string HorizontalAxisName = "Horizontal";
        
        public Vector3 Direction 
            => new Vector3(SimpleInput.GetAxis(HorizontalAxisName), 0.0f, SimpleInput.GetAxis(VerticalAxisName));

        public bool InputInAction => Direction != Vector3.zero;
    }
}