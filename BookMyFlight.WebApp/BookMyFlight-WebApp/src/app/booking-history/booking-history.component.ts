import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FlightUserService } from '../services/flight-user.service';
import { BookingHistory } from './booking-history.model';
import { TicketModel } from './models/ticketModel';

@Component({
  selector: 'app-booking-history',
  templateUrl: './booking-history.component.html',
  styleUrls: ['./booking-history.component.css']
})
export class BookingHistoryComponent implements OnInit {

  showSpinner:boolean=false;
  modalText:string="";
  modalHeader:string="";
  pnrNumber: string = "";
  ticketDetails = new TicketModel();
  isNoResultFound: boolean = false;
  showResultList: boolean = false;
  constructor(private http: HttpClient, private flightUserService: FlightUserService) { }

  ngOnInit(): void {
  }

  GetTicketHistory() {
    this.ShowSpinner();
    this.flightUserService.GetPnrDetails(this.pnrNumber).subscribe(res=>{this.MapTicketDetails(res),this.HideSpinner()},
    err=>{this.HideSpinner(),console.log(err)});
  }

  MapTicketDetails(res: any){
    this.ticketDetails = res;
    if(this.ticketDetails.id == 0){
      this.isNoResultFound = true;
    }
    else{
      this.showResultList = true;
    }
    console.log(this.ticketDetails);
  }

  DisplayModalPopup(modalHeader:string, modaltext:string)
{
  this.modalHeader = modalHeader;
  this.modalText=modaltext;
  document.getElementById("btnLaunchModal")?.click();
}

ShowSpinner()
{
  this.showSpinner = true;
}

HideSpinner()
{
  this.showSpinner = false;
}

ViewTicketDetails(){
  let modalText = "PNR Number: " + this.ticketDetails.pnr + "\nCustomer Name: " + this.ticketDetails.name
   +"\nCustomer Email-Id: " + this.ticketDetails.emailId
   +"\nBooked On: " + (new Date(this.ticketDetails.bookedOn)).toLocaleString()
   +"\nSource: " + this.ticketDetails.source
   +"\nDestination: " + this.ticketDetails.destination
   +"\nBookingStatus: " + this.ticketDetails.bookingStatus
   +"\nSeatType: " + this.ticketDetails.seatType
   +"\n\nBooking Passenger Details";

   for(let i = 0; i < this.ticketDetails.passengerDetailList.length; i++)
   {
     modalText = modalText + "\n\nPassenger Name: " + this.ticketDetails.passengerDetailList[i].firstName + " " + 
     this.ticketDetails.passengerDetailList[i].lastName
     +"\nPassenger Age: " + this.ticketDetails.passengerDetailList[i].age
     +"\nPassenger Gender: " + this.ticketDetails.passengerDetailList[i].gender
     +"\nSeat No: " + this.ticketDetails.passengerDetailList[i].seatNumber;
   }

   this.DisplayModalPopup("Ticket Details", modalText);
}

}
