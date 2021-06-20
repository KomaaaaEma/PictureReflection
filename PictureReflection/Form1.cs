using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using PluginInterface;

namespace PictureReflection
{
    public partial class Form1 : Form
    {

        //1.Преобразовать цветное изображение в оттенки серого.
        //2.Добавить текущую дату в правый нижний угол изображения.
        //3.Разбить изображение на девять равных частей и поменять их местами в произвольном порядке.

        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        public Form1()
        {
            InitializeComponent();
            FindPlugins();
            CreatePluginsMenu();
        }

        public void FindPlugins()
        {
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(folder, "*.dll");
            foreach (var item in files)
            {
                try
                {
                    Assembly pluginsASM = Assembly.LoadFrom(item);
                    foreach (var type in pluginsASM.GetTypes())
                    {
                        Type currentType = type.GetInterface("PluginInterface.IPlugin");
                        if (currentType != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина" + ex.Message);
                }
            }
        }
        private void CreatePluginsMenu()
        {
            foreach (var p in plugins)
            {
                var item = PluginsMenu.DropDownItems.Add(p.Value.Name);
                item.Click += OnPluginClick;
            }
        }


        private void OnPluginClick(object sender, EventArgs args)
        {
            IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            pictureBox1.Image = (Bitmap)plugin.Transform((Bitmap)pictureBox1.Image);
        }

        private void exitItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
