import { PassengerDetails } from "./PassengerDetails";

export class FlightBookRequest {
        emailId: string = '';
        name: string = '';
        noOfSeats: number = 1;
        bookedBy: string ='';
        bookedOn: string = '';
        inventoryId: number =0;
        discountUsed: string = '';
        userId: number = 0;
        seatType: string = '';
        passengerDetailList: PassengerDetails[]=[];

}
