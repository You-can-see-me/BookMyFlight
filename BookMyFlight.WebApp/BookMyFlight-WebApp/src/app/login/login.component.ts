import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserData } from './userdata.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginModel: UserData = new UserData();
  showSpinner: boolean = false;
  modalText:string="";
  modalHeader:string="";
  constructor(private _auth: AuthService, private _router: Router) { }

  ngOnInit(): void {
  }
  Login() {
    if (this.loginModel.emailId == '' || this.loginModel.password == '') {
      this.DisplayModalPopup("Error", "Please enter the username and password.")
      return;
    }
    let loginRequest = {
      emailId:this.loginModel.emailId,
      password:this.loginModel.password
    };
    this.ShowSpinner();
    this._auth.loginUser(loginRequest).subscribe(res => {this.HideSpinner(),
      localStorage.setItem('token', res.token);
      localStorage.setItem('emailId', this.loginModel.emailId);
      localStorage.setItem('usertype', 'user');
      this._router.navigate(['/Home']);
    }, res =>{console.log(res), this.HideSpinner(), this.DisplayModalPopup("Unauthorised", "Authorisation for the user failed!")});
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

  hasError(typeofvalidator:string,controlname:string):Boolean{
    return this.loginModel.formLoginGroup.controls[controlname].hasError(typeofvalidator);
  }

}
