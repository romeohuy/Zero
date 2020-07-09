using System;
using System.ComponentModel.DataAnnotations;

namespace Zero.Stores
{
    public class StoreCreateDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
}