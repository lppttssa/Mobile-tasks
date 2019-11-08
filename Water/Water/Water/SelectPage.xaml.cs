using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Water
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPage : ContentPage
    {
        Label label;
        public static int count;
        public static object selectedItem;
        public SelectPage()
        {
            InitializeComponent();
        }

        public void Select(object sender, EventArgs e)
        {
            count = 0;
            Picker picker = sender as Picker;
            selectedItem = picker.SelectedItem;


            switch (selectedItem)
            {
                case "Вода":
                    Fruit.Source = "wata.jpg";
                    break;
                case "Сок":
                    Fruit.Source = "j.jpg";
                    break;
                case "Водка":
                    Fruit.Source = "v.jpg";
                    break;
            }

            Stepper stepper = new Stepper
            {
                Value = 0,
                Minimum = 0,
                Maximum = 10,
                Increment = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            stepper.ValueChanged += OnStepperValueChanged;

            label = new Label
            {
                Text = String.Format("{0}", stepper.Value),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Button button = new Button
            {
                Text = "Подтвердить",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Pink
            };

            button.Clicked += OnButtonClicked;

            this.Content = new StackLayout
            {
                Children =
                {
                    picker,            
                    Fruit,
                    label,
                    stepper,
                    button
                }
            };
        }

        void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            label.Text = String.Format("{0}", e.NewValue);
            count++;
        }

        void OnButtonClicked(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}