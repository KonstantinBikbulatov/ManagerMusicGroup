using ManagerMusicGroup.ViewModel;
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
using System.Windows.Shapes;

namespace ManagerMusicGroup.View
{
    /// <summary>
    /// Логика взаимодействия для ConfirmDeleteGroupView.xaml
    /// </summary>
    public partial class ConfirmDeleteGroupView : Window
    {
        private EditGroupViewModel editGroup;
        public ConfirmDeleteGroupView()
        {
            InitializeComponent();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            editGroup = EditGroupViewModel.getInstance();
            editGroup.RunDelete();
        }
    }
}
