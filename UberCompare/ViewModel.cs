using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Threading;
using System.Reflection;
using UberCompare.Models;
namespace UberCompare
{
    public class ViewModel : ViewModelBase
    {

        private MainModel _model = new MainModel();
        private Comparers.IComparer compare = new Comparers.TextComparer();
        public bool UseConversion { get { return _model.CharConversion; } set { _model.CharConversion = value; OnPropertyChanged("UseConversion"); } }
        public string LogText { get { return _model.LogText; } }
        public string LeftFile { get { return _model.LeftFileName; } set { _model.LeftFileName = value; OnPropertyChanged("LeftFile"); } }
        public string RightFile { get { return _model.RightFileName; } set { _model.LeftFileName = value; OnPropertyChanged("RightFile"); } }

        private void ClearAll()
        {
            _model = new MainModel();
            OnPropertyChanged("LogText");
        }

        private List<string> Compare()
        {
            compare.UseConversion = _model.CharConversion;

            return compare.Compare(_model.LeftFileHandler.GetStream(), _model.RightFileHandler.GetStream());

        }

        private void SaveFiles()
        {
            var leftWrite = Task.Run(() =>
            {
                _model.LeftFileHandler.SaveFile(_model.LeftFileName);
            });
            var rightWrite = Task.Run(() =>
            {
                _model.LeftFileHandler.SaveFile(_model.LeftFileName);
            });

            rightWrite.Wait();
            leftWrite.Wait();

            Log("Save complete.");

        }
        private void SelectFiles(bool left)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Dat Files (*.dat)|*.dat|Text Files (.txt)|*.txt";
            dlg.FilterIndex = 1;
            dlg.InitialDirectory = @"C:\";
            dlg.Title = "Select Right File...";

            if (left)
            {
                dlg.Title = "Select Left File...";
            }
            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                if (left)
                {
                    _model.LeftFileName = dlg.FileName;

                }
                else
                {
                    _model.LeftFileName = dlg.FileName;

                }
            }
            else
            {
                ClearAll();
                return;
            }

            OnPropertyChanged("LeftFile");
            OnPropertyChanged("RightFile");
        }

        private void LoadFiles()
        {
            if (!_model.LeftFileHandler.LoadFile(LeftFile))
            {
                Log("Failed to open left file.");
            }
            if (!_model.RightFileHandler.LoadFile(RightFile))
            {
                Log("Failed to open left file.");
            }
        }

        private void CompareFiles()
        {
            List<string> errors = Compare();
            if (errors.Count == 0)
            {
                Log("The files " + _model.LeftFileName + " and " + _model.RightFileName + " contains the same data...");
            }
            else
            {
                Log("The files " + _model.LeftFileName + " and " + _model.RightFileName + " does not contain the same data.");
                Log("Errors: ");
                errors.ForEach(Log);
            }

        }
        private void Log(string msg)
        {
            _model.LogText += msg + "\r\n";
            OnPropertyChanged("LogText");
        }
        public ICommand SelectRightFilesButtonClickCommand
        {
            get
            {
                return new RelayCommand(o => { SelectFiles(false); }, o => true);
            }
        }
        public ICommand SelectLeftFilesButtonClickCommand
        {
            get
            {
                return new RelayCommand(o => { SelectFiles(true); }, o => true);
            }
        }

        public ICommand CompareFilesButtonClickCommand
        {
            get
            {
                return new RelayCommand(o => { CompareFiles(); }, o => true);
            }
        }

    }
}
