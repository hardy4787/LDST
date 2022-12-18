using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDST.Domain.EFModels;

public class CitySport
{
    public int CityId { get; set; }
    public City City { get; set; } = null!;
    public int SportId { get; set; }
    public Sport Sport { get; set; } = null!;
}

