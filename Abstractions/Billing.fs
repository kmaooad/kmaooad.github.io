module Billing

open System

type Refund =
    { amount: decimal
      customer: int
      taxId: string option
      date: DateTime }

type BillingPeriod = { from: DateTime; till: DateTime }

type Payment =
    { customer_id: int
      date: DateTime
      amount: decimal }

let toRefund p =
    let norm a = -a
    { Refund.amount = norm p.amount
      Refund.taxId = None
      Refund.customer = p.customer_id
      Refund.date = p.date }

let filter ps = 
    ps
    |> Seq.filter (fun p -> p.amount < 0m)

let refundsCreatorImpl (payments: Payment seq) =
    payments
    |> filter
    |> Seq.map (toRefund)
    |> List.ofSeq

let refundsCreatorWithLookup payments (taxIdLookup: int -> string) =
    let withTaxId (r: Refund) =
        let taxId = taxIdLookup r.customer
        { r with taxId = Some taxId }

    payments
    |> filter
    |> Seq.map (toRefund >> withTaxId)
    |> List.ofSeq

let resultProcessorImpl payments iRefundsCreator iRefundsRepository =
    payments |> iRefundsCreator |> iRefundsRepository

let download customer period iDownloader iResultProcessor =
    (customer, period)
    ||> iDownloader
    |> iResultProcessor
