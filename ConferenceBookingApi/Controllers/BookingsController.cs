using ConferenceBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBookingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAllBookings() => Ok(DataStore.Bookings);

    [HttpGet("{id}")]
    public IActionResult GetBookingById(int id)
    {
        var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
        return booking == null ? NotFound() : Ok(booking);
    }

    [HttpPost]
    public IActionResult CreateBooking(Booking booking)
    {
        if (booking.StartDate < DateTime.Now)
        {
            return BadRequest("Неможливо створити бронювання в минулому часі.");
        }

        if (!DataStore.Rooms.Any(r => r.Id == booking.RoomId))
        {
            return BadRequest("Неможливо створити бронювання: вказаного залу не існує.");
        }

        booking.Id = DataStore.Bookings.Any() ? DataStore.Bookings.Max(b => b.Id) + 1 : 1;
        DataStore.Bookings.Add(booking);
        return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBooking(int id, Booking updatedBooking)
    {
        if (updatedBooking.StartDate < DateTime.Now)
        {
            return BadRequest("Неможливо перенести бронювання на минулий час.");
        }
        
        var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
        if (booking == null) return NotFound();

        if (!DataStore.Rooms.Any(r => r.Id == updatedBooking.RoomId))
        {
            return BadRequest("Вказаного залу не існує.");
        }

        booking.RoomId = updatedBooking.RoomId;
        booking.StartDate = updatedBooking.StartDate;
        booking.DurationInHours = updatedBooking.DurationInHours;
        return Ok(booking);
    }

    [HttpDelete]
    public IActionResult DeleteAllBookings()
    {
        DataStore.Bookings.Clear();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteBooking(int id)
    {
        var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
        if (booking == null) return NotFound();

        DataStore.Bookings.Remove(booking);
        return NoContent();
    }
}