using System;

namespace VoxStripePaymentServer.models;

public class StripeOptions
{

     public string Secretkey {get;set;}
    public string ApiKey {get;set;}
     public string SuccessUrl {get;set;}
     public string CancelUrl {get;set;}
}
