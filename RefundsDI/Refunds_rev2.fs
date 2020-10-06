module Refunds2

open System
open FSharp.Data
open FSharp.Json

module DB =
    let insert<'T> (table: string) (entity: 'T): unit = failwith "not implemented"
    let select<'T> (table: string): 'T list = failwith "not implemented"

module Domain =
    type Refund =
        { amount: decimal
          customer: int
          date: DateTime }

    type BillingPeriod = { from: DateTime; till: DateTime }

module Billing =

    open Domain 
    
    type Payment =
        { customer_id: int
          date: DateTime
          amount: decimal }

    [<Literal>]
    let Url = "https://fancybilling.kmaooadtech.io"

    let billingPeriod (from: DateTime) (till: DateTime): (BillingPeriod) = { from = from; till = till }

    let requestPayments (customer: int) (p: BillingPeriod) =
        let request =
            [ ("customer", customer |> string)
              ("from", p.from |> string)
              ("till", p.till |> string) ]

        Http.RequestString(Url, httpMethod = "POST", body = FormValues request)
        |> Json.deserialize<Payment list>

    let toRefund p =
        { Refund.amount = -p.amount
          Refund.customer = p.customer_id
          Refund.date = p.date }

    let savePayments pp =
        pp
        |> List.filter (fun p -> p.amount < 0m)
        |> List.map toRefund
        |> List.iter (DB.insert "refunds")

    let download (customer: int) (from: DateTime) (till: DateTime) =
        (customer, billingPeriod from till)
        ||> requestPayments
        |> savePayments

/// ------
/// Client
/// ------

module Client =
    open Billing

    let customer = 404

    download customer (DateTime(2019, 10, 1)) (DateTime(2019, 10, 31))
