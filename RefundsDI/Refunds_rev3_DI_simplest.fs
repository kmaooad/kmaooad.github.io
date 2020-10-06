module Refunds31

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

    let toRefund p =
        { Refund.amount = -p.amount
          Refund.customer = p.customer_id
          Refund.date = p.date }

    let savePayments pp =
        pp
        |> List.filter (fun p -> p.amount < 0m)
        |> List.map toRefund
        |> List.iter (DB.insert "refunds")

    let download customer period requestPayments =
        (customer, period)
        ||> requestPayments
        |> savePayments

/// ------
/// Client
/// ------

module Client =
    open Billing
    open Domain

    [<Literal>]
    let Url = "https://fancybilling.kmaooadtech.io"

    let requestPaymentsImpl (customer: int) (p: BillingPeriod) =

        let request =
            [ ("customer", customer |> string)
              ("from", p.from |> string)
              ("till", p.till |> string) ]

        Http.RequestString(Url, httpMethod = "POST", body = FormValues request)
        |> Json.deserialize<Payment list>

    let customer = 404

    let period =
        { from = DateTime(2019, 10, 1)
          till = DateTime(2019, 10, 31) }

    download customer period requestPaymentsImpl
