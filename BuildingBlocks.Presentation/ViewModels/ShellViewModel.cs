using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<ShowShellEvent>
    {
        private readonly IEventAggregator eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void Handle(ShowShellEvent @event)
        {
            var viewModel = @event.ViewModel;
            ActivateItem(IoC.GetInstance(viewModel, ""));
        }

        protected override void OnInitialize()
        {
            eventAggregator.Subscribe(this);
            DisplayName = "Building Blocks";
            ActivateItem(IoC.Get<MainWindowViewModel>());
        }
    }
}
