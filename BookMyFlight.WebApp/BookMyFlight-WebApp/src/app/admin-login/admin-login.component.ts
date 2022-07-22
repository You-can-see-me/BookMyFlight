import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { AdminData } from './admindata.model';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent implements OnInit {

  adminLoginModel: AdminData = new AdminData();
  showSpinner: boolean = false;
  modalText:string="";
  modalHeader:string="";
  constructor(private _auth: AuthService, private _router: Router) { }

  ngOnInit(): void {
  }

  Login() {
    if (this.adminLoginModel.userName == '' || this.adminLoginModel.password == '') {
      this.DisplayModalPopup("Error", "Please enter the username and password.")
      return;
    }
    let loginRequest = {
      username:this.adminLoginModel.userName,
      password:this.adminLoginModel.password
    };
    this.ShowSpinner();
    
    this._auth.loginAdmin(loginRequest).subscribe(res => {this.HideSpinner(),
      localStorage.setItem('token', res.token);
      localStorage.setItem('usertype', 'admin');
      this._router.navigate(['/admin']);
    }, res=>{console.log(res), this.HideSpinner(), this.DisplayModalPopup("Unauthorised", "Authorisation for the user failed!")});
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
    return this.adminLoginModel.formLoginGroup.controls[controlname].hasError(typeofvalidator);
  }

}
