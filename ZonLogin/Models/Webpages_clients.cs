//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZonLogin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Webpages_clients
    {
        public int Webpages_clientsId { get; set; }
        public int userId { get; set; }
        public string clientId { get; set; }
        public System.DateTime authDate { get; set; }
    
        public virtual Clients Clients { get; set; }
    }
}
