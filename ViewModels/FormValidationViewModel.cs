using MauiStylesDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStylesDemo.ViewModels
{
    public class FormValidationViewModel:ViewModelBase
    {
        #region שם
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion
        #region גיל
        private bool showAgeError;

        public bool ShowAgeError
        {
            get => showAgeError;
            set
            {
                showAgeError = value;
                OnPropertyChanged("ShowAgeError");
            }
        }

        private double? age;

        public double? Age
        {
            get => age;
            set
            {
                age = value;
                ValidateAge();
                OnPropertyChanged("Age");
            }
        }

        private string ageError;

        public string AgeError
        {
            get => ageError;
            set
            {
                ageError = value;
                OnPropertyChanged("AgeError");
            }
        }


        private void ValidateAge()
        {
            this.ShowAgeError = !Age.HasValue || Age <= 13;
        }
        #endregion
        #region תאריך לידה
        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                ValidateDate();
                OnPropertyChanged("Date");
            }
        }

        private DateTime today;

        private string dateError;

        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                OnPropertyChanged("DateError");
            }
        }

        private bool showDateError;

        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged("ShowDateError");
            }
        }

        private void ValidateDate()
        {
            TimeSpan diff = today.Subtract(date);
            this.ShowDateError = diff.TotalDays < 13 * 364.25;
        }
        #endregion

        #region סיסמא
        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string passwordConfirmation;

        public string PasswordConfirmation
        {
            get => passwordConfirmation;
            set
            {
                passwordConfirmation = value;
                ValidatePasswordConfirmation();
                OnPropertyChanged("PasswordConfirmation");
            }
        }


        private void ValidatePasswordConfirmation()
        {
            this.ShowPasswordError = Password != PasswordConfirmation;
        }
        #endregion

        public FormValidationViewModel()
        {
            this.today = DateTime.Now;
            this.DateError = "הגיל חייב להיות גדול מ-13";
            this.ShowDateError = false;
            this.PasswordError = "הסיסמאות לא שוות";
            this.ShowPasswordError = false;
            this.NameError = "זהו שדה חובה";
            this.ShowNameError = false;
            this.AgeError = "הגיל חייב להיות גדול מ 13";
            this.ShowAgeError = false;
            this.SaveDataCommand = new Command(() => SaveData());
        }

        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateAge();
            ValidateName();
            ValidatePasswordConfirmation();
            ValidateDate();

            //check if any validation failed
            if (ShowAgeError ||
                ShowNameError || showPasswordError || ShowDateError)
                return false;
            return true;
        }

        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            if (ValidateForm())
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", "הנתונים נבדקו ונשמרו", "אישור", FlowDirection.RightToLeft);
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);
        }
    }
}
