module Container

open Billing
open Impl

let downloaderDI (c: int) (p: BillingPeriod) =
    (c, p)
    ||> 
    requestPaymentsImpl
    <||| 
    (requestFactoryImpl, httpClientImpl, responseHandlerImpl)

let resultProcessorDI payments =
    payments
    |> resultProcessorImpl
    <|| (refundsCreatorImpl, refundRepositoryImpl)
