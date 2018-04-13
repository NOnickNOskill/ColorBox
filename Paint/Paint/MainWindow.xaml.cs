using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Paint.classes;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;

namespace Paint
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<classes.Shape> shapeList = new List<classes.Shape>();

        private classes.Shape shape;
        private Creator currentcreator;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            classes.Shape shape = new classes.Rectangle(Colors.Green, new Point(100, 100), new Point(200, 300));
            classes.Drawing.Draw(shape, canvas);

            shape = new Square(Colors.Blue, new Point(400, 100), new Point(500, 300));
            classes.Drawing.Draw(shape, canvas);

            shape = new classes.Ellipse(Colors.Red, new Point(100, 400), new Point(300, 500));
            classes.Drawing.Draw(shape, canvas);

            shape = new Circle(Colors.Yellow, new Point(800, 400), new Point(900, 600));
            classes.Drawing.Draw(shape, canvas);

            Point[] triangle = new Point[3] { new Point(800, 200), new Point(1000, 200), new Point(1000, 400) };
            shape = new classes.Polygon(Colors.DarkBlue, triangle);
            classes.Drawing.Draw(shape, canvas);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            listItems.Items.Clear();
            shapeList.Clear();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(canvas);
            shape = currentcreator.FactoryMethod(Colors.Red, position, position);
            classes.Drawing.Draw(shape, canvas);
            shapeList.Add(shape);
            listItems.Items.Add(shape.ToString().Substring(shape.ToString().LastIndexOf('.') + 1)); ;
            shape.factoryType = currentcreator.GetType();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = Mouse.GetPosition(canvas);
                shape.BottomRight = position; 
            }

            if (e.RightButton == MouseButtonState.Pressed && canvas.Children.Count > 0)
            {

                shape.OffsetX = -((shape.TopLeft.X + shape.BottomRight.X) / 2 - e.GetPosition(canvas).X);
                shape.OffsetY = -((shape.TopLeft.Y + shape.BottomRight.Y) / 2 - e.GetPosition(canvas).Y);
                
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            currentcreator = new RectangleCreator();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            currentcreator = new SquareCreator();
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            currentcreator = new EllipseCreator();
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            currentcreator = new CircleCreator();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && Keyboard.IsKeyDown(Key.LeftCtrl) && canvas.Children.Count != 0)
            {
                canvas.Children.Remove(canvas.Children[canvas.Children.Count - 1]);
                listItems.Items.RemoveAt(listItems.Items.Count - 1);
                shapeList.RemoveAt(shapeList.Count - 1);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Picture";
            dlg.DefaultExt = ".json";
            dlg.Filter = "Text documents (.json)|*.json";
            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                using (StreamWriter stream = new StreamWriter(filename))
                {
                    using (JsonWriter writer = new JsonTextWriter(stream))
                    {
                        for (int i=0; i < shapeList.Count; i++)
                        {
                            serializer.Serialize(writer, shapeList[i]);
                            if (i != shapeList.Count - 1)
                                stream.Write('\n');
                        }
                    }
                }
            }
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            OpenFileDialog openFile = new OpenFileDialog
            {
                FileName = "",
                DefaultExt = ".json",
                Filter = "Text documents (.json)|*.json"
            };
            if (openFile.ShowDialog() ==true)
            {
                string filename = openFile.FileName;
                using (StreamReader stream = new StreamReader(filename))
                {
                    string data = stream.ReadToEnd();
                    {
                        string[] dataArray = data.Split('\n'); 
                        foreach (string dataBlock in dataArray)
                        {
                            try
                            {
                                classes.Shape sh = JsonConvert.DeserializeObject<classes.Rectangle>(dataBlock, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
                                Creator factory = (Creator)Activator.CreateInstance(sh.factoryType);
                                shape = factory.FactoryMethod(sh.Color, sh.TopLeft, sh.BottomRight);
                                shape.factoryType = sh.factoryType;
                                shape.OffsetX = sh.OffsetX;
                                shape.OffsetY = sh.OffsetY;
                                shapeList.Add(shape);
                                listItems.Items.Add(shape.ToString().Substring(shape.ToString().LastIndexOf('.') + 1)); ;
                                classes.Drawing.Draw(shape, canvas);
                            }
                            catch
                            {
                                continue;
                            }
                          
                        }
                    }
                }
            }
        }

        private void listItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listItems.SelectedIndex != -1)
                shape = shapeList[listItems.SelectedIndex];
        }
    }
}
