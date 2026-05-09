using System.Text.RegularExpressions;

namespace Kaucjomat.Behavior
{
    public class DecimalValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                return;

            var regex = new Regex(@"^\d*[,.]?\d{0,2}$");

            if (!regex.IsMatch(e.NewTextValue))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }
    }
}
