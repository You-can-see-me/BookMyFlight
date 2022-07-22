import { NgForm, FormGroup, Validators, FormBuilder, FormControl } from "@angular/forms";

export class SearchRequest {
    fromCity: string = '';
    toCity: string = '';
    departureDate: Date = new Date();
    returnDate: Date = new Date();
    isRoundTrip: boolean = false;
    formSearchGroup: FormGroup;//Create
    constructor() {
        var _builder = new FormBuilder();
        this.formSearchGroup = _builder.group({});
        this.formSearchGroup.addControl("FromCityControl", new FormControl('', Validators.required));
        this.formSearchGroup.addControl("ToCityControl", new FormControl('', Validators.required));

        // var validationcollection = [];
        // validationcollection.push(Validators.required);
        // validationcollection.push(Validators.pattern("^[0-9]{4,4}$"))
        // this.formSearchGroup.addControl("CustomerCodeControl", new FormControl('', Validators.compose(validationcollection)));

    }
}