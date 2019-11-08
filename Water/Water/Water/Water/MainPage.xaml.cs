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
                Label label = AddLabel(Convert.ToString(SelectPage.count));
                bool isInBusket = false;
                Image img = new Image();
                img.HeightRequest = 80;
                img.WidthRequest = 80;
                for (int i = 0; i < busket.Count; i++)
                {
                    if (busket[i].name == SelectPage.selectedItem)
                    {
                        liquid mem;
                        mem.name = busket[i].name;
                        mem.count = SelectPage.count + busket[i].count;
                        busket[i] = mem;
                        isInBusket = true;
                    }
                }
                if (isInBusket)
                {
                    foreach(var child in grid.Children.Reverse())
                    {
                        grid.Children.Remove(child);
                        pos = 0;
                    }

                    for (int i = 0; i < busket.Count; i++)
                    {
                        Label l = AddLabel(Convert.ToString(busket[i].count));
                        Image im = new Image();
                        im.HeightRequest = 80;
                        im.WidthRequest = 80;
                        switch (busket[i].name)
                        {
                            case "Вода":
                                im.Source = "wata.jpg";
                                break;
                            case "Сок":
                                im.Source = "j.jpg";
                                break;
                            case "Водка":
                                im.Source = "v.jpg";
                                break;
                        }
                        grid.Children.Add(im, 0, i);
                        grid.Children.Add(l, 1, i);
                        AddButton();
                        pos++;
                    }
                }

                if (!isInBusket)
                {
                    liquid mem;
                    mem.name = SelectPage.selectedItem;
                    mem.count = SelectPage.count;
                    busket.Add(mem);

                    switch (SelectPage.selectedItem)
                    {
                        case "Вода":
                            img.Source = "wata.jpg";
                            break;
                        case "Сок":
                            img.Source = "j.jpg";
                            break;
                        case "Водка":
                            img.Source = "v.jpg";
                            break;
                    }
                    grid.Children.Add(img, 0, pos);
                    grid.Children.Add(label, 1, pos);
                    AddButton();
                    pos++;
                }
            };
            Navigation.PushAsync(select_page); //запушим новую функцию
        }

        public void Delete(object sender, System.EventArgs e)
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

        public void AddButton()
        {
            Button del = new Button();
            del.HeightRequest = 40;
            del.WidthRequest = 60;
            del.BackgroundColor = Color.Pink;
            del.Text = "BACKSPACE";
            del.Clicked += Delete;
            grid.Children.Add(del, 2, pos);
        }

        public Label AddLabel(string s)
        {
            Label l = new Label();
            l.Text = s;
            l.FontSize = 50;
            l.HorizontalTextAlignment = TextAlignment.Center;
            l.TextColor = Color.Pink;
            return l;
        }

    }
}
