using System.Collections;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace YiChing.Controls
{
    public partial class SelectionActionControl : ContentView
    {
        public SelectionActionControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleProperty = 
            BindableProperty.Create(nameof(Title), typeof(string), typeof(SelectionActionControl));
        
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty = 
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SelectionActionControl));
        
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty = 
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(SelectionActionControl), 
                defaultBindingMode: BindingMode.TwoWay);
            
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly BindableProperty ActionTextProperty = 
            BindableProperty.Create(nameof(ActionText), typeof(string), typeof(SelectionActionControl));
        
        public string ActionText
        {
            get => (string)GetValue(ActionTextProperty);
            set => SetValue(ActionTextProperty, value);
        }

        public static readonly BindableProperty ActionCommandProperty = 
            BindableProperty.Create(nameof(ActionCommand), typeof(ICommand), typeof(SelectionActionControl));
        
        public ICommand ActionCommand
        {
            get => (ICommand)GetValue(ActionCommandProperty);
            set => SetValue(ActionCommandProperty, value);
        }

        public static readonly BindableProperty BackgroundColorProperty = 
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(SelectionActionControl));

        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
    }
}