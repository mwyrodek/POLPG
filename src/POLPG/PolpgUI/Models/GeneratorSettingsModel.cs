using System.ComponentModel;
using System.Runtime.CompilerServices;
using PolpgUI.Annotations;

namespace PolpgUI.Models
{
    public class GeneratorSettingsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string pageName;
        private string parentName;
        private bool isInheritance;
        private string generatedCode;

        public string PageName
        {
            get => this.pageName;

            set
            {
                this.pageName = value;
                this.OnPropertyChanged(nameof(this.PageName));
            }

        }

        public bool IsInheritance
        {
            get => this.isInheritance;
            set
            {
                this.isInheritance = value;
                this.OnPropertyChanged(nameof(this.IsInheritance));
            }
        }

        public string ParentName
        {
            get => this.parentName;
            set
            {
                this.parentName = value;
                this.OnPropertyChanged(nameof(this.ParentName));
            }
        }


        public string GeneratedCode
        {
            get => this.generatedCode;
            set
            {
                this.generatedCode = value;
                this.OnPropertyChanged(nameof(this.GeneratedCode));
            }
        }

        //// driver name

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}