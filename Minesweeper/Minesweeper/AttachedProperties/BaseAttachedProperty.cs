using System;
using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// A base attached property every other attached property can inherit from
    /// </summary>
    /// <typeparam name="Parent">the parent to attach the property to</typeparam>
    /// <typeparam name="Property">the property to attach</typeparam>
    public class BaseAttachedProperty<Parent, Property>
        where Parent : BaseAttachedProperty<Parent, Property>, new()
    {
        #region Public Events

        /// <summary>
        /// Event that is raised when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// A singleton instance of the parent
        /// </summary>
        public static Parent Instance { get; private set; }

        #endregion

        #region Attached Property Definitions

        /// <summary>
        /// Creates a custom attached property of type <see cref="Property"/> for <see cref="Parent"/>
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), new PropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));

        #endregion

        #region Get and Set Methods

        /// <summary>
        /// Gets the current value of the property
        /// </summary>
        /// <param name="d">the property to return value of</param>
        /// <returns>the value of the property</returns>
        public static Property GetValue(DependencyObject d)
        {
            return (Property)d.GetValue(ValueProperty);
        }

        /// <summary>
        /// Sets the value of the property
        /// </summary>
        /// <param name="d">the object to set value of</param>
        /// <param name="value">the value to change to</param>
        private static void SetValue(DependencyObject d, Property value)
        {
            d.SetValue(ValueProperty, value);
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Method that is run when the value property changes
        /// </summary>
        /// <param name="d">the parent of the property</param>
        /// <param name="e">the arguments for this event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //call the parent function
            Instance.OnValueChanged(d, e);

            //Call event listeners
            Instance.ValueChanged(d, e);
        }

        /// <summary>
        /// the method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="d">the UI element this property is changed for</param>
        /// <param name="e">the arguments for this event</param>
        public virtual void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion Event Methods
    }
}