using System.Threading.Tasks;
using Core.Reasons;
using Core.UI;

namespace Core
{
    public class Alert : IAlert
    {
        private readonly AlertUI _alertUI;

        public Alert(AlertUI alertUI)
        {
            _alertUI = alertUI;
        }
        
        public async void AlertPlayer(Factory.Factory factory, IReason reason)
        {
            _alertUI.AlertTextComponent.text = $"{factory.name}:{reason.AlertReason}.";
            await Task.Delay(5000);
            _alertUI.AlertTextComponent.text = "";
        }
    }
}