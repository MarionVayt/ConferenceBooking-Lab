using System.ComponentModel.DataAnnotations;

namespace ConferenceBookingApi.Models;

public class Room
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Назва залу є обов'язковою.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Назва має містити від 3 до 100 символів.")]
    public string Name { get; set; } = string.Empty;
    
    [Range(1, 500, ErrorMessage = "Місткість залу має бути від 1 до 500 осіб.")]
    public int Capacity { get; set; }
    
    [Range(0.01, 10000, ErrorMessage = "Базова ціна за годину має бути більшою за 0.")]
    public decimal BasePricePerHour { get; set; }
}
