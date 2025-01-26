# Components Documentation

## Unified Selection-Action Control

### Problem
The current implementation uses separate Picker and Button controls for AI URL selection and consultation:
```xml
<Picker x:Name="aiUrlPicker"
        Title="Select AI URL"
        ItemsSource="{Binding Settings.SavedUrls}"
        SelectedItem="{Binding SelectedAIUrl, Mode=TwoWay}"/>

<Button x:Name="btnOpenAI"
        Text="Consult AI"
        Clicked="OnOpenAIClicked"
        BackgroundColor="#0078D4">
```

### Solution
Create a new `SelectionActionControl` that combines both selection and action functionality:

```xml
<SelectionActionControl x:Name="aiSelectionAction"
    Title="AI Consultation"
    ItemsSource="{Binding Settings.SavedUrls}"
    SelectedItem="{Binding SelectedAIUrl, Mode=TwoWay}"
    ActionText="Consult"
    ActionCommand="{Binding OpenAICommand}"
    BackgroundColor="#0078D4"/>
```

### Benefits
- Single control for selection and action
- Improved code maintainability
- Consistent UI pattern
- Better encapsulation of related functionality

### Implementation Details

#### XAML
```xml
<ContentView xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="YiChing.Controls.SelectionActionControl">
    <HorizontalStackLayout Spacing="10">
        <Picker x:Name="selectionPicker"
                Title="{Binding Title}"
                ItemsSource="{Binding ItemsSource}"
                SelectedItem="{Binding SelectedItem, Mode=TwoWay}"
                WidthRequest="200"/>
                
        <Button x:Name="actionButton"
                Text="{Binding ActionText}"
                Command="{Binding ActionCommand}"
                BackgroundColor="{Binding BackgroundColor}"/>
    </HorizontalStackLayout>
</ContentView>
```

#### Code-Behind
```csharp
public partial class SelectionActionControl : ContentView
{
    public static readonly BindableProperty TitleProperty = 
        BindableProperty.Create(nameof(Title), typeof(string), typeof(SelectionActionControl));
        
    public static readonly BindableProperty ItemsSourceProperty = 
        BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SelectionActionControl));
        
    public static readonly BindableProperty SelectedItemProperty = 
        BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(SelectionActionControl), 
            defaultBindingMode: BindingMode.TwoWay);
            
    public static readonly BindableProperty ActionTextProperty = 
        BindableProperty.Create(nameof(ActionText), typeof(string), typeof(SelectionActionControl));
        
    public static readonly BindableProperty ActionCommandProperty = 
        BindableProperty.Create(nameof(ActionCommand), typeof(ICommand), typeof(SelectionActionControl));
        
    public static readonly BindableProperty BackgroundColorProperty = 
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(SelectionActionControl));

    // Property implementations...
}
```

### Migration Plan
1. Create new `SelectionActionControl` in `YiChing/Controls` folder
2. Update existing usage in `CvHexagram.xaml`
3. Update ViewModel to implement `OpenAICommand`
4. Update unit tests to verify new control behavior
