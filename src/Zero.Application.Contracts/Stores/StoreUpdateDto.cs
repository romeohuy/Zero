using System;
using System.ComponentModel.DataAnnotations;

namespace Zero.Stores
{
    public class StoreUpdateDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
}