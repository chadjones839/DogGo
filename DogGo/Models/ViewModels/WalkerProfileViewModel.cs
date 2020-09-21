using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public List<Walk> Walks { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}