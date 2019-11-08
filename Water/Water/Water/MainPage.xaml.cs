using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Water
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public struct liquid
        {
            public object name;
            public int count;
        }

        public static int pos = 0;
        List<liquid> busket = new List<liquid>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var select_page = new SelectPage();

            select_page.Disappearing += (a, b) =>
            {   
                if (SelectPage.is_clicked)
                {
                    bool isInBusket = false;

                    for (int i = 0; i < busket.Count; i++)
                    {
                        if (busket[i].name == SelectPage.selectedItem)
                        {
                            liquid mem;
                            mem.name = busket[i].name;
                            mem.count = SelectPage.count + busket[i].count;
                            busket[i] = mem;
                            isInBusket = true;
                            break;
                        }
                    }
                    if (isInBusket)
                    {
                        ClearGrid(grid);

                        for (int i = 0; i < busket.Count; i++)
                        {
                            AddToGrid(Convert.ToString(busket[i].name), Convert.ToString(busket[i].count), i);
                            pos++;
                        }
                    }
                    else
                    {
                        liquid mem;
                        mem.name = SelectPage.selectedItem;
                        mem.count = SelectPage.count;
                        busket.Add(mem);
                        AddToGrid(Convert.ToString(SelectPage.selectedItem), Convert.ToString(SelectPage.count), pos);
                        pos++;


                    }
                }
            };
            Navigation.PushAsync(select_page); //запушим новую функцию
        }

        private void AddToGrid(string image, string lbl, int p)
        {
            Image img = new Image();
            img.Source = ChooseImage(image);
            grid.Children.Add(img, 0, p);

            Label label = AddLabel(lbl);
            grid.Children.Add(label, 1, p);

            AddButton();
        }

        private void Delete(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            var row = Grid.GetRow(button);
            var children = grid.Children.ToList();
            foreach (var child in children.Where(child => Grid.GetRow(child) == row))
            {
                grid.Children.Remove(child);
            }
            foreach (var child in children.Where(child => Grid.GetRow(child) > row))
            {
                Grid.SetRow(child, Grid.GetRow(child) - 1);
            }
            busket.RemoveAt(row);
            pos--;
        }

        private void AddButton()
        {
            Button del = new Button();
            del.HeightRequest = 40;
            del.WidthRequest = 60;
            del.BackgroundColor = Color.Pink;
            del.Text = "BACKSPACE";
            del.Clicked += Delete;
            grid.Children.Add(del, 2, pos);
        }

        private Label AddLabel(string s)
        {
            Label l = new Label();
            l.Text = s;
            l.FontSize = 50;
            l.HorizontalTextAlignment = TextAlignment.Center;
            l.TextColor = Color.Pink;
            return l;
        }

        private string ChooseImage(string s)
        {
            switch (s)
            {
                case "Вода":
                    return "wata.jpg";
                case "Сок":
                    return "j.jpg";
                case "Водка":
                    return "v.jpg";
            }
            return "a";
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (busket.Count == 0)
            {
                await DisplayAlert("Уведомление", "Вы ничего не выбрали!", "Протити");
            }
            else
            {
                await DisplayAlert("Уведомление", "Ваш заказ принят в обработку!", "Пасиба дотвиданя");
                ClearGrid(grid);
                busket.Clear();
            }      
        }

        private void ClearGrid(Grid g)
        {
            foreach (var child in grid.Children.Reverse())
            {
                grid.Children.Remove(child);
            }
            pos = 0;
        }
    }
}
