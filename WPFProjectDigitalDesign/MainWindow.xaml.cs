using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFProjectDigitalDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(text_box1.Text, out int number))
            {
                if (number < 2)
                {
                    MessageBox.Show("Размер поля не может быть меньше 2");
                    number = 2;
                }
                game = new Game(number);
                game.Start();

                var myGrid = LayoutGrid.Children.OfType<UniformGrid>();

                foreach (var item in myGrid)
                {
                    if (item.Name == "Board")
                    {
                        item.Children.Clear();
                    }
                }

                UniformGrid grid = new UniformGrid();
                grid.Rows = number;
                grid.Columns = number;
                grid.Name = "Board";

                for (int i = 0; i < number; i++)
                {
                    for (int j = 0; j < number; j++)
                    {
                        var button = CreateButton(i, j);
                        grid.Children.Add(button);
                    }
                }

                LayoutGrid.Children.Add(grid);

                Random random = new Random();
                int count = random.Next(1, 5);

                var buttonList = new List<Button>();

                foreach (var item in grid.Children)
                {
                    buttonList.Add((Button)item);
                }

                for (int i = 0; i < count; i++)
                {
                    string randX = random.Next(0, game.GetSize()).ToString();
                    string randY = random.Next(0, game.GetSize()).ToString();

                    foreach (var item in buttonList)
                    {
                        if (item.Tag.ToString().Split(" ")[0] == randX || item.Tag.ToString().Split(" ")[1] == randY)
                        {
                            TurnButton(item);
                        }
                    }

                    if (game.CheckOrientation())
                    {
                        foreach (var item in buttonList)
                        {
                            if (item.Tag.ToString().Split(" ")[0] == randX || item.Tag.ToString().Split(" ")[1] == randY)
                            {
                                TurnButton(item);
                            }
                        }
                        break;
                    }
                }
            }
        }

        public void Turn(object sender, RoutedEventArgs e)
        {
            var but = (Button)sender;

            var orientation = but.Tag.ToString()?.Split(" ")[2];
            string x = but.Tag.ToString()?.Split(" ")[0];
            string y = but.Tag.ToString()?.Split(" ")[1];

            var myGrid = LayoutGrid.Children.OfType<UniformGrid>().Where(a => a.Name == "Board");

            var buttonList = new List<Button>();

            foreach (var grid in myGrid)
            {
                foreach (var item in grid.Children)
                {
                    buttonList.Add((Button)item);
                }
            }
            foreach (var item in buttonList)
            {
                if (item.Tag.ToString().Split(" ")[0] == x || item.Tag.ToString().Split(" ")[1] == y)
                {
                    TurnButton(item);
                }
            }
            if (game.CheckOrientation())
            {
                MessageBox.Show("Вы победили!");
            }
        }

        public Button CreateButton(int i, int j)
        {
            Button newButton = new Button { MinHeight = 20, Tag = $"{i} {j} {game.GetOrientation(i, j)}", FontSize = 20 };
            newButton.Content = newButton.Tag.ToString()?.Split(" ")[2] == "Vertical" ? newButton.Content = "|" : newButton.Content = "—";
            newButton.Click += Turn;
            return newButton;
        }

        public void TurnButton(Button but)
        {
            var orientation = but.Tag.ToString()?.Split(" ")[2];

            if (orientation == "Horizontal")
            {
                but.Tag = $"{but.Tag.ToString()?.Split(" ")[0]} {but.Tag.ToString()?.Split(" ")[1]} Vertical";
                game.TurnOrientation(int.Parse(but.Tag.ToString()?.Split(" ")[0]), int.Parse(but.Tag.ToString()?.Split(" ")[1]));
                but.Content = "|";
            }
            else
            {
                but.Tag = $"{but.Tag.ToString()?.Split(" ")[0]} {but.Tag.ToString()?.Split(" ")[1]} Horizontal";
                game.TurnOrientation(int.Parse(but.Tag.ToString()?.Split(" ")[0]), int.Parse(but.Tag.ToString()?.Split(" ")[1]));
                but.Content = "—";
            }
        }

    }
}
