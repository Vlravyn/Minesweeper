using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// Custom Attached property for windows to trigger close from view model without breaking MVVM
    /// </summary>
    public class CloseWindowBehavior : Behavior<Window>
    {
        /// <summary>
        /// The property that can be binded to the view model
        /// Will close the window if true
        /// </summary>
        public bool CloseTrigger
        {
            get
            {
                return (bool)GetValue(CloseTriggerProperty);
            }
            set
            {
                SetValue(CloseTriggerProperty, value);
            }
        }

        public static readonly DependencyProperty CloseTriggerProperty =
            DependencyProperty.Register(nameof(CloseTrigger), typeof(bool), typeof(CloseWindowBehavior), new PropertyMetadata(false, OnCloseTriggerChanged));

        private static void OnCloseTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CloseWindowBehavior behavior = d as CloseWindowBehavior;

            behavior?.OnCloseTriggerChanged();
        }

        private void OnCloseTriggerChanged()
        {
            // when close trigger is true, close the window
            if (CloseTrigger)
            {
                AssociatedObject.Close();
            }
        }
    }
}