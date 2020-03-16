namespace EmptyMVXProjCore.Model
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }

        protected void SetProperty<T>(ref T property, T value, [CallerMemberName]string propertyName = "")
        {
            property = value;

            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
