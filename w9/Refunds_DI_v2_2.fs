module RefundsV2x2

open System
open FSharp.Data
open FSharp.Json


module DB =
    let insert<'T> _ _ = ()
    let select<'T> _: 'T list = []

module Domain =
    type Refund =
        { amount: decimal
          customer: int
          date: DateTime }

    type BillingPeriod =
        { from: DateTime
          till: DateTime }

module Billing =

    type Payment =
        { customer_id: int
          date: DateTime
          amount: decimal }

    let toRefund p =
        { Domain.Refund.amount = -p.amount
          Domain.Refund.customer = p.customer_id
          Domain.Refund.date = p.date }

    let resultProcessorImpl pp =
        pp
        |> List.filter (fun p -> p.amount < 0m)
        |> List.iter (toRefund >> DB.insert "refunds")

    let download customer period iDownloader iResultProcessor =
        (customer, period)
        ||> iDownloader
        |> iResultProcessor

/// ------
/// Client
/// ------

module Client =
    open Billing
    open Domain

    let adjustTz (d: DateTime) = d.AddHours(-5.0)
    let format (d: DateTime) = sprintf "%O" d
    let prepare = adjustTz >> format

    let httpClientImpl url formData = Http.RequestString(url, httpMethod = "POST", body = FormValues formData)



    [<Literal>]
    let Url = "https://fancybilling.kmaooadtech.io"

    let requestFactoryImpl (customer: int) (p: BillingPeriod) =
        let body =
            [ ("customer_id", customer |> string)
              ("from", p.from |> prepare)
              ("to", p.till |> prepare) ]
        (Url, body)

    let responseHandlerImpl (response: string): Payment list = response |> Json.deserialize<Payment list>

    let requestPaymentsImpl c p iRequestFactory iHttpClient iResponseHandler =
        (c, p)
        ||> iRequestFactory
        ||> iHttpClient
        |> iResponseHandler

    let customer = 404

    let period =
        { from = DateTime(2019, 10, 1)
          till = DateTime(2019, 10, 31) }

    // Mixed DI and params
    let httpPaymentClient (customer: int) (p: BillingPeriod) =
        (customer, period) // parameters
        ||> requestPaymentsImpl
        <||| (requestFactoryImpl, httpClientImpl, responseHandlerImpl) // dependencies

    // Split DI from passing params (objects imitation!)
    let _DI_download c p =
        (c, p) // parameters - pass through
        ||> download
        <|| (httpPaymentClient, resultProcessorImpl) // inject dependencies

    (customer, period) ||> _DI_download 
