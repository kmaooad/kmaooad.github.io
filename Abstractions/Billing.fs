module Billing

open System

type Refund =
    { amount: decimal
      customer: int
      date: DateTime }

type BillingPeriod = { from: DateTime; till: DateTime }

type Payment =
    { customer_id: int
      date: DateTime
      amount: decimal }


let refundsCreatorImpl payments =
    let norm a = -a

    seq {
        for p in payments do
            if p.amount < 0m then
                yield
                    { Refund.amount = norm p.amount
                      Refund.customer = p.customer_id
                      Refund.date = p.date }
    }
    |> List.ofSeq


let resultProcessorImpl payments iRefundsCreator iRefundsRepository =
    payments |> iRefundsCreator |> iRefundsRepository

let download customer period iDownloader iResultProcessor =
    (customer, period)
    ||> iDownloader // (customer,period) -> Payment list
    |> iResultProcessor // Payment list -> unit
