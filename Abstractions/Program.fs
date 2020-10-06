module Program

open Billing
open System
open Container


let download () = 
  let customer = 404

  let period =
      { from = DateTime(2019, 10, 1)
        till = DateTime(2019, 10, 31) }

  (customer, period)
  ||> 
  Billing.download
  <|| 
  (downloaderDI,
   resultProcessorDI)
