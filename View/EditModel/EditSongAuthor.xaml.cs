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
    /// Логика взаимодействия для EditSongAuthor.xaml
    /// </summary>
    public partial class EditSongAuthor : UserControl
    {
        public EditSongAuthor()
        {
            InitializeComponent();
        }
        private void Click_Author(object sender, EventArgs e)
        {
            NameAuthor.Clear();
        }
        private void Click_Composer(object sender, EventArgs e)
        {
            NameComposer.Clear();
        }
    }
}
