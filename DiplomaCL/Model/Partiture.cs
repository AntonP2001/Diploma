using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaCL.Model
{
    public class Partiture : INotifyPropertyChanged
    {
        private string author;
        private string name;
        private string style;
        private string workType;
        private string instrumentation;
        private string language;
        private byte[] file;
        private byte[] image;
        private Catalogue catalogue;

        public int Id { get; set; }
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Style
        {
            get { return style; }
            set
            {
                style = value;
                OnPropertyChanged("Style");
            }
        }
        public string WorkType
        {
            get { return workType; }
            set
            {
                workType = value;
                OnPropertyChanged("WorkType");
            }
        }
        public string Instrumentation
        {
            get { return instrumentation; }
            set
            {
                instrumentation = value;
                OnPropertyChanged("Instrumentation");
            }
        }
        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged("Language");
            }
        }
        public byte[] File
        {
            get { return file; }
            set
            {
                file = value;
                OnPropertyChanged("File");
            }
        }
        public byte[] Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        public Catalogue Catalogue
        {
            get => catalogue;
            set
            {
                catalogue = value;
                OnPropertyChanged("Catalogue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}