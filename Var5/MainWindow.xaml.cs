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

namespace Var5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        de05Ent db = new de05Ent();
        string fld = "\\materials\\", fName = "material_", ext = ".jpeg";
        string fullpath = "";
        int no = 1, num = 0;
        BitmapImage gf = new BitmapImage();
        public MainWindow()
        {
            InitializeComponent();
            filtCb.ItemsSource = db.MaterialType.ToList();
            Load();
        }
        void Load()
        {
            List<Material> list = new List<Material>();
            List<Material> nList = new List<Material>();
            if (!(filtCb.SelectedItem is MaterialType mt))
            {
                int k = no;
                list = db.Material.ToList();
                nList = new List<Material>(list.GetRange(k * 15 - 14, 15));
                mat.ItemsSource = nList;
            }
            else
            {
                int k = no;
                list = db.Material.Where(c => c.MaterialTypeID == mt.ID).ToList();
                nList = new List<Material>(list.GetRange(k * 15 - 14, 15));
                if (nList.Count > k * 15 - 14) mat.ItemsSource = nList;
                else mat.ItemsSource = list.GetRange(k * 15 - 14, nList.Count);
            }
        }

        private void Bwd(object sender, MouseButtonEventArgs e)
        {
            if (no == 1) return;
            no--;
            if (mat.ItemsSource is null) return;
            Load();
        }

        private void SelPg(object sender, MouseButtonEventArgs e)
        {
            TextBlock snd = sender as TextBlock;
            if (no != Convert.ToInt32(snd.Text)) no = Convert.ToInt32(snd.Text);
            else return;
            Load();
        }

        void CheckImg(object sender, MouseEventArgs e)
        {
            foreach (var sp in mat.Items.OfType<StackPanel>())
            {
                foreach (var img in sp.Children.OfType<Image>())
                {
                    for (int i = 0; i < 26; i++)
                    {
                        num = i;
                        fullpath = fld + fName + num.ToString() + ext;
                        gf.UriSource = new Uri(fullpath);
                        MessageBox.Show(fullpath);
                        if (img.Source != gf) img.Source = new BitmapImage(new Uri("\\materials\\picture.png"));
                    }
                }
            }
        }
        void Fwd(object sender, MouseEventArgs m)
        {
            no++;
            if (mat.ItemsSource is null) return;
            Load();
        }

        private void Selector(object sender, SelectionChangedEventArgs e)
        {

            Load();

        }
    }
}
