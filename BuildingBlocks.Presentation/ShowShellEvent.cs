using System;

namespace BuildingBlocks.Presentation
{
    public class ShowShellEvent
    {
        public Type ViewModel { get; private set; }

        public ShowShellEvent(Type viewModel)
        {
            ViewModel = viewModel;
        }
    }
}