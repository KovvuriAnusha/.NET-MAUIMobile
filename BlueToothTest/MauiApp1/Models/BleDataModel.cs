using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class BleDataModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DiscoveredAt { get; set; } = DateTime.Now;
    }
}
