using LDST.Domain.Common.Models;
using LDST.Domain.Playground;
using LDST.Domain.Playground.ValueObjects;
using LDST.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDST.Domain.Guest.ValueObjects;

public sealed class GuestRatingId : ValueObject
{
    public Guid Value { get; }

    private GuestRatingId(Guid value)
    {
        Value = value;
    }

    public static GuestRatingId CreateUnique()
    {
        return new GuestRatingId(new Guid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}