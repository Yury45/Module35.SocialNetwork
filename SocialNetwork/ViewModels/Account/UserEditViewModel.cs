using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.ViewModels.Account
{
    public class UserEditViewModel
    {
        [Required]
        [Display(Name = "Id пользователя")]
        public string UserId { get; set; }


        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Отчество", Prompt = "Введите отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "example.com")]
        public string EmailReg { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Поле \"Дата рождения\" обязательно для заполнения")]
        [Display(Name = "Дата рождения", Prompt = "число/месяц/год")]
        public DateTime Bitrthday { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Фото", Prompt = "Ссылка на фотографию")]
        public string Image { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Статус", Prompt = "Поделитесь текущей активностью")]
        public string Status { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "О себе", Prompt = "Расскажите немного о себе")]
        public string About { get; set; }

        public string UserName => EmailReg;
    }
}
