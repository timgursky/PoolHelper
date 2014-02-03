using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using PoolAdapters;

namespace PoolHelper.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PoolAdapter adapter = new HashFasterPoolAdapter(new HashFasterPoolAdapterOptions(new NetworkCredential("mad.hedgehogg@gmail.com", "D3dCgtBtgnDXhKeh")));
            
            adapter.GetPoolStatsAsync();

            adapter = new WeMineLtcPoolAdapter(new WeMineLtcPoolAdapterOptions(@"beb7a2aed41cbb959cc2190cd00bc7da431681a6a76c802191fd3d387cca1e72"));

            adapter.GetPoolStatsAsync();
        }
    }
}
