using System;
using Common.Extensions;
using Contracts.Classes;
using Contracts.Events;
using Models;

namespace Contracts.MappingExtensions
{
    public static class EventMappingExtensions
    {
        public static Event ToEntity(this EventModel instance)
        {
            if (instance.IsNull())
                return null;

            Event model = null;

            var classInstance = instance as ClassModel;
            if (classInstance.IsNotNull())
                model = classInstance.ToEntity();

            var standAloneEventInstance = instance as StandAloneEventModel;
            if (standAloneEventInstance.IsNotNull())
                model = standAloneEventInstance.ToEntity();

            if (model.IsNull())
                throw new ArgumentException(
                    "Can't handle converting to entity for type {0}".FormatWith(instance.GetType()), "instance");

            model.Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull();
            model.Name = instance.Name;
            model.EndTime = instance.EndTime;
            model.StartTime = instance.StartTime;
            model.ClassCapacity = instance.ClassCapacity;
            model.RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull();
            model.ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull();

            return model;
        }

        public static EventModel ToModel(this Event instance)
        {
            if (instance.IsNull())
                return null;

            EventModel model = null;

            var classInstance = instance as Class;
            if (classInstance.IsNotNull())
                model = classInstance.ToModel();

            var standAloneEventInstance = instance as StandAloneEvent;
            if (standAloneEventInstance.IsNotNull())
                model = standAloneEventInstance.ToModel();

            if (model.IsNull())
                throw new ArgumentException(
                    "Can't handle converting to model for type {0}".FormatWith(instance.GetType()), "instance");

            model.Id = instance.Id;
            model.Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull();
            model.Name = instance.Name;
            model.EndTime = instance.EndTime;
            model.StartTime = instance.StartTime;
            model.ClassCapacity = instance.ClassCapacity;
            model.Room = instance.Room.ToStripedModel();
            model.RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull();
            model.ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull();

            return model;

        }

        private static StandAloneEventModel ToModel(this StandAloneEvent instance)
        {
            if (instance.IsNull())
                return null;

            return new StandAloneEventModel
            {
                Id = instance.Id,
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                Room = instance.Room.ToStripedModel(),
                Price = instance.Price,
                IsPrivate = instance.IsPrivate
            };
        }

        private static ClassModel ToModel(this Class instance)
        {
            if (instance.IsNull())
                return null;

            return new ClassModel
            {
                Block = instance.Block.ToStripedModel(),
                ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull()
            };
        }

        internal static ClassModel ToStripedModel(this Class instance)
        {
            if (instance.IsNull())
                return null;

            return new ClassModel
            {
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                Room = instance.Room.ToStripedModel(),
                ClassCapacity = instance.ClassCapacity,
                Id = instance.Id
            };
        }

        private static StandAloneEventModel ToStripedModel(this StandAloneEvent instance)
        {
            if (instance.IsNull())
                return null;

            return new StandAloneEventModel
            {
                Price = instance.Price,
                IsPrivate = instance.IsPrivate,
                ClassCapacity = instance.ClassCapacity
            };
        }

        public static EventModel ToStripedModel(this Event instance)
        {
            if (instance.IsNull())
                return null;

            EventModel model = null;

            var classInstance = instance as Class;
            if (classInstance.IsNotNull())
                model = classInstance.ToStripedModel();

            var standAloneEventInstance = instance as StandAloneEvent;
            if (standAloneEventInstance.IsNotNull())
                model = standAloneEventInstance.ToStripedModel();

            if (model.IsNull())
                throw new ArgumentException(
                    "Can't handle converting to striped model for type {0}".FormatWith(instance.GetType()), "instance");

            model.Id = instance.Id;
            model.Name = instance.Name;
            model.EndTime = instance.EndTime;
            model.StartTime = instance.StartTime;
            model.ClassCapacity = instance.ClassCapacity;

            return model;
        }
    }
}