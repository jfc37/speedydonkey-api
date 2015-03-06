using System;
using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual Person Person { get; set; }
    }

    public interface IEntity { }
    public interface IAccount
    {
        string Email { get; set; }
        string Password { get; set; }
    }

    public interface IUser
    {
        IAccount Account { get; set; }
        string FirstName { get; set; }
        string Surname { get; set; }
        ISchedule Schedule { get; set; } 
        IList<IBlock> EnroledBlocks { get; set; }
        IList<IPass> Passes { get; set; }  
    }

    public interface ITeacher : IUser
    {
        IList<IAvailableTime> AvailableTimes { get; set; }
        ISchedule TeachingSchedule { get; set; } 
    }

    public interface IAvailableTime
    {
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
    }

    public interface ISchedule
    {
        IList<IBooking> Bookings { get; set; } 
    }

    public interface IBooking
    {
        IRoom Room { get; set; }
        IEvent Event { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        IList<ITeacher> Teachers { get; set; } 
        IList<IUser> Students { get; set; } 
    }

    public interface IPass
    {
        decimal Price { get; set; }

    }

    public interface IClipPass : IPass
    {
        int ClipsRemaining { get; set; }
    }

    public interface IUnlimitedPass : IPass
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }

    public interface IRoom
    {
        string Location { get; set; }
        IList<IBooking> Bookings { get; set; }
    }

    public interface ILevel
    {
        string Name { get; set; }
        IRoom Room { get; set; }
        IList<ITeacher> Teachers { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        int ClassesInBlock { get; set; }
    }

    public interface IBlock
    {
        IList<IUser> EnroledStudents { get; set; }
        ILevel Level { get; set; }
        IList<IClass> Classes { get; set; } 
    }

    public interface IEvent
    {
        IList<ITeacher> Teachers { get; set; } 
        IList<ITeacher> RegisteredStudents { get; set; } 
        IBooking Booking { get; set; }
    }

    public interface IClass : IEvent
    {
        IList<IUser> ActualStudents { get; set; }
        IBlock Block { get; set; }
    }
}
