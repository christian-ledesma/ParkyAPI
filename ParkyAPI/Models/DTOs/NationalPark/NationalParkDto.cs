using System;

namespace ParkyAPI.Models.DTOs.NationalPark
{
    public class NationalParkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
    }
}
