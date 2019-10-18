module Refunds

open System
open FSharp.Data
open FSharp.Json

let ``Not implemented`` a = failwith "not implemented"

module DB =
    let insert<'T> (table: string) (entity: 'T): unit = failwith "no impl"
    let select<'T> (table: string): 'T list = failwith "no impl"

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

    let savePayments pp =
        pp
        |> List.filter (fun p -> p.amount < 0m)
        |> List.iter (toRefund >> DB.insert "refunds")

    let download customer period iRequestPayments =
        (customer, period)
        ||> iRequestPayments
        |> savePayments

/// ------
/// Client
/// ------

module Client =
    open Billing
    open Domain

    let adjustTz (d: DateTime) = d.AddHours(-5.0)
    let format (d: DateTime) = sprintf "%O" d
    let prepare = adjustTz >> format

    let httpClientImpl url formData =
        Http.RequestString(url, httpMethod = "POST", body = FormValues formData)


    let requestPaymentsImpl (customer: int) (p: BillingPeriod) iHttpClient =
        let request =
            [ ("customer_id", customer |> string)
              ("from", p.from |> prepare)
              ("to", p.till |> prepare) ]
        let url = "https://fancybilling.kmaooadtech.io"
        
        (url,request) 
        ||> iHttpClient 
        |> Json.deserialize<Payment list>

    let customer = 404

    let period =
        { from = DateTime(2019, 10, 1)
          till = DateTime(2019, 10, 31) }

    let httpPaymentClient (customer: int) (p: BillingPeriod) = 
        (customer, period, httpClientImpl) |||> requestPaymentsImpl

    (customer, period, httpPaymentClient) |||> download
