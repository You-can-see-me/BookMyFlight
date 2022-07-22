import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

export class SignUp{
    userName: string = '';
    emailId: string='';
    password:string='';

    formRegisterGroup:FormGroup;

    
    constructor() {
        let _builder = new FormBuilder();
        this.formRegisterGroup = _builder.group({});
        this.formRegisterGroup.addControl('userNameControl', new FormControl('', Validators.required));
        this.formRegisterGroup.addControl('emailIdControl', new FormControl('', Validators.required));
        this.formRegisterGroup.addControl('passwordControl', new FormControl('', Validators.required));
    }
}