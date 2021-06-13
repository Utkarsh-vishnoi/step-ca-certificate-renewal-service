using System;

public class Configuration
{
    public string certificate_file { get; set; }
    public string key_file { get; set; }
    public string root_certificate_file { get; set; }
    public string ca_server_url { get; set; }
    public int check_interval { get; set; }
    public string export_password { get; set; }
}
