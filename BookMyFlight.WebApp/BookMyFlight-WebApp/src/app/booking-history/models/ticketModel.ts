import { PassengerDetails } from "./passengerDetails";

export class TicketModel {
    id:number=0;
    inventoryId:number=0;
    name:string="";
    emailId:string="";
    noOfSeats:number=0;
    bookedBy: string="";
    bookedOn: string="";
    discountUsed:string ="";
    seatType:string="";
    pnr:string="";
    userId:number=0;
    ticketCost:number=0;
    bookingStatus:string="";
    source:string="";
    destination:string="";
    airlineName:string="";
    passengerDetailList:Array<PassengerDetails> = new Array<PassengerDetails>();
}
