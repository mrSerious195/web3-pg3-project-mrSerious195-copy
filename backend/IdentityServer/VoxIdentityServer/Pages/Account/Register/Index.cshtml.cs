// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using VoxIdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VoxIdentityServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using IdentityModel;
using IdentityModel.Client;

namespace VoxIdentityServer.Pages.Register;

[SecurityHeaders]
[AllowAnonymous]
public class Index: PageModel
{
    /*UserManager<VoxUser> userManager,*/
    private readonly UserManager<VoxUser> _userManager;
     private readonly IIdentityServerInteractionService _interaction;
    private readonly IConfiguration _config;
       private readonly IHttpClientFactory _httpClientFactory;

       [BindProperty]
    public RegisterModel Input { get; set; } = default!;



    public string[] PossibleCountries{get;set;} = [];
 
 public Index(IConfiguration config,UserManager<VoxUser> userManager,IIdentityServerInteractionService interaction,IHttpClientFactory httpClientFactory)
    {
        _interaction=interaction;
        _userManager=userManager;
        _httpClientFactory=httpClientFactory;
        _config=config;
        //Input.PossibleCountries
    }


    public class countryDataFromApi
    {
        public required Name name{get;set;}
    }

    public class Name
    {
         public required string common{get;set;}="";
    }


    public async Task<IActionResult> OnGet(string returnUrl)
    {
        var client = _httpClientFactory.CreateClient();
        var countries = await client.GetFromJsonAsync<countryDataFromApi[]>("https://restcountries.com/v3.1/all?fields=name");
        if(countries==null)
        throw new Exception("countries is null");
        PossibleCountries=countries.Select(c=>c==null||c.name==null?"missing":c.name.common).Order().ToArray();
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

       await BuildModelAsync(returnUrl);

        return Page();
    }
    public async Task<IActionResult> OnPost(string returnUrl)
    {      
         var userToAdd = new VoxUser
                {
                    UserName = Input.Username,
                    EmailConfirmed = true
                };
        var result = _userManager.CreateAsync(userToAdd,Input.Password ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                    var userClaims =  new Claim[]{
                            new Claim(JwtClaimTypes.Name, Input.Username),
                        };
                 result = _userManager.AddClaimsAsync(userToAdd,userClaims).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }


                var userForVoxDataServer = new UserContract()
                {
                    IdentityServerId=userToAdd.Id,
                    Name=userToAdd.UserName,
                    Country=Input.Country
                };

     var selfAsclient = _httpClientFactory.CreateClient("IDserver");
      var voxDataApi = _httpClientFactory.CreateClient("VoxData");
var tokenClient = _httpClientFactory.CreateClient();
var disco = await selfAsclient.GetDiscoveryDocumentAsync();
var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "m2m-client",
                ClientSecret = _config["m2msecret"],
                Scope = "VoxelGallery.Api.m2m"
            });
        voxDataApi.SetBearerToken(tokenResponse.AccessToken ?? throw new Exception("Could not obtain token."));


               var resultvox2 = await voxDataApi.PostAsJsonAsync("api/Users",userForVoxDataServer);
                
                 if (!resultvox2.IsSuccessStatusCode)
                {
                   await _userManager.DeleteAsync(userToAdd);
                    throw new Exception($"ERROR {resultvox2.StatusCode} [{resultvox2.ReasonPhrase}]");
                }
        
        return Redirect(Input.ReturnUrl ?? "~/");
    }

    private async Task BuildModelAsync(string? returnUrl)
    {
        Input = new RegisterModel
        {
            ReturnUrl = returnUrl
        };
    }



        private class UserContract
    {
     public  string IdentityServerId {get;set;}
      public string Name {get;set;}

        public string Country {get;set;}
    }
}

