using log4net;
using MT.PrismApp.Common.Consts;
using MT.PrismApp.Common.PrismEvents;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.PrismApp.ModuleWorkbench.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        IEventAggregator _eventAggregator;
        public ViewAViewModel(
            IEventAggregator eventAggregator
            )
        {
            _eventAggregator = eventAggregator;
            Message = "View A from your Prism Module";
        }

        public DelegateCommand GotoPowerMgrCmd => new DelegateCommand(GotoPowerMgr);

        private void GotoPowerMgr()
        {
            OpenMenuEventParm openMenuEventParm = new OpenMenuEventParm()
            {
                RegionName = RegionNames.PowerMgr
            };
            Log.Info($"Event Trriger OpenMenuEvent {openMenuEventParm }");
            _eventAggregator.GetEvent<OpenMenuEvent>().Publish(openMenuEventParm);
        }

        public DelegateCommand GotoDrawingExampleCmd => new DelegateCommand(GotoDrawingExample);

        private void GotoDrawingExample()
        {
            OpenMenuEventParm openMenuEventParm = new OpenMenuEventParm()
            {
                RegionName = RegionNames.DrawingExample
            };
            Log.Info($"Event Trriger OpenMenuEvent {openMenuEventParm }");
            _eventAggregator.GetEvent<OpenMenuEvent>().Publish(openMenuEventParm);
        }
    }
}
