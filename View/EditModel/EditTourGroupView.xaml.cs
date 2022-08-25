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

namespace ManagerMusicGroup.View
{
    /// <summary>
    /// Логика взаимодействия для EditTourGroupView.xaml
    /// </summary>
    public partial class EditTourGroupView : UserControl
    {
        public EditTourGroupView()
        {
            InitializeComponent();
        }
        private void Click_Add(object sender, EventArgs e)
        {
            City.Clear();
        }
    }
}
