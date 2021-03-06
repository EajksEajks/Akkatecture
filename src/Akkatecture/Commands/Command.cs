﻿using System;
using Akkatecture.Aggregates;
using Akkatecture.Core;
using Akkatecture.ValueObjects;

namespace Akkatecture.Commands
{
    public abstract class Command<TAggregate, TIdentity, TSourceIdentity> :
        ValueObject,
        ICommand<TAggregate, TIdentity, TSourceIdentity>
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
        where TSourceIdentity : ISourceId
    {
        public TSourceIdentity SourceId { get; }
        public TIdentity AggregateId { get; }

        protected Command(TIdentity aggregateId, TSourceIdentity sourceId)
        {
            if (aggregateId == null) throw new ArgumentNullException(nameof(aggregateId));
            if (sourceId == null) throw new ArgumentNullException(nameof(sourceId));

            AggregateId = aggregateId;
            SourceId = sourceId;
        }

        public ISourceId GetSourceId()
        {
            return SourceId;
        }
    }

    public abstract class Command<TAggregate, TIdentity> :
        Command<TAggregate, TIdentity, ISourceId>,
        ICommand<TAggregate, TIdentity>
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        protected Command(TIdentity aggregateId)
            : this(aggregateId, CommandId.New)
        {
        }

        protected Command(TIdentity aggregateId, ISourceId sourceId)
            : base(aggregateId, sourceId)
        {
        }
    }
}