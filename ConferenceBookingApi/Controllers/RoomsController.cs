using ConferenceBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBookingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllRooms() => Ok(DataStore.Rooms);
    
    [HttpGet("{id}")]
    public IActionResult GetRoomById(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        return room == null ? NotFound() : Ok(room);
    }

    [HttpPost]
    public IActionResult CreateRoom(Room room)
    {
        room.Id = DataStore.Rooms.Any() ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
        DataStore.Rooms.Add(room);
        return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, Room updatedRoom)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        room.Name = updatedRoom.Name;
        room.Capacity = updatedRoom.Capacity;
        room.BasePricePerHour = updatedRoom.BasePricePerHour;
        return Ok(room);
    }
    
    [HttpDelete]
    public IActionResult DeleteAllRooms()
    {
        DataStore.Rooms.Clear();
        DataStore.Bookings.Clear(); 
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        DataStore.Rooms.Remove(room);
        DataStore.Bookings.RemoveAll(b => b.RoomId == id); 
        return NoContent();
    }
    
    [HttpGet("{id}/bookings")]
    public IActionResult GetBookingsForRoom(int id)
    {
        if (!DataStore.Rooms.Any(r => r.Id == id)) return NotFound("Зал не знайдено");
        var roomBookings = DataStore.Bookings.Where(b => b.RoomId == id).ToList();
        return Ok(roomBookings);
    }
}