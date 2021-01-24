using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace IPTVSchedulerQuartz.Models
{
    public class ListIptv
    {
        public IList<ListIptvItem>? Items { get; set; }
        public int? TotalCountUsers { get; set; }
        public int? TotalUsersActive { get; set; }
        public int? TotalUsersExpired { get; set; }
        public int? TotalUsersOnline { get; set; }
        public int? TotalUsersOffline { get; set; }
        public int? LimitPerPage { get; set; }
        public int? Page { get; set; }
    }
}
