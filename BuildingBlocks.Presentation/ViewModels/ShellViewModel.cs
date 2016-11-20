using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    /// <summary>
    ///     Shell view model class
    /// </summary>
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<ShowShellEvent>
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        ///     Constructor of the class
        /// </summary>
        /// <param name="eventAggregator"></param>
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        ///     ShowShellEvent handler
        /// </summary>
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