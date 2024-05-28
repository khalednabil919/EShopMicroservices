﻿namespace Basket.API.Dtos;
public class BasketCheckoutDto
{
    public string UserName { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public double TotalPrice { get; set; } = default!;

    //Shipping and BillingAddress
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAdress { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;

    //Payment
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string Expiration { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public int PaymentMethod { get; set; } = default!;

}
