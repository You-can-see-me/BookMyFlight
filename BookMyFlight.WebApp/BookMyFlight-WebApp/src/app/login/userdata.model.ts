import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

export class UserData{
    emailId: string='';
    password:string='';
    formLoginGroup:FormGroup;

    constructor() {
        var _builder = new FormBuilder();
        this.formLoginGroup = _builder.group({});
        this.formLoginGroup.addControl('emailIdControl', new FormControl('', Validators.required));
        this.formLoginGroup.addControl('passwordControl', new FormControl('', Validators.required));
        
    }
}