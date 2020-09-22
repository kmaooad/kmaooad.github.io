module Refunds5

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


    let refundRepositoryImpl refunds =
        refunds |> List.iter (DB.insert "refunds")

    let resultProcessorImpl payments iRefundsCreator iRefundsRepository =
        payments |> iRefundsCreator |> iRefundsRepository

    let download customer period iDownloader iResultProcessor =
        (customer, period)
        ||> iDownloader // (customer,period) -> Payment list
        |> iResultProcessor // Payment list -> unit

module Config =
    [<Literal>]
    let BillingUrl = "https://fancybilling.kmaooadtech.io"

/// ------
/// Client
/// ------

module Client =
    open Billing
    open Domain


    let httpClientImpl url formData =
        Http.RequestString(url, httpMethod = "POST", body = FormValues formData)

    let requestFactoryImpl (customer: int) (p: BillingPeriod) =
        let body =
            [ ("customer", customer |> string)
              ("from", p.from |> string)
              ("till", p.till |> string) ]

        (Config.BillingUrl, body)

    let responseHandlerImpl (response: string): Payment list =
        response |> Json.deserialize<Payment list>

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
    let downloaderImpl (customer: int) (p: BillingPeriod) =
        (customer, period)
        ||> requestPaymentsImpl
        <||| (requestFactoryImpl, httpClientImpl, responseHandlerImpl)

    // Split DI from passing params (objects imitation!)

    let _DI_resultProcessorImpl payments =
        payments
        |> resultProcessorImpl
        <|| (refundsCreatorImpl, refundRepositoryImpl)

    let _DI_download c p =
        (c, p)
        ||> download
        <|| (downloaderImpl, _DI_resultProcessorImpl)

    (customer, period) ||> _DI_download
