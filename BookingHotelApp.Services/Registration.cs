using BookingHotelApp.DataAccess;
using BookingHotelApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BookingHotelApp.Services
{
    public class Registration
    {
        #region Регистрация
        public static void SignUp(User user, AccountsTableDataService dataService)
        {
            EnterLogin(user);
            EnterPassword(user);
            ConfirmPassword(user);
            EnterEmail(user);
            EnterPhoneNumber(user);

            string adoptedCode = Registration.VerificationAccount();

            System.Console.WriteLine("\nМы отправили на указанный Вами номер код верификации, введите его ниже...");

            while (true)
            {
                System.Console.Write("\nКод подтверждения: ");
                string verificationCode = System.Console.ReadLine();
                if (verificationCode == adoptedCode)
                {
                    System.Console.Clear();
                    dataService.AddUser(user);
                    System.Console.WriteLine("Поздравляем! Вы успешно прошли регистрацию");
                    break;
                }
                else
                {
                    System.Console.WriteLine("Неверный код верификации, нажмите Enter чтобы ввести заново...");
                    System.Console.ReadKey();
                    System.Console.Clear();
                    String stars = new string('*', user.Password.Length);
                    Registration.Show(user, 5, stars, stars);
                }
            }
        }
        #endregion

        #region Вывод на консоль
        public static void Show(User user, int numb, string stars = "", string stars2 = "")
        {
            for (int i = 0; i < numb; i++)
            {
                switch (i + 1)
                {
                    case 1: Console.Write("Login: " + user.Login); break;
                    case 2: Console.Write("\nPassword: " + stars); break;
                    case 3: Console.Write("\nConfirm Password: " + stars2); break;
                    case 4: Console.Write("\nEmail: " + user.Email); break;
                    case 5: Console.Write("\nPhone number (+XYYYZZZZZZZ): " + user.PhoneNumber); break;
                }
            }
        }
        #endregion

        #region Ввод логина
        public static void EnterLogin(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER = 1;

            Show(user, FIELD_NUMBER);
            user.Login = Console.ReadLine();
            
            Regex regex = new Regex(@"\W|[А-Яа-я]+");
            MatchCollection matches = regex.Matches(user.Login);
            if (matches.Count > 0)
            {
                Console.WriteLine("Логин содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                user.Login = "";
                Console.ReadKey();
                EnterLogin(user);
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                user.Login = "";
                Console.ReadKey();
                EnterLogin(user);
            }
            if (AccountsTableDataService.CheckForAvailability("Login", user.Login))
            {
                Console.WriteLine("Логин уже занят, нажмите Enter чтобы ввести заново...");
                user.Login = "";
                Console.ReadKey();
                EnterLogin(user);
            }
        }
        #endregion

        #region Ввод пароля
        public static void EnterPassword(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER_2 = 2;

            Show(user, FIELD_NUMBER_2);
            user.Password = HideCharacter();

            string pattern = @"(?=^.{6,32}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

            while (true)
            {
                if (user.Password.Length < 6 || user.Password.Length > 32)
                    Console.WriteLine("\nДлина пароля должна быть не меньше 6 символов и не больше 32 символов, нажмите Enter чтобы ввести заново...");
                else if (!(Regex.IsMatch(user.Password, pattern)))
                    Console.WriteLine("\nПароль должен содержать цифровой и спец символы, а также буквы верхнего, нижнего регистра, нажмите Enter чтобы ввести заново...");
                else
                    break;
                user.Password = "";
                Console.ReadKey();
                EnterPassword(user);
            }
        }
        #endregion

        #region Подтверждение пароля
        public static void ConfirmPassword(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER_3 = 3;
            String stars = new string('*', user.Password.Length);

            Show(user, FIELD_NUMBER_3, stars);

            string PasswordCopy = HideCharacter();

            if (user.Password != PasswordCopy)
            {
                Console.WriteLine("Пароли не совпадают, нажмите Enter чтобы ввести заново...");
                Console.ReadKey();
                ConfirmPassword(user);
            }
        }
        #endregion

        #region Ввод почты
        public static void EnterEmail(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER_4 = 4;
            String stars = new string('*', user.Password.Length);

            Show(user, FIELD_NUMBER_4, stars, stars);
            user.Email = Console.ReadLine();

            string pattern = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";

            if (!(Regex.IsMatch(user.Email, pattern)))
            {
                Console.WriteLine("Некорректный email, нажмите Enter чтобы ввести заново...");
                user.Email = "";
                Console.ReadKey();
                EnterEmail(user);
            }
            if (AccountsTableDataService.CheckForAvailability("Email", user.Email))
            {
                Console.WriteLine("Данная почта уже существует в базе данных, нажмите Enter чтобы ввести заново...");
                user.Email = "";
                Console.ReadKey();
                EnterEmail(user);
            }
        }
        #endregion

        #region Ввод номера
        public static void EnterPhoneNumber(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER_5 = 5;
            String stars = new string('*', user.Password.Length);

            Show(user, FIELD_NUMBER_5, stars, stars);
            user.PhoneNumber = Console.ReadLine();

            string pattern = @"^\+?[7]\d{10}$";

            if (!(Regex.IsMatch(user.PhoneNumber, pattern, RegexOptions.IgnoreCase)))
            {
                Console.WriteLine("Телефонный номер содержит недопустимые символы или введен не корректно, нажмите Enter чтобы ввести заново...");
                user.PhoneNumber = "";
                Console.ReadKey();
                EnterPhoneNumber(user);
            }
            if (AccountsTableDataService.CheckForAvailability("PhoneNumber", user.PhoneNumber))
            {
                Console.WriteLine("Номер уже использовался, нажмите Enter чтобы ввести заново...");
                user.PhoneNumber = "";
                Console.ReadKey();
                EnterPhoneNumber(user);
            }
        }
        #endregion

        #region Верификация аккаунта
        public static string VerificationAccount()
        {
            // Find your Account Sid and Token at twilio.com/console
            const string accountSid = "AC9dc0d107f661a29db6e2db341af2beb4";
            const string authToken = "d7d8170696c1de5bda1e96b4a9347018";

            Random rnd = new Random();
            string code = Convert.ToString(rnd.Next(100, 1000));

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("+18432585652"),
                body: code,
                to: new Twilio.Types.PhoneNumber("+77719777518") // user.PhoneNumber
            );

            // Console.WriteLine(message.Sid);
            return code;
        }
        #endregion

        #region Скрытие пароля
        public static string HideCharacter()
        {
            string text = "";
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (text.Length == 0)
                        continue;
                    text = text.Remove(text.Length - 1);
                    Console.Write("\b \b");
                }
                else
                {
                    text += keyInfo.KeyChar;
                    Console.Write("*");
                }
            }

            return text;
        }
        #endregion
    }
}