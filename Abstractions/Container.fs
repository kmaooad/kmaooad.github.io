module Container

open Billing
open Impl

// aka pattern "Decorator"
let retryingHttpClientDI =
    fun u f -> retyingHttpClientImpl u f httpClientImpl 

// aka pattern "Adapter"
let refundsCreatorWithLookupDI = 
    fun payments -> refundsCreatorWithLookup payments taxIdLookup

let downloaderDI (c: int) (p: BillingPeriod) =
    (c, p)
    ||> 
    requestPaymentsImpl
    <||| 
    (requestFactoryImpl, retryingHttpClientDI, responseHandlerImpl)

let resultProcessorDI payments =
    payments
    |> resultProcessorImpl
    <|| (refundsCreatorWithLookupDI, refundRepositoryImpl)

