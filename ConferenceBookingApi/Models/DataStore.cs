namespace ConferenceBookingApi.Models;

public static class DataStore
{
    public static List<Room> Rooms { get; set; } = new List<Room>();
    public static List<Booking> Bookings { get; set; } = new List<Booking>();
}