using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace iPhoto.Commands
{
    internal class ExtendSearchMenuCommand : CommandBase
    {
        private const int _minWidth = 30;
        private const int _maxWidth = 280;
        private const int _rate = 10;
        private ColumnDefinition? _columnDefinition;

        public override void Execute(object parameter)
        {
            _columnDefinition = parameter as ColumnDefinition;

            if (_columnDefinition.ActualWidth == _minWidth)
            {
                Extend();
            }
            else if (_columnDefinition.ActualWidth == _maxWidth)
            {
                Collapse();
            }
        }
        private async void Extend()
        {
            for (int i = _minWidth; i < _maxWidth; i+= _rate)
            {
                _columnDefinition.Width = new GridLength(i += _rate);
                await Task.Delay(1);
            }
        }
        private async void Collapse()
        {
            for (int i = _maxWidth; i > _minWidth; i -= _rate)
            {
                _columnDefinition.Width = new GridLength(i -= _rate);
                await Task.Delay(1);
            }
        }
    }
}
