module Container

open Billing
open Impl

let retryingHttpClientDI =
    fun u f -> retyingHttpClientImpl u f httpClientImpl 

let downloaderDI (c: int) (p: BillingPeriod) =
    (c, p)
    ||> 
    requestPaymentsImpl
    <||| 
    (requestFactoryImpl, retryingHttpClientDI, responseHandlerImpl)

let resultProcessorDI payments =
    payments
    |> resultProcessorImpl
    <|| (refundsCreatorImpl, refundRepositoryImpl)

