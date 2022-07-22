import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class FlightUserService {

  private _bookingHistoryUrl="https://localhost:44341/api/v1.0/flight/booking/history/";
  private _cancelTicketUrl = "https://localhost:44341/api/v1.0/flight/booking/cancel/";
  private _pnrDetailTicketUrl = "https://localhost:44341/api/v1.0/flight/ticket/";
  private _bookTicketUrl = "https://localhost:44341/api/v1.0/flight/booking/";

  constructor(private http:HttpClient,private _router:Router) { }

  GetBookingHistory(emailId:string | undefined)
  {
    return this.http.get(this._bookingHistoryUrl + emailId);
  }

  CancelTicket(pnr: string){
    return this.http.delete(this._cancelTicketUrl + pnr);
  }

  GetPnrDetails(pnr: string){
    return this.http.get(this._pnrDetailTicketUrl + pnr);
  }

  BookTicket(bookingRequest: any){
    return this.http.post(this._bookTicketUrl + bookingRequest.inventoryId,bookingRequest);
  }
}
