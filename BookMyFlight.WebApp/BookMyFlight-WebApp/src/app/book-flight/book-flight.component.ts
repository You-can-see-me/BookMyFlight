import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightUserService } from '../services/flight-user.service';
import { BookingResponse } from './bookingResponse.model';
import { FlightBookRequest } from './flightBook-request.model';
import { PassengerDetails } from "./PassengerDetails";

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {

  bookingResponse: BookingResponse = new BookingResponse();
  showSpinner: boolean = false;
  modalText: string = "";
  modalHeader: string = "";
  flightInventoryId: number = 0;
  flightBookRequest: FlightBookRequest = new FlightBookRequest();
  passengerDetailList: Array<PassengerDetails> = new Array<PassengerDetails>();
  passengerData: PassengerDetails = new PassengerDetails();
  constructor(private activatedRoute: ActivatedRoute, private _route: Router, private flightUserService: FlightUserService) {
    this.activatedRoute.params.subscribe(paramsId => {
      this.flightInventoryId = paramsId['id'];
    });
  }

  ngOnInit(): void {
    this.passengerData.index = 0;
    this.passengerDetailList.push(this.passengerData);
  }

  OnNoOfSeatChange(event: any) {
    this.passengerDetailList = [];
    if (event.target.value > 0) {
      for (let i = 0; i < Number(event.target.value); i++) {
        this.passengerData.index = i;
        this.passengerDetailList.push(this.passengerData);
        this.passengerData = new PassengerDetails();
      }
    }
    console.log(this.passengerDetailList);

  }

  BookTicket() {
    if(this.flightBookRequest.noOfSeats == undefined || this.flightBookRequest.noOfSeats <= 0)
    {
      this.DisplayModalPopup("Error", "Please select a minimum of 1 passenger to continue booking");
      return;
    }
    if(this.flightBookRequest.name == undefined || this.flightBookRequest.name == "")
    {
      this.DisplayModalPopup("Error", "Please enter Name");
      return;
    }
    if(this.flightBookRequest.seatType == undefined || this.flightBookRequest.seatType == "")
    {
      this.DisplayModalPopup("Error", "Please select seating class");
      return;
    }
    for(let passenger of this.passengerDetailList){
      if(passenger.firstName==undefined || passenger.firstName == ""
      || passenger.lastName==undefined || passenger.lastName == ""
      || passenger.gender==undefined || passenger.gender == ""
      || passenger.age==undefined || passenger.age <=0 
      || passenger.meal==undefined || passenger.meal == ""
      || passenger.seatNumber==undefined || passenger.seatNumber == ""){
        this.DisplayModalPopup("Error", "Please enter all passenger details");
        return;
      }
    }
    this.ShowSpinner();
    let bookTicketRequest: any = {
      emailId: localStorage.getItem('emailId'),
      name: this.flightBookRequest.name,
      noOfSeats: this.flightBookRequest.noOfSeats,
      bookedBy: localStorage.getItem('emailId'),
      bookedOn: Date.now().toString(),
      inventoryId: Number(this.flightInventoryId),
      discountUsed: this.flightBookRequest.discountUsed,
      userId: 1,
      seatType: this.flightBookRequest.seatType,
      passengerDetailList: this.passengerDetailList
    }
    console.log(bookTicketRequest);
    this.flightUserService.BookTicket(bookTicketRequest).subscribe(res=>{this.mapBookingResponse(res);
      this.HideSpinner();
      var modalText = this.bookingResponse.responseMessage+
                      "\nBookingId: "+this.bookingResponse.bookingId+
                      "\nPNR: "+this.bookingResponse.pnr+
                      "\nTicketCost: "+this.bookingResponse.ticketCost;
      this.DisplayModalPopup('Booking Response',modalText);
      },
      err=>{this.HideSpinner(), console.log(err)});
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

  mapBookingResponse(res: any){
    this.bookingResponse = res;
  }

}
