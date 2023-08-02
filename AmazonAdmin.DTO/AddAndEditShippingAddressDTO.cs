using AmazonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.DTO
{
    public class AddAndEditShippingAddressDTO
    {
        public int id { get; set; }
        public string street { get; set; }
        public string buildname { get; set; }
        public string userid { get; set; }
        public int? CityId { get; set; }
        public  City?city { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

    }
}
