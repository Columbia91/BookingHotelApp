using System.ComponentModel.DataAnnotations;

namespace BookingHotelApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 3, 
            ErrorMessage = "Мин. длина логина должна быть 3, макс. 20, нажимте Enter чтобы ввести заново...")]
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
/*Standart - стандартный номер состоящий из одной комнаты.

Bedroom - номер со спальней. Номер состоит из двух комнат. В одной из них стоит кровать.

Luxe - номер из двух и более жилых комнат и полного санузла; рассчитан на проживание одного-двух человек.

 King Suite - роскошные номера отеля, состоящие из нескольких комнат, спален, туалетов.*/
