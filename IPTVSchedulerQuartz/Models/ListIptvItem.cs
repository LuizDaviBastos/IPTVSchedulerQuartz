namespace IPTVSchedulerQuartz.Models
{
    public class ListIptvItem
    {
        public long? Id { get; set; }
        public long? Member_Id { get; set; }
        public string Password { get; set; }
        public int? Max_Connections { get; set; }
        public long? Exp_Date { get; set; }
        public int? Admin_Enabled { get; set; }
        public int? Enabled { get; set; }
        public object Stream_Id { get; set; }
        public object Server_Id { get; set; }
        public object Container { get; set; }
        public object User_Agent { get; set; }
        public object User_Ip { get; set; }
        public object Date_Start { get; set; }
        public object Date_End { get; set; }
        public object Geoip_Country_Code { get; set; }
        public object Isp { get; set; }
        public object Stream_Display_Name { get; set; }
        public object Total_Connections { get; set; }
        public object User_Online_Att { get; set; }
        public bool? User_Online { get; set; }
        public bool? Status_List { get; set; }
        public long? Reseller_Id { get; set; }
        public string Reseller_Username { get; set; }
        public string Reseller_Full_Name { get; set; }
        public string Dateserver { get; set; }

        /*
          {
            "id": 2483810,
            "member_id": 4528,
            "username": "Josiziviani",
            "password": "t91783xzay",
            "max_connections": 1,
            "exp_date": 1615690712,
            "admin_enabled": 1,
            "enabled": 1,
            "stream_id": null,
            "server_id": null,
            "container": null,
            "user_agent": null,
            "user_ip": null,
            "date_start": null,
            "date_end": null,
            "geoip_country_code": null,
            "isp": null,
            "stream_display_name": null,
            "total_connections": null,
            "user_online_att": null,
            "user_online": false,
            "status_list": true,
            "reseller_id": 2037,
            "reseller_username": "LuizDavi",
            "reseller_full_name": "Luiz Davi",
            "dateserver": "1611510252"
        }
         */
    }
}
