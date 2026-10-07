using System.ComponentModel.DataAnnotations;

namespace ConferenceBookingApi.Models;

public class Booking
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "ID залу є обов'язковим.")]
    [Range(1, int.MaxValue, ErrorMessage = "Некоректний ідентифікатор залу.")]
    public int RoomId { get; set; }
    
    [Required(ErrorMessage = "Дата початку бронювання є обов'язковою.")]
    public DateTime StartDate { get; set; }
    
    [Range(1, 24, ErrorMessage = "Бронювання має тривати від 1 до 24 годин.")]
    public int DurationInHours  { get; set; }
}