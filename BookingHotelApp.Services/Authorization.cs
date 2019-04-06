using System;
using BookingHotelApp.DataAccess;
using BookingHotelApp.Models;

namespace BookingHotelApp.Services
{
    public class Authorization
    {
        #region Войти
        public static void SignIn(User user)
        {
            bool check = false;
            while (true)
            {
                Console.Clear();
                if (!check && !EnterLogin(user))
                {
                    Console.WriteLine("Пользователя с таким логином не существует, нажмите Enter чтобы ввести заново...");
                    Console.ReadLine();
                    continue;
                }
                if (check)
                {
                    Console.WriteLine($"Login: {user.Login}");
                }
                if (!EnterPassword(user))
                {
                    Console.WriteLine("\nВведенный Вами пароль не корректный, нажмите Enter чтобы ввести заново...");
                    Console.ReadLine();
                    check = true;
                }
                else
                {
                    Console.WriteLine("\nВход выполнен!");
                    break;
                }
            }
        }
        #endregion

        #region Ввод пароля
        private static bool EnterPassword(User user)
        {
            Console.Write("Password: ");
            user.Password = HideCharacter();
            return AccountsTableDataService.CheckForAvailability("Password", user.Password);
        }
        #endregion

        #region Ввод логина
        private static bool EnterLogin(User user)
        {
            Console.Write("Login: ");
            user.Login = Console.ReadLine();
            return AccountsTableDataService.CheckForAvailability("Login", user.Login);
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