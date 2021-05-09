using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using MapControl;
using MapControl.Caching;

namespace VizTest
{
    public partial class MainWindow : Window
    {
        static MainWindow()
        {
            try
            {
                ImageLoader.HttpClient.DefaultRequestHeaders.Add("User-Agent", "OverlordBot Visualizer");

                TileImageLoader.Cache = new ImageFileCache(TileImageLoader.DefaultCacheFolder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            if (TileImageLoader.Cache is ImageFileCache cache)
            {
                Loaded += async (s, e) =>
                {
                    await Task.Delay(2000);
                    await cache.Clean();
                };
            }
        }
    }
}
