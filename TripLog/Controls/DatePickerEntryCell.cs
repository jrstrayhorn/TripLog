using System;
using Xamarin.Forms;

namespace TripLog.Controls
{
    public class DatePickerEntryCell : EntryCell
    {
        public static readonly BindableProperty DateProperty =
            BindableProperty
                .Create<DatePickerEntryCell, DateTime>(p =>
                                                       p.Date,
                                                       DateTime.Now,
                                                       propertyChanged: new BindableProperty.BindingPropertyChangedDelegate<DateTime>(DatePropertyChanged)

                                                      );
        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public new event EventHandler Completed;

        static void DatePropertyChanged(BindableObject bindable,
                                       DateTime oldValue,
                                       DateTime newValue)
        {
            var @this = (DatePickerEntryCell)bindable;

            if (@this.Completed != null)
                @this.Completed(bindable, new EventArgs());
        }
        public DatePickerEntryCell()
        {
        }
    }
}
