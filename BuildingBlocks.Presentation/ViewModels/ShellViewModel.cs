using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<ShowShellEvent>
    {
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(ShowShellEvent @event)
        {
            var viewModel = @event.ViewModel;
            ActivateItem(IoC.GetInstance(viewModel, ""));
        }

        protected override void OnInitialize()
        {
            _eventAggregator.Subscribe(this);
            DisplayName = "Building Blocks";
            ActivateItem(IoC.Get<MainWindowViewModel>());
        }
    }
}
