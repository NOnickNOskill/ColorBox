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
using System.Reflection;

namespace Paint
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RefPlugins();
            RefFactory();
        }

        List<classes.Shape> shapeList = new List<classes.Shape>();
        private List<Creator> FactoryList = new List<Creator>()
        {
            new RectangleCreator(),
            new EllipseCreator(),
            new CircleCreator()
        };
        

        private classes.Shape shape;
        private Creator currentcreator;
        private readonly string pluginPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            classes.Shape shape = new classes.Rectangle(Colors.Green, new Point(100, 100), new Point(200, 300));
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
                        serializer.Serialize(writer, shapeList);
                        //foreach (classes.Shape x in shapeList)
                        //{
                            
                        //    stream.Write('\n');
                        //}
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

        private void RefPlugins()
        {
            DirectoryInfo pluginDir = new DirectoryInfo(pluginPath);
            if (!pluginDir.Exists)
            {
                pluginDir.Create();
            }

            var pluginFiles = Directory.GetFiles(pluginPath, "*dll");
            foreach (var file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);
                var types = asm.GetTypes().
                    Where(t => t.GetInterfaces().
                    Where(i => i.FullName == typeof(Plugin).FullName).Any());
                foreach (var type in types)
                {
                    figurelist.Items.Add(type.Name);
                }
            }
        }

        private void RefFactory()
        {
            DirectoryInfo pluginDir = new DirectoryInfo(pluginPath);
            if (!pluginDir.Exists)
            {
                pluginDir.Create();
            }

            var pluginFiles = Directory.GetFiles(pluginPath, "*dll");
            foreach (var file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);
                var types = asm.GetTypes().
                    Where(t => t.GetInterfaces().
                    Where(i => i.FullName == typeof(IPluginFactory).FullName).Any());
                foreach (var type in types)
                {
                    var plugin = (Creator)Activator.CreateInstance(type);
                    FactoryList.Add(plugin);
                }
            }
        }

        private void figurelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentcreator = FactoryList[figurelist.SelectedIndex];
        }
    }
}
