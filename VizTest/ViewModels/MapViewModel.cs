using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using MapControl;

namespace VizTest.ViewModels
{
    public class MapViewModel: INotifyPropertyChanged
    {
        private static Timer aTimer;

        public event PropertyChangedEventHandler PropertyChanged;

        public MapTileLayer Osm = new MapTileLayer
        {
            TileSource = new TileSource {UriFormat = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"},
            SourceName = "OpenStreetMap",
            Description = "© [OpenStreetMap contributors](http://www.openstreetmap.org/copyright)"
        };


        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Location mapCenter = new Location(43, 41);
        public Location MapCenter
        {
            get => mapCenter;
            set
            {
                mapCenter = value;
                RaisePropertyChanged(nameof(MapCenter));
            }
        }
        
        public UIElement CurrentMapLayer => Osm;


        public ObservableCollection<AirUnit> AirUnits { get; } = new ObservableCollection<AirUnit>();

        public MapViewModel()
        {
            AirUnits.Add(new AirUnit
            {
                Name = "DOLT-12 | RurouniJones",
                Location = new Location(44, 42)
            });
            // Create a timer with a two second interval.
            aTimer = new Timer(200);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            AirUnits[0].Location.Latitude -= 0.01;
        }
    }

    public class AirUnit : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
            
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private Location location;
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                RaisePropertyChanged(nameof(Location));
            }
        }
    }
}
