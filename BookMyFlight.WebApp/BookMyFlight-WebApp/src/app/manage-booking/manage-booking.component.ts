import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BookingHistory } from '../booking-history/booking-history.model';
import { TicketModel } from '../booking-history/models/ticketModel';
import { FlightUserService } from '../services/flight-user.service';

@Component({
  selector: 'app-manage-booking',
  templateUrl: './manage-booking.component.html',
  styleUrls: ['./manage-booking.component.css']
})
export class ManageBookingComponent implements OnInit {

  today: any;
  showSpinner: boolean = false;
  modalText: string = "";
  modalHeader: string = "";
  isNoResultFound: boolean = false;
  EmailID: string | undefined;
  private _bookingHistoryUrl = "https://localhost:44306/api/FlightBooking/GetBookingHistory/";
  bookingHistoryResponse: Array<BookingHistory> = new Array<BookingHistory>();
  ticketDetails: TicketModel = new TicketModel();
  constructor(private http: HttpClient, private flightUserService: FlightUserService) { }

  ngOnInit(): void {
    this.ShowSpinner();
    this.loadBookingHistory();
  }

  loadBookingHistory() {
    var emailID = localStorage.getItem('emailId');
    this.EmailID = emailID?.toString();
    this.flightUserService.GetBookingHistory(this.EmailID).subscribe(res => { this.HideSpinner(), this.GetFromServer(res) },
      err => { this.HideSpinner(), this.DisplayModalPopup("Error", err.error) });
  }

  GetFromServer(res: any) {
    this.bookingHistoryResponse = res;

    this.isNoResultFound = this.bookingHistoryResponse.length == 0 ? true : false;
  }

  DisplayModalPopup(modalHeader: string, modaltext: string) {
    this.modalHeader = modalHeader;
    this.modalText = modaltext;
    document.getElementById("btnLaunchModal")?.click();
  }

  ShowSpinner() {
    this.showSpinner = true;
  }

  HideSpinner() {
    this.showSpinner = false;
  }

  IsCancelAllowed(bookingHistory: BookingHistory): boolean {
    let result: boolean = false;
    this.today = new Date().toISOString().split('T')[0];
    if (bookingHistory.dateOfJourney.toString() > this.today) {
      result = true;
    }
    return result;
  }

  cancelTicket(pnr: string) {
    let isCancelConfirmed = window.confirm("Confirm ticket cancellation")

    if (isCancelConfirmed) {
      this.ShowSpinner();
      this.flightUserService.CancelTicket(pnr).subscribe(res => { this.HideSpinner(), this.DisplayModalPopup("Success", "Ticket cancelled successfully"), this.loadBookingHistory() },
        err => { this.HideSpinner(), console.log(err) });
    }


  }

  viewTicket(pnr: string) {
    this.ShowSpinner();
    this.flightUserService.GetPnrDetails(pnr).subscribe(res => { this.mapPnrDetails(res) },
      err => { this.HideSpinner(), console.log(err) });
    this.HideSpinner();
  }

  mapPnrDetails(res: any) {
    this.ticketDetails = res;
    let modalText = "PNR Number: " + this.ticketDetails.pnr + "\nCustomer Name: " + this.ticketDetails.name
      + "\nCustomer Email-Id: " + this.ticketDetails.emailId
      + "\nBooked On: " + (new Date(this.ticketDetails.bookedOn)).toLocaleString()
      + "\nSource: " + this.ticketDetails.source
      + "\nDestination: " + this.ticketDetails.destination
      + "\nBookingStatus: " + this.ticketDetails.bookingStatus
      + "\nSeatType: " + this.ticketDetails.seatType
      + "\n\nBooking Passenger Details";

    for (let i = 0; i < this.ticketDetails.passengerDetailList.length; i++) {
      modalText = modalText + "\n\nPassenger Name: " + this.ticketDetails.passengerDetailList[i].firstName + " " +
        this.ticketDetails.passengerDetailList[i].lastName
        + "\nPassenger Age: " + this.ticketDetails.passengerDetailList[i].age
        + "\nPassenger Gender: " + this.ticketDetails.passengerDetailList[i].gender
        + "\nSeat No: " + this.ticketDetails.passengerDetailList[i].seatNumber;
    }

    this.DisplayModalPopup("Ticket Details", modalText);
  }
}
