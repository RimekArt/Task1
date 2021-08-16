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

namespace Task
{  
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  

        }

        //функция переноса
        private void panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                e.Effects = DragDropEffects.Move;
               
            }
        }

        //функция Drop и удаления
        private void panel_Drop(object sender, DragEventArgs e)
        {
            // Если элемент на панели уже обработал drop, 
            //панель не должна обрабатывать его.
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");

                if (_panel != null && _element != null)
                {
                    //Получает панель, которой в настоящее время принадлежит элемент, 
                    //затем удаляет ее с этой панели и добавьте дочерние элементы панели, на которую он был перетащен.

                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        

                        if (e.KeyStates == DragDropKeyStates.ShiftKey &&
                            e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            Ticket _ticket = new Ticket((Ticket)_element);
                            _parent.Children.Remove(_element);
                            _panel.Children.Remove(_element);
                            
                        }

                        else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            _parent.Children.Remove(_element);
                            _panel.Children.Add(_element);
                            
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }

        }

        private void ButtonAddTicket_Click(object sender, RoutedEventArgs e)
        {

            Ticket _ticket = new Ticket();
            MyCanvas.Children.Add(_ticket);
            
        }

    }
}
