import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

export class AddScheduleRequest{
    airlineName: string="";
    airlineStatus: string="";
    flightNumber: string="";
    departure: string="";
    destination: string="";
    departureDateTime: Date=new Date();
    destinationDateTime: Date=new Date();
    scheduledDays: string="";
    modelType: string="";
    noOfBusinessClassSeats: string="";
    noOfNonBusinessClassSeats: string="";
    ticketCost: number=0;
    numberOfRows: number=0;
    meal: string = "";

    formScheduleAddGroup:FormGroup;

    constructor() {
        
        var numericValidators = [];
        numericValidators.push(Validators.required);
        numericValidators.push(Validators.pattern("^[0-9]+$"));

        var priceValidators = [];
        priceValidators.push(Validators.required);
        priceValidators.push(Validators.pattern("^[0-9]+(\.[0-9]*)?$"));

        var _builder = new FormBuilder();
        this.formScheduleAddGroup = _builder.group({});
        this.formScheduleAddGroup.addControl("flightNumberControl", new FormControl('', Validators.required));
        this.formScheduleAddGroup.addControl("businessSeatControl", new FormControl('', Validators.compose(numericValidators)));
        this.formScheduleAddGroup.addControl("regularSeatControl", new FormControl('', Validators.compose(numericValidators)));
        this.formScheduleAddGroup.addControl("ticketCostControl", new FormControl('', Validators.compose(priceValidators)));
        this.formScheduleAddGroup.addControl("rowControl", new FormControl('', Validators.compose(numericValidators)));
        this.formScheduleAddGroup.addControl("startDateControl", new FormControl('', Validators.required));
        this.formScheduleAddGroup.addControl("endDateControl", new FormControl('', Validators.required));
        this.formScheduleAddGroup.addControl("scheduledDaysControl", new FormControl('', Validators.required));

        
    }

}
