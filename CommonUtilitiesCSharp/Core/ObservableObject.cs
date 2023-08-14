using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CommonUtilitiesCSharp.Core
{
    /// <summary>
    /// Base class for objects that need to notify when their properties change. (Implementation of INotifyPropertyChanged)
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Triggers the PropertyChanged event for the given property name or the calling member name if no property name is given.
        /// </summary>
        /// <param name="propertyName">Name of the property used to raise the <see cref="PropertyChanged"/> event.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}