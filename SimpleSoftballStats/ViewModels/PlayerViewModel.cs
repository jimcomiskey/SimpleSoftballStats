using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.ViewModels
{
    public class PlayerViewModel : IObjectWithState
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ObjectState ObjectState { get; set;}
        
    }
}